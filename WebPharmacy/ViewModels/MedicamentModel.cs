using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebPharmacy.Models;

namespace WebPharmacy.ViewModels
{
    public class MedicamentModel
    {
        public int? MedicamentId { get; set; }
 
        [Required]
        [MaxLength(255)]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        [Display(Name = "Показание к применению")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Производитель")]
        public int FirmId { get; set; }

        [Display(Name ="Изображение")]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Тип лекарства")]
        public int MedicamentTypeId { get; set; }

        [Required]
        [Display(Name = "Форма выпуска")]
        public int FormulationId { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Срок годности")]
        public DateTime ExpirationDate { get; set; }
        public IEnumerable<Medicament> Medicaments { get; set; }
        public virtual Firm Firm { get; set; }
        public virtual Formulation Formulation { get; set; }
        public virtual MedicamentType MedicamentType { get; set; }
    }
}
