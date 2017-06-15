using System;
using System.Linq;
using WebPharmacy.Models;
using Xunit;

namespace WebPharmacy.Tests
{
    public class CartTests
    {
       [Fact]
        public void Can_Add_New_Lines()
        {
            //Arrange-  create some test products
            Medicament m1 = new Medicament { MedicamentId=1,Name="M1"};
            Medicament m2 = new Medicament { MedicamentId = 2, Name = "M2" };
            //Arrange- create new cart
            Cart target = new Cart();
            //Act
            target.AddItem(m1, 1);
            target.AddItem(m2, 1);
            CartLine[] results = target.Lines.ToArray();
            Assert.Equal(2, results.Length);
            Assert.Equal(m1, results[0].Medicament);
            Assert.Equal(m2, results[1].Medicament);

        }
        [Fact]
        public void Can_Remove_Line()
        {
            //Arrange-create some test products
            Medicament m1 = new Medicament { MedicamentId = 1, Name = "M1" };
            Medicament m2 = new Medicament { MedicamentId = 2, Name = "M2" };
            Medicament m3 = new Medicament { MedicamentId = 2, Name = "M3" };
            //Arrange -create a new cart
            Cart target = new Cart();
            //Arrange-add some products to the cart
            target.AddItem(m1, 1);
            target.AddItem(m2, 1);
            target.AddItem(m3, 1);
            //Act
            target.RemoveLine(m2);
            //Assert
            Assert.Equal(0, target.Lines.Where(c => c.Medicament == m2).Count());
            Assert.Equal(2, target.Lines.Count());

           }

        [Fact]
        public void Calculate_Cart_Total()
        {
            // Arrange - create some test products
            Medicament p1 = new Medicament { MedicamentId = 1, Name = "M1", Price = 100M };
            Medicament p2 = new Medicament { MedicamentId = 2, Name = "M2", Price = 50M };
            // Arrange - create a new cart
            Cart target = new Cart();
            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            decimal result = target.ComputeTotalValue();
            // Assert
            Assert.Equal(450M, result);
        }

            [Fact]
        public void Can_Clear_Contents()
        {
            // Arrange - create some test products
            Medicament p1 = new Medicament { MedicamentId = 1, Name = "P1", Price = 100M };
            Medicament p2 = new Medicament { MedicamentId = 2, Name = "P2", Price = 50M };
            // Arrange - create a new cart
            Cart target = new Cart();
            // Arrange - add some items
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            // Act - reset the cart
            target.Clear();
            // Assert
            Assert.Equal(0, target.Lines.Count());

        }
    }
}
