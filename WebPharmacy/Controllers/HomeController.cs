using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebPharmacy.Data;
using WebPharmacy.ViewModels;
using ReflectionIT.Mvc.Paging;
using WebPharmacy.Models;
using Microsoft.EntityFrameworkCore;

namespace WebPharmacy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;
        public int pageSize = 3;
        public HomeController(ApplicationDbContext _context)
        {
            context = _context;
        }
        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                Medicaments = context.Medicament
            };
            return View(homeViewModel);

        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
