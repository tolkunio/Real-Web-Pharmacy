using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebPharmacy.Data;
using WebPharmacy.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebPharmacy.Controllers
{
    public class IncomingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IncomingsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Incomings
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Incoming.Include(i => i.Medicament);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Incomings/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["MedicamentId"] = new SelectList(_context.Medicament, "MedicamentId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MedicamentId,Count,Price,IncomedAt")] Incoming incoming)
        {
            if (ModelState.IsValid)
            {
                _context.Add(incoming);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["MedicamentId"] = new SelectList(_context.Medicament, "MedicamentId", "Name", incoming.MedicamentId);
            return View(incoming);
        }
        [Authorize]
        // GET: Incomings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incoming = await _context.Incoming.SingleOrDefaultAsync(m => m.Id == id);
            if (incoming == null)
            {
                return NotFound();
            }
            ViewData["MedicamentId"] = new SelectList(_context.Medicament, "MedicamentId", "Name", incoming.MedicamentId);
            return View(incoming);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MedicamentId,Count,Price,IncomedAt")] Incoming incoming)
        {
            if (id != incoming.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(incoming);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncomingExists(incoming.Id))
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
            ViewData["MedicamentId"] = new SelectList(_context.Medicament, "MedicamentId", "Name", incoming.MedicamentId);
            return View(incoming);
        }
        [Authorize]
        // GET: Incomings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incoming = await _context.Incoming
                .Include(i => i.Medicament)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (incoming == null)
            {
                return NotFound();
            }

            return View(incoming);
        }

        // POST: Incomings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var incoming = await _context.Incoming.SingleOrDefaultAsync(m => m.Id == id);
            _context.Incoming.Remove(incoming);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool IncomingExists(int id)
        {
            return _context.Incoming.Any(e => e.Id == id);
        }
    }
}
