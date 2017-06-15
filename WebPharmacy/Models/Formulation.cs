using System;
using System.Collections.Generic;

namespace WebPharmacy.Models
{
    public partial class Formulation
    {
        public Formulation()
        {
            Medicament = new HashSet<Medicament>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Medicament> Medicament { get; set; }
    }
}
