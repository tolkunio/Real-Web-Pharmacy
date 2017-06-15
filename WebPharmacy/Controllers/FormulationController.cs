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
    public class FormulationController : Controller
    {
        ApplicationDbContext _context;
        public FormulationController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Formulation.Select(x => new FormulationModel
            {
                Id = x.Id,
                Name = x.Name
            }));
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new FormulationModel { });
        }
        [HttpPost]
        public IActionResult Create(FormulationModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Formulation.Add(new Formulation
                {
                    Name = model.Name
                });
                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError(string.Empty, "Ќазвание форма выпуска должна быть уникальной");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Formulation formulation = _context.Formulation.FirstOrDefault(x => x.Id == id);
            if (formulation == null)
            {
                return NotFound();
            }
            ViewBag.Id = id;
            return View(new FormulationModel
            {
                Name = formulation.Name
            });
        }

        [HttpPost]
        public IActionResult Edit(int id, FormulationModel model)
        {
            if (ModelState.IsValid)
            {
                Formulation formulation = _context.Formulation.FirstOrDefault(x => x.Id == id);
                if (formulation == null)
                {
                    return NotFound();
                }
                formulation.Name = model.Name;
                _context.Formulation.Update(formulation);
                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError(string.Empty, "Ќазвание форма выпуска должна быть уникальной");
                    return View(model);
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var model = _context.Formulation.FirstOrDefault(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            _context.Formulation.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}