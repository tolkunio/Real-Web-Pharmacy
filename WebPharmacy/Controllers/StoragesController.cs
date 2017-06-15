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
    public class StoragesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StoragesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Storages1
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Storage.Include(s => s.Medicament);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Storages1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storage = await _context.Storage
                .Include(s => s.Medicament)
                .SingleOrDefaultAsync(m => m.StorageId == id);
            if (storage == null)
            {
                return NotFound();
            }

            return View(storage);
        }

        // GET: Storages1/Create
        public IActionResult Create()
        {
            ViewData["MedicamentId"] = new SelectList(_context.Medicament, "MedicamentId", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StorageId,MedicamentId,Count")] Storage storage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(storage);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["MedicamentId"] = new SelectList(_context.Medicament, "MedicamentId", "Name", storage.MedicamentId);
            return View(storage);
        }

        // GET: Storages1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storage = await _context.Storage.SingleOrDefaultAsync(m => m.StorageId == id);
            if (storage == null)
            {
                return NotFound();
            }
            ViewData["MedicamentId"] = new SelectList(_context.Medicament, "MedicamentId", "Name", storage.MedicamentId);
            return View(storage);
        }

        // POST: Storages1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StorageId,MedicamentId,Count")] Storage storage)
        {
            if (id != storage.StorageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StorageExists(storage.StorageId))
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
            ViewData["MedicamentId"] = new SelectList(_context.Medicament, "MedicamentId", "Name", storage.MedicamentId);
            return View(storage);
        }

        // GET: Storages1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storage = await _context.Storage
                .Include(s => s.Medicament)
                .SingleOrDefaultAsync(m => m.StorageId == id);
            if (storage == null)
            {
                return NotFound();
            }

            return View(storage);
        }

        // POST: Storages1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var storage = await _context.Storage.SingleOrDefaultAsync(m => m.StorageId == id);
            _context.Storage.Remove(storage);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool StorageExists(int id)
        {
            return _context.Storage.Any(e => e.StorageId == id);
        }
    }
}
