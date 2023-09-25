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
    public class ProvidersController : ControllerBase
    {
        private readonly DBContext _context;

        public ProvidersController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Providers
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Provider>>> Getprovider()
        {
          if (_context.provider == null)
          {
              return NotFound();
          }
            return await _context.provider.ToListAsync();
        }

        // GET: api/Providers/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Provider>> GetProvider(int id)
        {
          if (_context.provider == null)
          {
              return NotFound();
          }
            var provider = await _context.provider.Where(x=>(x.Speciality==id)).ToListAsync();

            if (provider == null)
            {
                return NotFound();
            }

            return Ok( provider);
        }
        
        [HttpGet("getByProviderId/{id}")]
        public async Task<ActionResult<Provider>> getByProviderId(int id)
        {
            if (_context.provider == null)
            {
                return NotFound();
            }
            var provider = await _context.provider.FindAsync(id);

            if (provider == null)
            {
                return NotFound();
            }

            return Ok(provider.First_name+" "+provider.Last_name);
        }
        [Authorize]
        [HttpGet("getProvider/{id}")]
        public async Task<ActionResult<Provider>> getProvider(int id)
        {
            if (_context.provider == null)
            {
                return NotFound();
            }
            var provider = await _context.provider.FindAsync(id);

            if (provider == null)
            {
                return NotFound();
            }

            return Ok(provider);
        }
        [Authorize]
        [HttpPut("update/{id}")]
        public async Task<ActionResult> update(int id,ProviderUpdateDto providerDto)
        {
            var provider = await _context.provider.FindAsync(id);
            if (provider == null) { return NotFound(); }
            provider.First_name= providerDto.First_name;
            provider.Last_name= providerDto.Last_name;
            provider.Address= providerDto.Address;
            provider.Mobile= providerDto.Mobile;
            provider.Experience= (int)providerDto.Experience;
            provider.Position= providerDto.Position;
            provider.Speciality=(int)providerDto.Speciality;
           
           
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
