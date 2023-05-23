using SimpleApp.Models;

namespace SimpleApp.Tests
{
    public class ProductTests
    {
        [Fact]
        public void CanChangeProductName()
        {
            // Arrange/Given
            var p = new Product { Name = "Test", Price = 100M };

            // Act/When
            p.Name = "New name";

            // Assert/Then
            Assert.Equal("New name", p.Name);
        }

        [Fact]
        public void CanChangeProductPrice()
        {
            // Arrange/Given
            var p = new Product { Name = "Test", Price = 100M };

            // Act/When
            p.Price = 200M;

            // Assert/Then
            Assert.Equal(200M, p.Price);
        }
    }
}
