using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebPharmacy.ViewModels
{
    public class FirmModel
    {
        public int ? Id { get; set; }
        [Required]
        [Display(Name = "Фирма")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Страна")]
        [MaxLength(255)]
        public string Country { get; set; }
    }
}
