using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPharmacy.Models;

namespace WebPharmacy.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Medicament> Medicaments { get; set; }
    }
}
