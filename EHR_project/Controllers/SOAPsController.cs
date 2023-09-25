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
using Microsoft.AspNetCore.Authorization;

namespace EHR_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SOAPsController : ControllerBase
    {
        private readonly DBContext _context;

        public SOAPsController(DBContext context)
        {
            _context = context;
        }

        // GET: api/SOAPs/5
       
        [HttpGet("{id}")]
        public async Task<ActionResult<SOAP>> GetSOAP(int id)
        {
          if (_context.soap == null)
          {
              return NotFound();
          }
            var sOAP = await _context.soap.FindAsync(id);

            if (sOAP == null)
            {
                return NotFound();
            }

            return sOAP;
        }

        // PUT: api/SOAPs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSOAP(int id, SOAP sOAP)
        {
            if (id != sOAP.Id)
            {
                return BadRequest();
            }

            _context.Entry(sOAP).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SOAPExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SOAPs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
     
        [HttpPost("addSoap")]
        public async Task<ActionResult<SOAP>> PostSOAP(SOAPDto SoapDto)
        {
          if (_context.soap == null)
          {
              return Problem("Entity set 'DBContext.soap'  is null.");
          }
            SOAP Soap = new SOAP();
            Soap.AppointmentId= SoapDto.AppointmentId;
            Soap.Assessment= SoapDto.Assessment;
            Soap.Objective = SoapDto.Objective;
            Soap.Subjective= SoapDto.Subjective;
            Soap.Plan= SoapDto.Plan;
            await _context.soap.AddAsync(Soap);
            await _context.SaveChangesAsync();

            var Appointment = await _context.appointment.FindAsync(SoapDto.AppointmentId);
            Appointment.AppointmentStatus = "Completed";
            await _context.SaveChangesAsync();

            return Ok();
        }

        [Authorize]
        [HttpGet("cancelAppointment/{id}")]
        public async Task<IActionResult> cancelAppointment(int id)
        {
            var appointment = await _context.appointment.FindAsync(id);
            if (appointment == null) { return BadRequest(); }
            appointment.AppointmentStatus = "Cancel";
            await _context.SaveChangesAsync();
            return Ok();
    
        }
        
        [HttpGet("SOAPByAppointmentId/{id}")]
        public async Task<IActionResult> SOAPByAppointmentId(int id)
        {
            var appointment = await _context.soap.FirstOrDefaultAsync(x=>(x.AppointmentId==id));
            if (appointment == null) { return BadRequest(); }
            var i = appointment.Id;
            var SOAP= await _context.soap.FindAsync(i);
            return Ok(SOAP);

        }


        private bool SOAPExists(int id)
        {
            return (_context.soap?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
