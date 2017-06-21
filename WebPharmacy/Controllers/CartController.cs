using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebPharmacy.Data;
using WebPharmacy.Models;
using WebPharmacy.Infrastructure;
using WebPharmacy.ViewModels;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebPharmacy.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _context;
        private Cart cart;

        public CartController(ApplicationDbContext context,Cart _cart)
        {
            _context = context;
            cart = _cart;
        }
        [Authorize]
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }
        [Authorize]
        public RedirectToActionResult AddToCart(int medicamentId, string returnUrl)
        {
            Medicament medicament = _context.Medicament
            .FirstOrDefault(p => p.MedicamentId == medicamentId);
            if (medicament != null)
            {
                cart.AddItem(medicament, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        [Authorize]
        public RedirectToActionResult RemoveFromCart(int medicamentId, string returnUrl)
        {
            Medicament product = _context.Medicament
            .FirstOrDefault(p => p.MedicamentId == medicamentId);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }
        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
    }
}
