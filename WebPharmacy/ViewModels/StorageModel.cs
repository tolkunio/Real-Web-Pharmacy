using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebPharmacy.Models;

namespace WebPharmacy.ViewModels
{
    public class StorageModel
    {
        public int StorageId { get; set; }

        [Required]
        [Display(Name = "Лекарства")]
        [MaxLength(255)]
        public int MedicamentId { get; set; }

        [Required]
        [Display(Name = "Количество")]
        public int Count { get; set; }

        public virtual Medicament Medicament { get; set; }
    }
}
