using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebPharmacy.Models;
using ReflectionIT.Mvc.Paging;
using WebPharmacy.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Net.Http.Headers;
using WebPharmacy.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace WebPharmacy.Controllers
{
    public class MedicamentController : Controller
    {
        private readonly IHostingEnvironment _enviroment;
        private ApplicationDbContext context;
        public int pageSize = 6;

        public MedicamentController(ApplicationDbContext context, IHostingEnvironment enviroment)
        {
            this.context = context;
            _enviroment = enviroment;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index(string filter, int page = 1, string sortExpression = "Name")
        {
            // IEnumerable<Medicament> medicaments;
            var qry = context.Medicament
                .AsNoTracking().
                Include(p => p.MedicamentType)
                .Include(p => p.Formulation)
                .Include(p => p.Firm)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                qry = qry.Where(p => p.Name.Contains(filter));
            }
            var model = await PagingList<Medicament>.CreateAsync(qry, pageSize, page, sortExpression, "Name");
            model.RouteValue = new RouteValueDictionary
            {
                 { "filter", filter}
            };
            return View(model);
        }
        public IActionResult List() => View("List");
        public IActionResult InformList() => View("InformList");

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Firms = new SelectList(context.Firm, "Id", "Name");
            ViewBag.MedicamentTypes = new SelectList(context.MedicamentType, "Id", "Name");
            ViewBag.Formulations = new SelectList(context.Formulation, "Id", "Name");
            return View(new MedicamentModel { });
        }
        [HttpPost]
        public IActionResult Create(MedicamentModel model)
        {

            if (ModelState.IsValid)
            {
                DownloadImage(model);
                context.Medicament.Add(new Medicament
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    ExpirationDate = model.ExpirationDate,
                    FirmId = model.FirmId,
                    FormulationId = model.FormulationId,
                    MedicamentTypeId = model.MedicamentTypeId,
                    ImageUrl = model.ImageUrl

                });
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError(string.Empty, "Ошибка при внесении в бд.");
                    ViewBag.Firms = new SelectList(context.Firm, "Id", "Name");
                    ViewBag.MedicamentTypes = new SelectList(context.MedicamentType, "Id", "Name");
                    ViewBag.Formulations = new SelectList(context.Formulation, "Id", "Name");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = context.Medicament.FirstOrDefault(x => x.MedicamentId == id);
            if (model == null)
            {
                return NotFound();
            }
            ViewBag.Firms = new SelectList(context.Firm, "Id", "Name");
            ViewBag.MedicamentTypes = new SelectList(context.MedicamentType, "Id", "Name");
            ViewBag.Formulations = new SelectList(context.Formulation, "Id", "Name");
            return View(new MedicamentModel
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                ExpirationDate = model.ExpirationDate,
                FirmId = model.FirmId,
                FormulationId = model.FormulationId,
                MedicamentTypeId = model.MedicamentTypeId,
                ImageUrl = model.ImageUrl

            });
        }
        [HttpPost]
        public IActionResult Edit(int id, MedicamentModel medicament)
        {
            if (ModelState.IsValid)
            {
                DownloadImage(medicament);
                Medicament med = context.Medicament.FirstOrDefault(x => x.MedicamentId == id);
                if (med == null)
                {
                    return NotFound();
                }
                med.Name = medicament.Name;
                med.Description = medicament.Description;
                med.Price = medicament.Price;
                med.ExpirationDate = medicament.ExpirationDate;
                med.FirmId = medicament.FirmId;
                med.FormulationId = medicament.FormulationId;
                med.MedicamentTypeId = medicament.MedicamentTypeId;
                med.ImageUrl = medicament.ImageUrl;
                context.Medicament.Update(med);
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    //ModelState.AddModelError(string.Empty, "Название типа лекарственных средств должна быть уникальной");
                    ViewBag.Firms = new SelectList(context.Firm, "Id", "Name");
                    ViewBag.MedicamentTypes = new SelectList(context.MedicamentType, "Id", "Name");
                    ViewBag.Formulations = new SelectList(context.Formulation, "Id", "Name");
                    return View(medicament);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(medicament);
        }
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var model = context.Medicament.FirstOrDefault(x => x.MedicamentId == id);
            if (model == null)
            {
                return NotFound();
            }
            context.Medicament.Remove(model);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var medicament = context.Medicament
                .Include(x => x.Firm)
                .Include(x => x.Formulation)
                .Include(x => x.MedicamentType)
            .SingleOrDefault(m => m.MedicamentId == id);

            return View(medicament);
        }

        public void DownloadImage(MedicamentModel model)
        {

            var files = HttpContext.Request.Form.Files;
            foreach (var image in files)
            {
                if (image != null && image.Length > 0)
                {
                    var file = image;
                    var uploadPath = Path.Combine(_enviroment.WebRootPath, "uploads");
                    Directory.CreateDirectory(Path.Combine(uploadPath));
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();
                        using (var fileStream = new FileStream(Path.Combine(uploadPath, file.FileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                            model.ImageUrl = file.FileName;
                        }
                    }

                }
            }
        }
    }

}
