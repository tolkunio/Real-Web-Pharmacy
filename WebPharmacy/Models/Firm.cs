using System;
using System.Collections.Generic;

namespace WebPharmacy.Models
{
    public partial class Firm
    {
        public Firm()
        {
            Medicament = new HashSet<Medicament>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Medicament> Medicament { get; set; }
    }
}
