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
}