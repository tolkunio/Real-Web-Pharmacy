using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebPharmacy.ViewModels
{
    public class FormulationModel
    {
        public int? Id { get; set; }
        [Required]
        [Display(Name = "Название форма выпуска")]
        [MaxLength(255)]
        public string Name { get; set; }

    }
}
