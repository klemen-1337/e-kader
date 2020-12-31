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
    public class DelovneUreApiController : ControllerBase
    {
        private readonly EkadriContext _context;

        public DelovneUreApiController(EkadriContext context)
        {
            _context = context;
        }

        // GET: api/DelovneUreApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DelovneUre>>> GetDelovneUre()
        {
            return await _context.DelovneUre.ToListAsync();
        }

        // GET: api/DelovneUreApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DelovneUre>> GetDelovneUre(int id)
        {
            var delovneUre = await _context.DelovneUre.FindAsync(id);

            if (delovneUre == null)
            {
                return NotFound();
            }

            return delovneUre;
        }

        // PUT: api/DelovneUreApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDelovneUre(int id, DelovneUre delovneUre)
        {
            if (id != delovneUre.ID)
            {
                return BadRequest();
            }

            _context.Entry(delovneUre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DelovneUreExists(id))
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

        // POST: api/DelovneUreApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DelovneUre>> PostDelovneUre(DelovneUre delovneUre)
        {
            _context.DelovneUre.Add(delovneUre);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDelovneUre", new { id = delovneUre.ID }, delovneUre);
        }

        // DELETE: api/DelovneUreApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DelovneUre>> DeleteDelovneUre(int id)
        {
            var delovneUre = await _context.DelovneUre.FindAsync(id);
            if (delovneUre == null)
            {
                return NotFound();
            }

            _context.DelovneUre.Remove(delovneUre);
            await _context.SaveChangesAsync();

            return delovneUre;
        }

        private bool DelovneUreExists(int id)
        {
            return _context.DelovneUre.Any(e => e.ID == id);
        }
    }
}
