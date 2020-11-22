using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using web.Data;
using web.Models;

namespace web.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class ZaposlitveController : Controller
    {
        private readonly EkadriContext _context;

        public ZaposlitveController(EkadriContext context)
        {
            _context = context;
        }

        // GET: Zaposlitve
        public async Task<IActionResult> Index()
        {
            var ekadriContext = _context.Zaposlitve.Include(z => z.DelovnaMesta).Include(z => z.Zaposlen);
            return View(await ekadriContext.ToListAsync());
        }

        // GET: Zaposlitve/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposlitve = await _context.Zaposlitve
                .Include(z => z.DelovnaMesta)
                .Include(z => z.Zaposlen)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (zaposlitve == null)
            {
                return NotFound();
            }

            return View(zaposlitve);
        }

        // GET: Zaposlitve/Create
        public IActionResult Create()
        {
            ViewData["DelovnaMestaID"] = new SelectList(_context.DelovnaMesta, "DelovnaMestaID", "DelovnaMestaID");
            ViewData["ZaposlenID"] = new SelectList(_context.Zaposleni, "ID", "ID");
            return View();
        }

        // POST: Zaposlitve/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ZaposlenID,DelovnaMestaID,DatumZaposlitve")] Zaposlitve zaposlitve)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zaposlitve);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DelovnaMestaID"] = new SelectList(_context.DelovnaMesta, "DelovnaMestaID", "DelovnaMestaID", zaposlitve.DelovnaMestaID);
            ViewData["ZaposlenID"] = new SelectList(_context.Zaposleni, "ID", "ID", zaposlitve.ZaposlenID);
            return View(zaposlitve);
        }

        // GET: Zaposlitve/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposlitve = await _context.Zaposlitve.FindAsync(id);
            if (zaposlitve == null)
            {
                return NotFound();
            }
            ViewData["DelovnaMestaID"] = new SelectList(_context.DelovnaMesta, "DelovnaMestaID", "DelovnaMestaID", zaposlitve.DelovnaMestaID);
            ViewData["ZaposlenID"] = new SelectList(_context.Zaposleni, "ID", "ID", zaposlitve.ZaposlenID);
            return View(zaposlitve);
        }

        // POST: Zaposlitve/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ZaposlenID,DelovnaMestaID,DatumZaposlitve")] Zaposlitve zaposlitve)
        {
            if (id != zaposlitve.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zaposlitve);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZaposlitveExists(zaposlitve.ID))
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
            ViewData["DelovnaMestaID"] = new SelectList(_context.DelovnaMesta, "DelovnaMestaID", "DelovnaMestaID", zaposlitve.DelovnaMestaID);
            ViewData["ZaposlenID"] = new SelectList(_context.Zaposleni, "ID", "ID", zaposlitve.ZaposlenID);
            return View(zaposlitve);
        }

        // GET: Zaposlitve/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposlitve = await _context.Zaposlitve
                .Include(z => z.DelovnaMesta)
                .Include(z => z.Zaposlen)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (zaposlitve == null)
            {
                return NotFound();
            }

            return View(zaposlitve);
        }

        // POST: Zaposlitve/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zaposlitve = await _context.Zaposlitve.FindAsync(id);
            _context.Zaposlitve.Remove(zaposlitve);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZaposlitveExists(int id)
        {
            return _context.Zaposlitve.Any(e => e.ID == id);
        }
    }
}
