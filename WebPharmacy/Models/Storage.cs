using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebPharmacy.Models
{
    public partial class Storage
    {
        public int StorageId { get; set; }
        [Display(Name = "Лекарство")]
        public int MedicamentId { get; set; }

        [Display(Name ="Количество")]
        public int Count { get; set; }

        public virtual Medicament Medicament { get; set; }
    }
}
