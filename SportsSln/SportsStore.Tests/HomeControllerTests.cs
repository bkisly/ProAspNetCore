using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;

namespace SportsStore.Tests;

public class HomeControllerTests
{
    [Fact]
    public void CanUseIndex()
    {
        // Arrange

        var mock = new Mock<IStoreRepository>();
        mock.Setup(p => p.Products).Returns(new[]
        {
            new Product { ProductId = 1, Name = "P1" },
            new Product { ProductId = 2, Name = "P2" }
        }.AsQueryable());

        var controller = new HomeController(mock.Object);

        // Act

        var result = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

        // Assert

        var resultArray = result?.ToArray() ?? Array.Empty<Product>();

        Assert.Equal(2, resultArray.Length);
        Assert.Equal("P1", resultArray[0].Name);
        Assert.Equal("P2", resultArray[1].Name);
    }

    [Fact]
    public void CanPaginate()
    {
        // Arrange

        Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
        mock.Setup(m => m.Products).Returns((new Product[] {
            new Product {ProductId = 1, Name = "P1"},
            new Product {ProductId = 2, Name = "P2"},
            new Product {ProductId = 3, Name = "P3"},
            new Product {ProductId = 4, Name = "P4"},
            new Product {ProductId = 5, Name = "P5"}
        }).AsQueryable<Product>());

        HomeController controller = new HomeController(mock.Object);
        controller.PageSize = 3;

        // Act

        IEnumerable<Product> result =
            (controller.Index(2) as ViewResult)?.ViewData.Model
            as IEnumerable<Product>
            ?? Enumerable.Empty<Product>();

        // Assert

        Product[] prodArray = result.ToArray();

        Assert.True(prodArray.Length == 2);
        Assert.Equal("P4", prodArray[0].Name);
        Assert.Equal("P5", prodArray[1].Name);
    }
}
