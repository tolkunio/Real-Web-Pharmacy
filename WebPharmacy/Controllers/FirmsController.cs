using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebPharmacy.Data;
using WebPharmacy.ViewModels;
using WebPharmacy.Models;

namespace WebPharmacy.Controllers
{
    public class FirmsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FirmsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Firms
        public  IActionResult Index()
        {
            return View( _context.Firm.Select(x=>new FirmModel
            {
                Name=x.Name,
                Country=x.Country,
                Id=x.Id
            }));
        }
        // GET: Firms/Create
        public IActionResult Create()
        {
            return View(new FirmModel { });
        }

        // POST: Firms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create([Bind("Id,Name,Country")] FirmModel firm)
        {
            if (ModelState.IsValid)
            {
                _context.Firm.Add(new Firm
                {
                    Name=firm.Name,
                    Country=firm.Country
                });
                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError(string.Empty, "Название Фирмы должна быть уникальной");
                    return View(firm);
                }
                return RedirectToAction("Index");
            }
            return View(firm);
        }

        // GET: Firms/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _context.Firm.FirstOrDefault(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            ViewBag.Id = id;
            return View(new FirmModel
            {
                Country = model.Country,
                Name = model.Name
            });
        }

        [HttpPost]
        public ActionResult Edit(int id, FirmModel model)
        {
            if (ModelState.IsValid)
            {
                Firm firm = _context.Firm.FirstOrDefault(x => x.Id == id);
                if (firm == null)
                {
                    return NotFound();
                }
                firm.Name = model.Name;
                firm.Country = model.Country;
                _context.Firm.Update(firm);

                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError(string.Empty, "Название Фирмы должна быть уникальной");
                    ViewBag.Id = id;
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var model = _context.Firm.FirstOrDefault(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            _context.Firm.Remove(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
