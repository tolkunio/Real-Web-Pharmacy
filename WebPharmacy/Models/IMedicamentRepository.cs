using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPharmacy.Models
{
   public interface IMedicamentRepository
    {
        IEnumerable<Medicament> Medicaments { get; }
    }
}
