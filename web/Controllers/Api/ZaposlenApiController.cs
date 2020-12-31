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

    public class ZaposlenApiController : ControllerBase
    {
        private readonly EkadriContext _context;

        public ZaposlenApiController(EkadriContext context)
        {
            _context = context;
        }

        // GET: api/ZaposlenApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Zaposlen>>> GetZaposleni()
        {
            return await _context.Zaposleni.ToListAsync();
        }

        // GET: api/ZaposlenApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Zaposlen>> GetZaposlen(int id)
        {
            var zaposlen = await _context.Zaposleni.FindAsync(id);

            if (zaposlen == null)
            {
                return NotFound();
            }

            return zaposlen;
        }

        // PUT: api/ZaposlenApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutZaposlen(int id, Zaposlen zaposlen)
        {
            if (id != zaposlen.ID)
            {
                return BadRequest();
            }

            _context.Entry(zaposlen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZaposlenExists(id))
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

        // POST: api/ZaposlenApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Zaposlen>> PostZaposlen(Zaposlen zaposlen)
        {
            _context.Zaposleni.Add(zaposlen);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetZaposlen", new { id = zaposlen.ID }, zaposlen);
        }

        // DELETE: api/ZaposlenApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Zaposlen>> DeleteZaposlen(int id)
        {
            var zaposlen = await _context.Zaposleni.FindAsync(id);
            if (zaposlen == null)
            {
                return NotFound();
            }

            _context.Zaposleni.Remove(zaposlen);
            await _context.SaveChangesAsync();

            return zaposlen;
        }

        private bool ZaposlenExists(int id)
        {
            return _context.Zaposleni.Any(e => e.ID == id);
        }
    }
}
