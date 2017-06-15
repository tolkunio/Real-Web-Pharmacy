using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebPharmacy.Data;
using WebPharmacy.ViewModels;
using WebPharmacy.Models;
using Microsoft.EntityFrameworkCore;

namespace WebPharmacy.Controllers
{
    public class MedicamentTypeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MedicamentTypeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var medicaments = from item in _context.MedicamentType
                              orderby item.Name
                              select new MedicamentTypeModel
                              {
                                  Id = item.Id,
                                  Name = item.Name
                              };
            return View(medicaments);
        }

        public IActionResult Create()
        {
            return View(new MedicamentTypeModel { });
        }

        [HttpPost]
        public IActionResult Create(MedicamentTypeModel model)
        {
            if (ModelState.IsValid)
            {
                _context.MedicamentType.Add(new MedicamentType
                {
                    Name = model.Name
                });
                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError(string.Empty, "Название типа лекарственных средств должна быть уникальной");
                    return View(model);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _context.MedicamentType.FirstOrDefault(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            ViewBag.Id = id;
            return View(new MedicamentTypeModel
            {
                Name = model.Name
            });
        }
        [HttpPost]
        public IActionResult Edit(int id, MedicamentTypeModel model)
        {
            if (ModelState.IsValid)
            {
                MedicamentType medicamentType = _context.MedicamentType.FirstOrDefault(x => x.Id == id);
                if (medicamentType == null)
                {
                    return NotFound();
                }
                medicamentType.Name = model.Name;
                _context.MedicamentType.Update(medicamentType);
                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    ViewBag.Id = id;
                    ModelState.AddModelError(String.Empty, "Название типа лекарственных средств должна быть уникальной");
                    return View(model);

                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult Delete(int Id)
        {
            var model = _context.MedicamentType.FirstOrDefault(x => x.Id == Id);
            if (model == null)
            {
                return NotFound();
            }
            _context.MedicamentType.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}