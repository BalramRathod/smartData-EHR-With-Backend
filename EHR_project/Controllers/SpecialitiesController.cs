using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EHR_project.Data;
using EHR_project.Models;
using Microsoft.AspNetCore.Authorization;

namespace EHR_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialitiesController : ControllerBase
    {
        private readonly DBContext _context;

        public SpecialitiesController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Specialities
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Speciality>>> Getspeciality()
        {
          if (_context.speciality == null)
          {
              return NotFound();
          }
            return await _context.speciality.ToListAsync();
        }

        // GET: api/Specialities/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Speciality>> GetSpeciality(int id)
        {
          if (_context.speciality == null)
          {
              return NotFound();
          }
            var speciality = await _context.speciality.FindAsync(id);

            if (speciality == null)
            {
                return NotFound();
            }

            return speciality;
        }



        // POST: api/Specialities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Speciality>> PostSpeciality(Speciality speciality)
        {
          if (_context.speciality == null)
          {
              return Problem("Entity set 'DBContext.speciality'  is null.");
          }
            _context.speciality.Add(speciality);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpeciality", new { id = speciality.Id }, speciality);
        }

        // DELETE: api/Specialities/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpeciality(int id)
        {
            if (_context.speciality == null)
            {
                return NotFound();
            }
            var speciality = await _context.speciality.FindAsync(id);
            if (speciality == null)
            {
                return NotFound();
            }

            _context.speciality.Remove(speciality);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpecialityExists(int id)
        {
            return (_context.speciality?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
