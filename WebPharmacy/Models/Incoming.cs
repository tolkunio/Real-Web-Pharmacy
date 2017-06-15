using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebPharmacy.Models
{
    public partial class Incoming
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Медикамент")]
        public int MedicamentId { get; set; }
        [Required]
        [Display(Name ="Количество")]
        public int Count { get; set; }
        [Required]
        [Display(Name ="Цена")]
        public decimal Price { get; set; }
        [Required]
        [Display(Name="Дата поступление")]
        public DateTime IncomedAt { get; set; }

        public virtual Medicament Medicament { get; set; }
    }
}
