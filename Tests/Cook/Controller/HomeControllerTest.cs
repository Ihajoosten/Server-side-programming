using Cook.Controllers;
using Domain;
using DomainServices;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Diagnostics;
using Xunit;

namespace Tests.Cook.Controller
{
    public class HomeControllerTest
    {
        [Fact]
        public void Dashboard()
        {
            // Arrange
            var mockDish = new Mock<IDishService>();
            var mockMeal = new Mock<IMealService>();
            var mockMenu = new Mock<IMenuService>();

            mockDish.Setup(repo => repo.GetDishes());
            mockMeal.Setup(repo => repo.GetMeals());
            mockMenu.Setup(repo => repo.GetMenus());
            var controller = new HomeController(mockDish.Object, mockMeal.Object, mockMenu.Object);

            // Act
            var result = controller.Dashboard();
            var mockedDishes = mockDish.Setup(repo => repo.GetDishes());
            var mockedMeals = mockMeal.Setup(repo => repo.GetMeals());
            var mockedMenus = mockMenu.Setup(repo => repo.GetMenus());

            // Assert
            Assert.IsType<ViewResult>(result);
            Assert.NotNull(mockedDishes);
            Assert.NotNull(mockedMeals);
            Assert.NotNull(mockedMenus);
        }

        [Fact]
        public void Privacy()
        {
            // Arrange
            var mockDish = new Mock<IDishService>();
            var mockMeal = new Mock<IMealService>();
            var mockMenu = new Mock<IMenuService>();
            var controller = new HomeController(mockDish.Object, mockMeal.Object, mockMenu.Object);

            // Act
            var result = controller.Privacy();

            // Assert
            Assert.IsType<ViewResult>(result);
            Assert.IsNotType<IActionResult>(result);
            Assert.IsNotType<ActionResult>(result);
        }
    }
}
