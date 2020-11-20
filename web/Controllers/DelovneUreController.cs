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
    public class DelovneUreController : Controller
    {
        private readonly EkadriContext _context;
        private readonly UserManager<Uporabniki> _usermanager;

        public DelovneUreController(EkadriContext context, UserManager<Uporabniki> userManager)
        {
            _context = context;
            _usermanager = userManager;
        }

        // GET: DelovneUre
        public async Task<IActionResult> Index()
        {
            var delovneUre =  _context.DelovneUre
                .Include(c => c.Uporabnik.Zaposlen)
                .AsNoTracking();
            return View(await delovneUre.ToListAsync());
        }

        // GET: DelovneUre/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delovneUre = await _context.DelovneUre
                .FirstOrDefaultAsync(m => m.ID == id);
            if (delovneUre == null)
            {
                return NotFound();
            }

            return View(delovneUre);
        }

        // GET: DelovneUre/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DelovneUre/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Datum,UraZacetka,UraKonca")] DelovneUre delovneUre)
        {
            var currentUser = await _usermanager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                delovneUre.Uporabnik = currentUser;
                _context.Add(delovneUre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(delovneUre);
        }

        // GET: DelovneUre/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delovneUre = await _context.DelovneUre.FindAsync(id);
            if (delovneUre == null)
            {
                return NotFound();
            }
            return View(delovneUre);
        }

        // POST: DelovneUre/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Datum,UraZacetka,UraKonca")] DelovneUre delovneUre)
        {
            if (id != delovneUre.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(delovneUre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DelovneUreExists(delovneUre.ID))
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
            return View(delovneUre);
        }

        // GET: DelovneUre/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delovneUre = await _context.DelovneUre
                .FirstOrDefaultAsync(m => m.ID == id);
            if (delovneUre == null)
            {
                return NotFound();
            }

            return View(delovneUre);
        }

        // POST: DelovneUre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var delovneUre = await _context.DelovneUre.FindAsync(id);
            _context.DelovneUre.Remove(delovneUre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DelovneUreExists(int id)
        {
            return _context.DelovneUre.Any(e => e.ID == id);
        }
    }
}
