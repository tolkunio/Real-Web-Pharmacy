using System;
using System.Collections.Generic;
using System.Text;
using WebPharmacy.Models;
using Xunit;

namespace WebPharmacy.Tests
{
 public   class ProductTests
    {
        [Fact]
        public void CanChangeProductName()
        {
            //Arrange
            var m = new Medicament { Name = "Test", Price = 100m };
            //Act
            m.Name = "New Name";
            //Assert
            Assert.Equal("New Name", m.Name);
        }
        [Fact]
        public void CanChangeProductPrice()
        {
            //Arrange
            var m = new Medicament { Name = "Test", Price = 100m };
            //Act
            m.Price = 100m;
            //Assert
            Assert.Equal(100m, m.Price);
        }
    }
}
