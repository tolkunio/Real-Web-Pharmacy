using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebPharmacy.Models
{
    public partial class Medicament
    {
        public Medicament()
        {
            Incoming = new HashSet<Incoming>();
            Outcoming = new HashSet<Outcoming>();
            Storage = new HashSet<Storage>();
        }

        public int MedicamentId { get; set; }
        [Display(Name="Название")]
        public string Name { get; set; }
        [Display(Name="Показание")]
        public string Description { get; set; }
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        [Display(Name = "Производитель")]
        public int FirmId { get; set; }
        [Display(Name = "Изображение")]
        public string ImageUrl { get; set; }
        [Display(Name = "Срок годности")]
        public DateTime ExpirationDate { get; set; }
        [Display(Name = "Тип медикамента")]
        public int MedicamentTypeId { get; set; }
        [Display(Name = "Форма выпуска")]
        public int FormulationId { get; set; }

        public virtual ICollection<Incoming> Incoming { get; set; }
        public virtual ICollection<Outcoming> Outcoming { get; set; }
        public virtual ICollection<Storage> Storage { get; set; }
        public virtual Firm Firm { get; set; }
        public virtual Formulation Formulation { get; set; }
        public virtual MedicamentType MedicamentType { get; set; }
    }
}
