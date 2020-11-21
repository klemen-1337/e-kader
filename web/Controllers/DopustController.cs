using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace web.Controllers
{
    [Authorize]
    public class DopustController : Controller
    {
        private readonly EkadriContext _context;

        private readonly UserManager<Uporabniki> _usermanager;

        public DopustController(EkadriContext context, UserManager<Uporabniki> userManager)
        {
            _context = context;
            _usermanager = userManager;
        }

        // GET: Dopust
        public async Task<IActionResult> Index()
        {
            var dopust =  _context.Dopusti
                .Include(c => c.Uporabnik.Zaposlen)
                .AsNoTracking();
            return View(await dopust.ToListAsync());
        }

        // GET: Dopust/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dopust = await _context.Dopusti
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dopust == null)
            {
                return NotFound();
            }

            return View(dopust);
        }

        // GET: Dopust/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dopust/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Datum,UraZacetka,UraKonca")] Dopust dopust)
        {
            var currentUser = await _usermanager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                dopust.Uporabnik = currentUser;
                _context.Add(dopust);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dopust);
        }

        // GET: Dopust/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dopust = await _context.Dopusti.FindAsync(id);
            if (dopust == null)
            {
                return NotFound();
            }
            return View(dopust);
        }

        // POST: Dopust/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Datum,UraZacetka,UraKonca")] Dopust dopust)
        {
            if (id != dopust.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dopust);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DopustExists(dopust.ID))
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
            return View(dopust);
        }

        // GET: Dopust/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dopust = await _context.Dopusti
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dopust == null)
            {
                return NotFound();
            }

            return View(dopust);
        }

        // POST: Dopust/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dopust = await _context.Dopusti.FindAsync(id);
            _context.Dopusti.Remove(dopust);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DopustExists(int id)
        {
            return _context.Dopusti.Any(e => e.ID == id);
        }
    }
}
