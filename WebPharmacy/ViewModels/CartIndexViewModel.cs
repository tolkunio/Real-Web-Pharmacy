using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPharmacy.Models;

namespace WebPharmacy.ViewModels
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}
