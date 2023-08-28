using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

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

        var result = (controller.Index(null) as ViewResult)?.ViewData.Model as ProductsListViewModel;

        // Assert

        var resultArray = result?.Products.ToArray() ?? Array.Empty<Product>();

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

        ProductsListViewModel result =
            (controller.Index(null, 2) as ViewResult)?.ViewData.Model
            as ProductsListViewModel
            ?? new();

        // Assert

        Product[] prodArray = result.Products.ToArray();

        Assert.True(prodArray.Length == 2);
        Assert.Equal("P4", prodArray[0].Name);
        Assert.Equal("P5", prodArray[1].Name);
    }

    [Fact]
    public void CanSendPaginationViewModel()
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

        HomeController controller =
            new HomeController(mock.Object) { PageSize = 3 };

        // Act

        ProductsListViewModel result =
            controller.Index(null, 2)?.ViewData.Model as ProductsListViewModel
            ?? new();

        // Assert

        PagingInfo pageInfo = result.PagingInfo;
        Assert.Equal(2, pageInfo.CurrentPage);
        Assert.Equal(3, pageInfo.ItemsPerPage);
        Assert.Equal(5, pageInfo.TotalItems);
        Assert.Equal(2, pageInfo.TotalPages);
    }

    [Fact]
    public void Can_Filter_Products()
    {
        // Arrange
        // - create the mock repository
        Mock<IStoreRepository> mock = new Mock<IStoreRepository>();

        mock.Setup(m => m.Products).Returns((new Product[] {
            new Product {ProductId = 1, Name = "P1", Category = "Cat1"},
            new Product {ProductId = 2, Name = "P2", Category = "Cat2"},
            new Product {ProductId = 3, Name = "P3", Category = "Cat1"},
            new Product {ProductId = 4, Name = "P4", Category = "Cat2"},
            new Product {ProductId = 5, Name = "P5", Category = "Cat3"}
        }).AsQueryable<Product>());
        // Arrange - create a controller and make the page size 3 items
        HomeController controller = new HomeController(mock.Object);
        controller.PageSize = 3;
        // Action
        Product[] result = (controller.Index("Cat2", 1)?.ViewData.Model
            as ProductsListViewModel ?? new()).Products.ToArray();
        // Assert
        Assert.Equal(2, result.Length);
        Assert.True(result[0].Name == "P2" && result[0].Category == "Cat2");
        Assert.True(result[1].Name == "P4" && result[1].Category == "Cat2");
    }

    [Fact]
    public void Generate_Category_Specific_Product_Count()
    {
        // Arrange
        Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
        mock.Setup(m => m.Products).Returns((new Product[] {
            new Product {ProductId = 1, Name = "P1", Category = "Cat1"},
            new Product {ProductId = 2, Name = "P2", Category = "Cat2"},
            new Product {ProductId = 3, Name = "P3", Category = "Cat1"},
            new Product {ProductId = 4, Name = "P4", Category = "Cat2"},
            new Product {ProductId = 5, Name = "P5", Category = "Cat3"}
        }).AsQueryable<Product>());
        HomeController target = new HomeController(mock.Object);
        target.PageSize = 3;
        Func<ViewResult, ProductsListViewModel?> GetModel = result
            => result?.ViewData?.Model as ProductsListViewModel;
        // Action
        int? res1 = GetModel(target.Index("Cat1"))?.PagingInfo.TotalItems;
        int? res2 = GetModel(target.Index("Cat2"))?.PagingInfo.TotalItems;
        int? res3 = GetModel(target.Index("Cat3"))?.PagingInfo.TotalItems;
        int? resAll = GetModel(target.Index(null))?.PagingInfo.TotalItems;
        // Assert
        Assert.Equal(2, res1);
        Assert.Equal(2, res2);
        Assert.Equal(1, res3);
        Assert.Equal(5, resAll);
    }
}
