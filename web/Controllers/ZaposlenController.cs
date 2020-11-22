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
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Identity;

namespace web.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class ZaposlenController : Controller
    {
        private readonly EkadriContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ZaposlenController(EkadriContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Zaposlen
        public async Task<IActionResult> Index( string searchString,                                                
                                                string sortOrder,
                                                string currentFilter)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;
            
            var zaposleni = from z in _context.Zaposleni
                            select z;
                            
            if (!String.IsNullOrEmpty(searchString))
            {
                zaposleni = zaposleni.Where(z => z.Priimek.Contains(searchString)
                                        || z.Ime.Contains(searchString)
                                        || z.Naslov.Contains(searchString)
                                        || z.Spol.Contains(searchString)
                                        || z.ID.ToString().Contains(searchString)
                                        );
            }
            //return View(await _context.Zaposleni.ToListAsync());
            return View(await zaposleni.AsNoTracking().ToListAsync());
        }

        public IActionResult New()  
        {  
            return View();  
        }  
  
        [HttpPost]  
        [ValidateAntiForgeryToken]  
        private string UploadedFile(ZaposlenViewModel model)  
        {  
            string uniqueFileName = null;  
  
            if (model.Slika != null)  
            {  
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images");  
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Slika.FileName;  
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);  
                using (var fileStream = new FileStream(filePath, FileMode.Create))  
                {  
                    model.Slika.CopyTo(fileStream);  
                }  
            }  
            return uniqueFileName;  
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
        public async Task<IActionResult> Create(ZaposlenViewModel model)
        {
            if (ModelState.IsValid)  
            {                
                await _context.SaveChangesAsync();  
                return RedirectToAction(nameof(Index));  
            }  
            return View();  
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
         public async Task<IActionResult> Edit(int id, [Bind("ID,Ime,Priimek,Naslov,Telefon,DatumRojstva,DatumZaposlitve,Spol,Kadrovanje")] Zaposlen zaposlen)
        {
            if (id != zaposlen.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {   IdentityRole role;
                    if(zaposlen.Kadrovanje){
                        role = await _context.Roles.FirstOrDefaultAsync(m => m.Name == "Manager");
                        
                    }else{
                       role = await _context.Roles.FirstOrDefaultAsync(m => m.Name == "Worker");
                    }
                    var user = await _context.Users.FirstOrDefaultAsync(m => m.Zaposlen.ID == id);
                    var userRole = await _context.UserRoles.FirstOrDefaultAsync(m => m.UserId == user.Id);
                    if(userRole != null){
                        _context.UserRoles.Remove(userRole);
                    }
                    var userRoleNew = new IdentityUserRole<string>{UserId = user.Id,RoleId = role.Id};
                    _context.UserRoles.Add(userRoleNew);
                        

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
            var uporabnik = await _context.Users
                .FirstOrDefaultAsync(b => b.Zaposlen.ID == id);
            
            if (uporabnik != null){
                _context.Users.Remove(uporabnik);
            }
            
            string path = zaposlen.PhotoPath;

            if(path != null){         
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images");
                string filePath = Path.Combine(uploadsFolder, path);
                Console.WriteLine(path);
                if(System.IO.File.Exists(filePath)){
                    System.IO.File.Delete(filePath);
                }
            }
            
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
