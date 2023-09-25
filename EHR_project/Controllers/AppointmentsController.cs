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
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Dapper;
using Microsoft.AspNetCore.Authorization;

namespace EHR_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly DBContext _context;

        public AppointmentsController(DBContext context)
        {
            _context = context;
        }



        
        [HttpGet("GetAppointmentUserId/{id}")]
        public async Task<ActionResult> GetAppointments(int id)
        {
            var DbUser = await _context.users.FindAsync(id);
            if (DbUser.User_type == 2)
            {
                var DbProvider = await _context.provider.FirstOrDefaultAsync(x => x.UserId == id);

                using var connection = _context.Database.GetDbConnection();
                var dbParams = new
                {
                    Provider_Id = DbProvider.ProviderId,
                };
                var appointmentEnumarables = await connection.QueryAsync<AppointmentListPatientDto>
                (
                    "spSD_Get_Appointments_By_Provider",
                    dbParams,
                    commandType: System.Data.CommandType.StoredProcedure
                );

                var appointmentListDto = appointmentEnumarables.AsList();

                return Ok(appointmentListDto);
            }
            if (DbUser.User_type == 1)
            {
                var DbPatient = await _context.patient.FirstOrDefaultAsync(x => x.UserId == DbUser.UserId);

                using var connection = _context.Database.GetDbConnection();
                var dbParams = new
                {
                    Patient_Id = DbPatient.PatientId,
                };
                var appointmentEnumarables = await connection.QueryAsync<AppointmentListProviderDto>
                (
                    "spSD_Get_Appointments_By_Patient",
                    dbParams,
                    commandType: System.Data.CommandType.StoredProcedure
                );

                var appointmentListDto = appointmentEnumarables.AsList();

                return Ok(appointmentListDto);
            }

            return Ok(new
            {
                message = "Not Found"
            });
        }






        /*  // GET: api/Appointments/5
          [HttpGet("PatientById/{id}")]
          public async Task<ActionResult<Appointment>> GetAppointment(int id)
          {
            if (_context.appointment == null)
            {
                return NotFound();
            }
              var appointment = await _context.appointment.Where(x=>(x.PatientId==id)).ToListAsync();

              if (appointment == null)
              {
                  return Ok(new {Message="empty"});
              }

              return Ok( appointment);
          }

          [HttpGet("ProviderById/{id}")]
          public async Task<ActionResult<Appointment>> GetByProviderId(int id)
          {
              if (_context.appointment == null)
              {
                  return NotFound();
              }
              var appointment = await _context.appointment.Where(x => (x.ProviderId == id)).ToListAsync();

              if (appointment == null)
              {
                  return Ok(new { Message = "empty" });
              }

              return Ok(appointment);
          }
        */



        // POST: api/Appointments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Appointment>> PostAppointment(AppointmentDto appointmentDto)
        {
          if (_context.appointment == null)
          {
              return Problem("Entity set 'DBContext.appointment'  is null.");
          }

            Appointment appointment = appointmentDto.Adapt<Appointment>();
            await _context.AddAsync(appointment);
            await _context.SaveChangesAsync();

            return Ok();
        }

       
        private bool AppointmentExists(int id)
        {
            return (_context.appointment?.Any(e => e.AppointmentId == id)).GetValueOrDefault();
        }
    }
}
