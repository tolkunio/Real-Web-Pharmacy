using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebPharmacy.Models
{
    public partial class Outcoming
    {
        public int Id { get; set; }
        [Display(Name="Медикамент")]
        public int MedicamentId { get; set; }
        [Display(Name ="Количество")]
        public int Count { get; set; }
        [Display(Name ="Цена")]
        public decimal Price { get; set; }
        [Display(Name ="Дата продажи")]
        public DateTime OutcomedAt { get; set; }

        public virtual Medicament Medicament { get; set; }
    }
}
