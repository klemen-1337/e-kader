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
    public class ZaposlitevApiController : ControllerBase
    {
        private readonly EkadriContext _context;

        public ZaposlitevApiController(EkadriContext context)
        {
            _context = context;
        }

        // GET: api/ZaposlitevApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Zaposlitve>>> GetZaposlitve()
        {
            return await _context.Zaposlitve.ToListAsync();
        }

        // GET: api/ZaposlitevApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Zaposlitve>> GetZaposlitve(int id)
        {
            var zaposlitve = await _context.Zaposlitve.FindAsync(id);

            if (zaposlitve == null)
            {
                return NotFound();
            }

            return zaposlitve;
        }

        // PUT: api/ZaposlitevApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutZaposlitve(int id, Zaposlitve zaposlitve)
        {
            if (id != zaposlitve.ID)
            {
                return BadRequest();
            }

            _context.Entry(zaposlitve).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZaposlitveExists(id))
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

        // POST: api/ZaposlitevApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Zaposlitve>> PostZaposlitve(Zaposlitve zaposlitve)
        {
            _context.Zaposlitve.Add(zaposlitve);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetZaposlitve", new { id = zaposlitve.ID }, zaposlitve);
        }

        // DELETE: api/ZaposlitevApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Zaposlitve>> DeleteZaposlitve(int id)
        {
            var zaposlitve = await _context.Zaposlitve.FindAsync(id);
            if (zaposlitve == null)
            {
                return NotFound();
            }

            _context.Zaposlitve.Remove(zaposlitve);
            await _context.SaveChangesAsync();

            return zaposlitve;
        }

        private bool ZaposlitveExists(int id)
        {
            return _context.Zaposlitve.Any(e => e.ID == id);
        }
    }
}
