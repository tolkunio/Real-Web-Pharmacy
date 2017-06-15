using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebPharmacy.Data;
using WebPharmacy.Models;

namespace WebPharmacy.Controllers
{
    public class OutcomingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OutcomingsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Outcomings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Outcoming.Include(o => o.Medicament);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Outcomings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outcoming = await _context.Outcoming
                .Include(o => o.Medicament)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (outcoming == null)
            {
                return NotFound();
            }

            return View(outcoming);
        }

        // GET: Outcomings/Create
        public IActionResult Create()
        {
            ViewData["MedicamentId"] = new SelectList(_context.Medicament, "MedicamentId", "Name");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MedicamentId,Count,Price,OutcomedAt")] Outcoming outcoming)
        {
            if (ModelState.IsValid)
            {
                _context.Add(outcoming);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["MedicamentId"] = new SelectList(_context.Medicament, "MedicamentId", "Name", outcoming.MedicamentId);
            return View(outcoming);
        }

        // GET: Outcomings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outcoming = await _context.Outcoming.SingleOrDefaultAsync(m => m.Id == id);
            if (outcoming == null)
            {
                return NotFound();
            }
            ViewData["MedicamentId"] = new SelectList(_context.Medicament, "MedicamentId", "Name", outcoming.MedicamentId);
            return View(outcoming);
        }

        // POST: Outcomings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MedicamentId,Count,Price,OutcomedAt")] Outcoming outcoming)
        {
            if (id != outcoming.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(outcoming);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OutcomingExists(outcoming.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["MedicamentId"] = new SelectList(_context.Medicament, "MedicamentId", "Name", outcoming.MedicamentId);
            return View(outcoming);
        }

        // GET: Outcomings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var outcoming = await _context.Outcoming
                .Include(o => o.Medicament)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (outcoming == null)
            {
                return NotFound();
            }

            return View(outcoming);
        }

        // POST: Outcomings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var outcoming = await _context.Outcoming.SingleOrDefaultAsync(m => m.Id == id);
            _context.Outcoming.Remove(outcoming);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool OutcomingExists(int id)
        {
            return _context.Outcoming.Any(e => e.Id == id);
        }
    }
}
