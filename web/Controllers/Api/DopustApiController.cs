using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
using web.Filters;


namespace web.Controllers_Api
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuth]

    public class DopustApiController : ControllerBase
    {
        private readonly EkadriContext _context;

        public DopustApiController(EkadriContext context)
        {
            _context = context;
        }

        // GET: api/DopustApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dopust>>> GetDopusti()
        {
            return await _context.Dopusti.ToListAsync();
        }

        // GET: api/DopustApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dopust>> GetDopust(int id)
        {
            var dopust = await _context.Dopusti.FindAsync(id);

            if (dopust == null)
            {
                return NotFound();
            }

            return dopust;
        }

        // PUT: api/DopustApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDopust(int id, Dopust dopust)
        {
            if (id != dopust.ID)
            {
                return BadRequest();
            }

            _context.Entry(dopust).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DopustExists(id))
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

        // POST: api/DopustApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Dopust>> PostDopust(Dopust dopust)
        {
            _context.Dopusti.Add(dopust);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDopust", new { id = dopust.ID }, dopust);
        }

        // DELETE: api/DopustApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Dopust>> DeleteDopust(int id)
        {
            var dopust = await _context.Dopusti.FindAsync(id);
            if (dopust == null)
            {
                return NotFound();
            }

            _context.Dopusti.Remove(dopust);
            await _context.SaveChangesAsync();

            return dopust;
        }

        private bool DopustExists(int id)
        {
            return _context.Dopusti.Any(e => e.ID == id);
        }
    }
}
