using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebPharmacy.ViewModels
{
    public class MedicamentTypeModel
    {
        public int? Id { get; set; }
        [Required]
        [Display(Name = "Название типа лекарства")]
        public string Name { get; set; }

    }
}
