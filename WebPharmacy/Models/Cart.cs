using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPharmacy.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        public virtual void AddItem(Medicament medicament, int quantity)
        {
            CartLine line = lineCollection
            .Where(p => p.Medicament.MedicamentId == medicament.MedicamentId)
            .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Medicament = medicament,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public virtual void RemoveLine(Medicament medicament) =>
        lineCollection.RemoveAll(l => l.Medicament.MedicamentId == medicament.MedicamentId);

        public virtual decimal ComputeTotalValue() => lineCollection.Sum(e => e.Medicament.Price * e.Quantity);
        public virtual void Clear() => lineCollection.Clear();

        public virtual IEnumerable<CartLine> Lines => lineCollection;
    }
    public class CartLine
    {
        public int CartLineID { get; set; }
        public Medicament Medicament { get; set; }
        public int Quantity { get; set; }
    }
}
