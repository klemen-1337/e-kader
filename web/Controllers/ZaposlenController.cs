using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;

namespace web.Controllers
{
    public class ZaposlenController : Controller
    {
        private readonly EkadriContext _context;

        public ZaposlenController(EkadriContext context)
        {
            _context = context;
        }

        // GET: Zaposlen
        public async Task<IActionResult> Index()
        {
            return View(await _context.Zaposleni.ToListAsync());
        }

        // GET: Zaposlen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposlen = await _context.Zaposleni
                .FirstOrDefaultAsync(m => m.ID == id);
            if (zaposlen == null)
            {
                return NotFound();
            }

            return View(zaposlen);
        }

        // GET: Zaposlen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zaposlen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Ime,Priimek,Naslov,Telefon,DatumRojstva,DatumZaposlitve,Spol")] Zaposlen zaposlen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zaposlen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zaposlen);
        }

        // GET: Zaposlen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposlen = await _context.Zaposleni.FindAsync(id);
            if (zaposlen == null)
            {
                return NotFound();
            }
            return View(zaposlen);
        }

        // POST: Zaposlen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Ime,Priimek,Naslov,Telefon,DatumRojstva,DatumZaposlitve,Spol")] Zaposlen zaposlen)
        {
            if (id != zaposlen.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zaposlen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZaposlenExists(zaposlen.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(zaposlen);
        }

        // GET: Zaposlen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposlen = await _context.Zaposleni
                .FirstOrDefaultAsync(m => m.ID == id);
            if (zaposlen == null)
            {
                return NotFound();
            }

            return View(zaposlen);
        }

        // POST: Zaposlen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zaposlen = await _context.Zaposleni.FindAsync(id);
            _context.Zaposleni.Remove(zaposlen);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZaposlenExists(int id)
        {
            return _context.Zaposleni.Any(e => e.ID == id);
        }
    }
}
