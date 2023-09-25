using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EHR_project.Data;
using EHR_project.Models;
using EHR_project.Dto;
using Mapster;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using EHR_project.encryption;
using NuGet.Protocol.Plugins;

namespace EHR_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly DBContext _context;

        public AuthenticationController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Authentication
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> Getusers()
        {
            if (_context.users == null)
            {
                return NotFound();
            }
            return await _context.users.ToListAsync();
        }


        [HttpPost("register")]
        public async Task<ActionResult<Users>> PostUsers(RegisterDto userdto)
        {
            if (_context.users == null)
            {
                return Problem("Entity set 'DBContext.users'  is null.");
            }
            // _context.users.Add(users);
            var random = new Random();
            string str = "abcdefghijklmnopqrstuvwxyz";
            var rand_string = "";
            for (int i = 0; i < 7; i++)
            {
                rand_string = rand_string + str[random.Next(0, 26)];
            }

            UsersDto users = new UsersDto();
            users.Username = userdto.Username;
            users.User_type = userdto.User_type;
            users.Password = rand_string;
            Users DBuser = users.Adapt<Users>();
            await _context.users.AddAsync(DBuser);

            var emailAuth = await _context.patient.Where(x => x.Email == userdto.Email).ToArrayAsync();
            var count=0;
            count = emailAuth.Count();
            var emailAuth2 = await _context.provider.Where(x => x.Email == userdto.Email).ToArrayAsync();
            count = count+ emailAuth2.Count();
            if (count == 0)
            {
                var result = _context.SaveChanges();

                if (result > 0)
                {
                    if (userdto.User_type == 1)
                    {

                        PatientDto patientDto = new PatientDto();
                        patientDto.UserId = DBuser.UserId;
                        patientDto.FirstName = userdto.FirstName;
                        patientDto.LastName = userdto.LastName;
                        patientDto.Address = userdto.Address;
                        patientDto.Email = userdto.Email;
                        patientDto.Phone = userdto.Phone;
                        patientDto.DOB = userdto.DOB;
                        patientDto.InsuranceNo = userdto.InsuranceNo;
                        Patient DBpatient = patientDto.Adapt<Patient>();
                        await _context.patient.AddAsync(DBpatient);
                        _context.SaveChanges();

                    }
                    if (userdto.User_type == 2)
                    {
                        ProviderDto providerDto = new ProviderDto();
                        providerDto.UserId = DBuser.UserId;
                        providerDto.First_name = userdto.FirstName;
                        providerDto.Last_name = userdto.LastName;
                        providerDto.Mobile = userdto.Phone;
                        providerDto.Email = userdto.Email;
                        providerDto.DOB = (DateTime)userdto.DOB;
                        providerDto.Address = userdto.Address;
                        Provider DBprovider = providerDto.Adapt<Provider>();
                        await _context.provider.AddAsync(DBprovider);
                        _context.SaveChanges();
                       

                    }
                    var otp = random.Next(1000, 9999);
                    OTP DBotp = new OTP();
                    DBotp.UserId = DBuser.UserId;
                    DBotp.Otp = otp;
                    await _context.otp.AddAsync(DBotp);
                    _context.SaveChanges();

                    sendEmail(rand_string,userdto.Email,DBuser.UserId, userdto.Username, otp);

                }
                // await _context.SaveChangesAsync();

                // return CreatedAtAction("GetUsers", new { id = users.UserId }, users);
            }
            else
            {
                return Ok(new { Message = "emailfound" });
            }
            return Ok(true);
        }



        [HttpPost("OTP")]
        public async Task<ActionResult> OtpValidation(OtpAuth otpAuth)
        {
            var id = EncryptDecrypt.Decrypt(otpAuth.UserId);
            
            var user = await _context.otp.FirstOrDefaultAsync(o => o.UserId ==int.Parse(id)  && o.Otp== otpAuth.Otp);
            if (user != null)
            {
                Users users = await _context.users.FindAsync(int.Parse(id));
                users.isValidate = true;
                _context.SaveChanges();
                return Ok(true);

                
            }
            return Ok(false);

        }



        [HttpPost("login")]
        public async Task<ActionResult<IEnumerable<Users>>> login([FromBody] LoginDto loginDto ) 
        {
            Patient p = new Patient();
            var user = await _context.users.FirstOrDefaultAsync(x=>(x.Username==loginDto.username &&x.Password== loginDto.password));

            if (user == null)
            {
                return Ok(new { Message = "notfound" });
            }

            if(user.isValidate==false)
            {
                return Ok(new {Message="notactive"});

            }
            if (user.User_type == 1)
            {
                var patient = await _context.patient.FirstOrDefaultAsync(x => (x.UserId == user.UserId));
                var token = CreateJwt(user);
                return Ok(new
                {
                    user = patient,
                    Token = token,
                    FirstName=p.FirstName, 
                    LastName=p.LastName




                });
            }
            if (user.User_type == 2)
            {
                var provider = await _context.provider.FirstOrDefaultAsync(x => (x.UserId == user.UserId));
                var token = CreateJwt(user);
                return Ok(new
                {
                    user = provider,
                    Token = token
                });
            }
            return BadRequest();

        }







        [NonAction]
        private string CreateJwt(Users user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("This Is SampleSecured Key.....");
            var role = user.User_type == 1 ? "Provider" : "Patient";
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.Name, user.Username )
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            return jwtTokenHandler.WriteToken(token);
        }


        //Email API
        [HttpPost("sendemail")]
        public IActionResult sendEmail(string Password, string UserEmail, int id, string username, int otp)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("balramrathod119@gmail.com"));
            email.To.Add(MailboxAddress.Parse(UserEmail));
            email.Subject = "One Time Password";
            string string_id = id.ToString();
            var Encrypt_id = EncryptDecrypt.Encrypt(string_id);


            var url = "http://localhost:4200/activeAccount?id=" + Encrypt_id;
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = "<h1>Registration Completed!</h1><br><h3>Wellcome to EHR</h3><br><p2>Dear user you need to active your account to use</p2> <br>" + " <a href=" + url + ">active now</a> <br> Your OTP is "+otp+"<br>  " + "<h3>your Password is : " + Password + " </h3> " + "<br>" + "<h3>your username is : " + username + " </h3>" + ""
            };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("balramrathod119@gmail.com", "jijdnmhvumfgmmgr");
            smtp.Send(email);
            smtp.Disconnect(true);
            return Ok();



        }

    }
}
