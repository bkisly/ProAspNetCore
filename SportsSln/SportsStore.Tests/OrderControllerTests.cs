﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;

namespace SportsStore.Tests
{
    public class OrderControllerTests
    {
        [Fact]
        public void CannotCheckoutEmptyCart()
        {
            // Arrange
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            Cart cart = new Cart();
            Order order = new Order();
            OrderController target = new OrderController(mock.Object, cart);

            // Act
            ViewResult? result = target.Checkout(order) as ViewResult;

            // Assert
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            Assert.True(string.IsNullOrEmpty(result?.ViewName));
            Assert.False(result?.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void CannotCheckoutInvalidShippingDetails()
        {
            // Arrange
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);

            OrderController target = new OrderController(mock.Object, cart);
            target.ModelState.AddModelError("error", "error");

            // Act
            ViewResult? result = target.Checkout(new Order()) as ViewResult;

            // Assert
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            Assert.True(string.IsNullOrEmpty(result?.ViewName));
            Assert.False(result?.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void CanCheckoutAndSubmitOrder()
        {
            // Arrange
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);

            OrderController target = new OrderController(mock.Object, cart);

            // Act

            RedirectToPageResult? result = target.Checkout(new Order()) as RedirectToPageResult;

            // Assert

            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once);
            Assert.Equal("/Completed", result?.PageName);
        }
    }
}
