using Client.Controllers;
using DomainServices;
using Microsoft.AspNetCore.Mvc;
using Models.Cart;
using Moq;
using Xunit;

namespace Tests.Client.Controller
{
    public class CartControllerTests
    {
        [Fact]
        public void Cart()
        {
            // Arrange
            var mockDish = new Mock<IDishService>();
            var mockMeal = new Mock<IMealService>();
            var cart = new Mock<Domain.Cart>();


            mockDish.Setup(repo => repo.GetDishes());
            var controller = new CartController(cart.Object, mockMeal.Object, mockDish.Object);

            // Act
            var result = controller.Cart();

            // Assert
            Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<CartViewModel>(result.ViewData.Model);
        }

        [Fact]
        public void AddToCart()
        {
            // Arrange
            var mockDish = new Mock<IDishService>();
            var mockMeal = new Mock<IMealService>();
            var cart = new Mock<Domain.Cart>();


            mockDish.Setup(repo => repo.GetDishes());
            mockMeal.Setup(repo => repo.GetMealById(1));
            var controller = new CartController(cart.Object, mockMeal.Object, mockDish.Object);

            // Act
            var result = controller.AddToCart(1, "Cart/Cart");

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }

        [Fact]
        public void RemoveFromCart()
        {
            // Arrange
            var mockDish = new Mock<IDishService>();
            var mockMeal = new Mock<IMealService>();
            var cart = new Mock<Domain.Cart>();


            mockDish.Setup(repo => repo.GetDishById(1));
            mockMeal.Setup(repo => repo.GetMealById(1));
            var controller = new CartController(cart.Object, mockMeal.Object, mockDish.Object);

            // Act
            var result = controller.RemoveFromCart(1);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
            Assert.IsAssignableFrom<RedirectToActionResult>(result);
        }

        [Fact]
        public void AddToCartReturnsRedirectToAction()
        {
            // Arrange
            var mockDish = new Mock<IDishService>();
            var mockMeal = new Mock<IMealService>();
            var cart = new Mock<Domain.Cart>();
            var controller = new CartController(cart.Object, mockMeal.Object, mockDish.Object);


            // Act
            var result = controller.AddToCart(99, "");

            // Assert
            var redirectToActionResult =
                Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Cart", redirectToActionResult.ControllerName);
            Assert.Equal("Cart", redirectToActionResult.ActionName);
        }

        [Fact]
        public void RemoveFromCartReturnsRedirectToAction()
        {
            // Arrange
            var mockDish = new Mock<IDishService>();
            var mockMeal = new Mock<IMealService>();
            var cart = new Mock<Domain.Cart>();
            var controller = new CartController(cart.Object, mockMeal.Object, mockDish.Object);


            // Act
            var result = controller.RemoveFromCart(99);

            // Assert
            var redirectToActionResult =
                Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Cart", redirectToActionResult.ControllerName);
            Assert.Equal("Cart", redirectToActionResult.ActionName);
        }

        [Fact]
        public void RemoveDishFromCartReturnsRedirectToAction()
        {
            // Arrange
            var mockDish = new Mock<IDishService>();
            var mockMeal = new Mock<IMealService>();
            var cart = new Mock<Domain.Cart>();
            var controller = new CartController(cart.Object, mockMeal.Object, mockDish.Object);

            // Act
            var result = controller.RemoveDish(99, 99);

            // Assert
            var redirectToActionResult =
                Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Cart", redirectToActionResult.ControllerName);
            Assert.Equal("Cart", redirectToActionResult.ActionName);
        }
    }
}
