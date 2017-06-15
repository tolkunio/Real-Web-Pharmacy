using System;
using System.Collections.Generic;

namespace WebPharmacy.Models
{
    public partial class MedicamentType
    {
        public MedicamentType()
        {
            Medicament = new HashSet<Medicament>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Medicament> Medicament { get; set; }
    }
}
