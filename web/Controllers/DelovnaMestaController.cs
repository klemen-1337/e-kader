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

namespace web.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class DelovnaMestaController : Controller
    {
        private readonly EkadriContext _context;

        public DelovnaMestaController(EkadriContext context)
        {
            _context = context;
        }

        // GET: DelovnaMesta
        public async Task<IActionResult> Index()
        {
            return View(await _context.DelovnaMesta.ToListAsync());
        }

        // GET: DelovnaMesta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delovnaMesta = await _context.DelovnaMesta
                .FirstOrDefaultAsync(m => m.DelovnaMestaID == id);
            if (delovnaMesta == null)
            {
                return NotFound();
            }

            return View(delovnaMesta);
        }

        // GET: DelovnaMesta/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DelovnaMesta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DelovnaMestaID,Oddelek,Lokacija,NazivDelovnegaMesta")] DelovnaMesta delovnaMesta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(delovnaMesta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(delovnaMesta);
        }

        // GET: DelovnaMesta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delovnaMesta = await _context.DelovnaMesta.FindAsync(id);
            if (delovnaMesta == null)
            {
                return NotFound();
            }
            return View(delovnaMesta);
        }

        // POST: DelovnaMesta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DelovnaMestaID,Oddelek,Lokacija,NazivDelovnegaMesta")] DelovnaMesta delovnaMesta)
        {
            if (id != delovnaMesta.DelovnaMestaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(delovnaMesta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DelovnaMestaExists(delovnaMesta.DelovnaMestaID))
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
            return View(delovnaMesta);
        }

        // GET: DelovnaMesta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delovnaMesta = await _context.DelovnaMesta
                .FirstOrDefaultAsync(m => m.DelovnaMestaID == id);
            if (delovnaMesta == null)
            {
                return NotFound();
            }

            return View(delovnaMesta);
        }

        // POST: DelovnaMesta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var delovnaMesta = await _context.DelovnaMesta.FindAsync(id);
            _context.DelovnaMesta.Remove(delovnaMesta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DelovnaMestaExists(int id)
        {
            return _context.DelovnaMesta.Any(e => e.DelovnaMestaID == id);
        }
    }
}
