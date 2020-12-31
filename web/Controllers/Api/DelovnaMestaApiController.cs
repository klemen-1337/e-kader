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
    public class DelovnaMestaApiController : ControllerBase
    {
        private readonly EkadriContext _context;

        public DelovnaMestaApiController(EkadriContext context)
        {
            _context = context;
        }

        // GET: api/DelovnaMestaApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DelovnaMesta>>> GetDelovnaMesta()
        {
            return await _context.DelovnaMesta.ToListAsync();
        }

        // GET: api/DelovnaMestaApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DelovnaMesta>> GetDelovnaMesta(int id)
        {
            var delovnaMesta = await _context.DelovnaMesta.FindAsync(id);

            if (delovnaMesta == null)
            {
                return NotFound();
            }

            return delovnaMesta;
        }

        // PUT: api/DelovnaMestaApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDelovnaMesta(int id, DelovnaMesta delovnaMesta)
        {
            if (id != delovnaMesta.DelovnaMestaID)
            {
                return BadRequest();
            }

            _context.Entry(delovnaMesta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DelovnaMestaExists(id))
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

        // POST: api/DelovnaMestaApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DelovnaMesta>> PostDelovnaMesta(DelovnaMesta delovnaMesta)
        {
            _context.DelovnaMesta.Add(delovnaMesta);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DelovnaMestaExists(delovnaMesta.DelovnaMestaID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDelovnaMesta", new { id = delovnaMesta.DelovnaMestaID }, delovnaMesta);
        }

        // DELETE: api/DelovnaMestaApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DelovnaMesta>> DeleteDelovnaMesta(int id)
        {
            var delovnaMesta = await _context.DelovnaMesta.FindAsync(id);
            if (delovnaMesta == null)
            {
                return NotFound();
            }

            _context.DelovnaMesta.Remove(delovnaMesta);
            await _context.SaveChangesAsync();

            return delovnaMesta;
        }

        private bool DelovnaMestaExists(int id)
        {
            return _context.DelovnaMesta.Any(e => e.DelovnaMestaID == id);
        }
    }
}
