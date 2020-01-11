using Cook.Controllers;
using Domain;
using DomainServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace Tests.Cook.Controller
{
    public class DishControllerTest
    {
        [Fact]
        public void Index()
        {
            // Arrange
            var mockDish = new Mock<IDishService>();


            mockDish.Setup(repo => repo.GetDishes());
            var controller = new DishesController(mockDish.Object);

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
            var list = Assert.IsAssignableFrom<IEnumerable<Dish>>(result.ViewData.Model);
            Assert.True(list != null);

        }

        [Fact]
        public void CreateViewResult()
        {
            // Arrange
            var mockDish = new Mock<IDishService>();


            mockDish.Setup(repo => repo.GetDishes());
            var controller = new DishesController(mockDish.Object);

            // Act
            var result = controller.Create();

            // Assert
            Assert.IsType<ViewResult>(result);

        }

        [Fact]
        public void CreateActionResult()
        {
            // Arrange
            var mockDish = new Mock<IDishService>();

            mockDish.Setup(repo => repo.GetDishes());
            var controller = new DishesController(mockDish.Object);
            var dish = new Dish();
            IFormFile file = null;

            // Act
            var result = controller.Create(dish, file);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);

        }

        [Fact]
        public void Index_Contains_All_Dish()
        {
            // Arrange - create the mock repository
            Mock<IDishService> mock = new Mock<IDishService>();
            mock.Setup(m => m.Dish).Returns(new Dish[] {
                new Dish {Id = 1, Name = "P1"},
                new Dish {Id = 2, Name = "P2"},
                new Dish {Id = 3, Name = "P3"},
            }.AsQueryable<Dish>());

            // Arrange - create a controller
            DishesController target = new DishesController(mock.Object);

            // Action
            Dish[] result
                = GetViewModel<IEnumerable<Dish>>(target.Index())?.ToArray();

            // Assert
            Assert.Equal(3, result.Length);
            Assert.Equal("P1", result[0].Name);
            Assert.Equal("P2", result[1].Name);
            Assert.Equal("P3", result[2].Name);
        }

        [Fact]
        public void Can_Edit_Dish()
        {
            // Arrange - create the mock repository
            Mock<IDishService> mock = new Mock<IDishService>();
            mock.Setup(m => m.Dish).Returns(new Dish[] {
                new Dish {Id = 1, Name = "P1"},
                new Dish {Id = 2, Name = "P2"},
                new Dish {Id = 3, Name = "P3"},
            }.AsQueryable<Dish>());

            // Arrange - create a controller
            DishesController target = new DishesController(mock.Object);

            // Act
            Dish p1 = GetViewModel<Dish>(target.Edit(1));
            Dish p2 = GetViewModel<Dish>(target.Edit(2));
            Dish p3 = GetViewModel<Dish>(target.Edit(3));

            // Assert
            Assert.Equal(1, p1.Id);
            Assert.Equal(2, p2.Id);
            Assert.Equal(3, p3.Id);
        }

        [Fact]
        public void Throws_Exception_When_Id_Is_Null()
        {
            // Arrange - create the mock repository
            Mock<IDishService> mock = new Mock<IDishService>();
            mock.Setup(m => m.Dish).Returns(new Dish[] {
                new Dish {Id = 1, Name = "P1"},
                new Dish {Id = 2, Name = "P2"},
                new Dish {Id = 3, Name = "P3"},
            }.AsQueryable<Dish>());

            // Arrange - create the controller
            DishesController target = new DishesController(mock.Object);

            // Assert
            Assert.Throws<NullReferenceException>(() => GetViewModel<Dish>(target.Edit(4)));
        }

        [Fact]
        public void ActionResult_Is_Of_Type_ViewResult()
        {
            // Arrange - create mock repository
            Mock<IDishService> mock = new Mock<IDishService>();
            mock.Setup(m => m.Dish).Returns(new Dish[] {
                new Dish {Id = 1, Name = "P1"},
                new Dish {Id = 2, Name = "P2"},
                new Dish {Id = 3, Name = "P3"},
            }.AsQueryable<Dish>());

            // Arrange - create the controller
            DishesController target = new DishesController(mock.Object);


            // Act - try to save the Dish
            IActionResult result = target.Edit(3);

            // Assert - check that the repository was called
            // Assert - check the result type is a ViewResult
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Get_KeyNotFound_Exception()
        {
            // Arrange - create mock repository
            Mock<IDishService> mock = new Mock<IDishService>();
            mock.Setup(m => m.Dish).Returns(new Dish[] {
                new Dish {Id = 1, Name = "P1"},
                new Dish {Id = 2, Name = "P2"},
                new Dish {Id = 3, Name = "P3"},
            }.AsQueryable<Dish>());

            // Arrange - create the controller
            DishesController target = new DishesController(mock.Object);

            // Assert - check that the repository was called
            // Assert - check the result type is a ViewResult
            Assert.Throws<KeyNotFoundException>(() => target.Edit(null));
        }

        private T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }
    }
}
