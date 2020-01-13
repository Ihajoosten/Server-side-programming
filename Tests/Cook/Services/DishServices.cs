using Domain;
using Infrastructure.Cook;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Tests.Cook.Services
{
    public class DishServices
    {
        public static Dish dish = new Dish()
        {
            Name = "Test dish",
            Description = "Test description",
            Price = 5.95
        };

        public static Dish dish2 = new Dish()
        {
            Name = "Test DISH",
            Description = "Test description",
            Price = 5.95
        };

        public static Dish dish3 = new Dish()
        {
            Name = "Test DISH",
            Description = "Test description lol",
            Price = 5.95
        };

        [Fact]
        public void GetAllDishes()
        {
            var options = new DbContextOptionsBuilder<CookDbContext>()
                .UseInMemoryDatabase(databaseName: "CookTest")
                .Options;

            // Run the test against one instance of the context
            using (var context = new CookDbContext(options))
            {

                var service = new EFDishService(context);
                service.CreateDish(dish);
                service.CreateDish(dish2);
                context.SaveChanges();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new CookDbContext(options))
            {
                var service = new EFDishService(context);
                var dishes = service.GetDishes();
                
                Assert.Equal(dishes.Count, context.Dish.Count());
            }
        }

        [Fact]
        public void UpdateDish()
        {
            var options = new DbContextOptionsBuilder<CookDbContext>()
                .UseInMemoryDatabase(databaseName: "CookTest")
                .Options;

            // Run the test against one instance of the context
            using (var context = new CookDbContext(options))
            {

                var service = new EFDishService(context);
                service.CreateDish(dish);
                service.CreateDish(dish2);
                context.SaveChanges();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new CookDbContext(options))
            {
                var service = new EFDishService(context);

                dish.Name = "Testing this dish";
                dish.Price = 7.99;
                dish.Description = "hhahaha";

                service.UpdateDish(dish);
                context.SaveChanges();

                Assert.Equal("Testing this dish", dish.Name);
                Assert.Equal(7.99, dish.Price);
                Assert.Equal("hhahaha", dish.Description);
            }
        }

        [Fact]
        public void DeleteDish()
        {
            var options = new DbContextOptionsBuilder<CookDbContext>()
                .UseInMemoryDatabase(databaseName: "CookTest")
                .Options;

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new CookDbContext(options))
            {
                var service = new EFDishService(context);

                service.CreateDish(dish3);
                context.SaveChanges();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new CookDbContext(options))
            {
                var service = new EFDishService(context);

                service.DeleteDish(dish3);
                context.SaveChanges();

                var getDish = service.GetDishById(dish3.Id);

                Assert.Null(getDish);
            }
        }

        [Fact]
        public void CreateNewDish()
        {
            var options = new DbContextOptionsBuilder<CookDbContext>()
                .UseInMemoryDatabase(databaseName: "CookTest")
                .Options;

            // Run the test against one instance of the context
            using (var context = new CookDbContext(options))
            {

                var service = new EFDishService(context);
                service.CreateDish(dish);
                context.SaveChanges();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new CookDbContext(options))
            {
                Assert.Equal("Testing this dish", context.Dish.FirstOrDefault(d => d.Id == dish.Id).Name); ;
                Assert.Equal(7.99, context.Dish.FirstOrDefault(d => d.Id == dish.Id).Price);
            }


        }

        [Fact]
        public void GetDishById()
        {
            var options = new DbContextOptionsBuilder<CookDbContext>()
                .UseInMemoryDatabase(databaseName: "CookTest")
                .Options;

            // Run the test against one instance of the context
            using (var context = new CookDbContext(options))
            {

                var service = new EFDishService(context);
                service.CreateDish(dish2);
                context.SaveChanges();
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new CookDbContext(options))
            {
                var service = new EFDishService(context);

                Dish dish = new Dish();
                var dishes = service.GetDishes();
                foreach (var item in dishes)
                {
                    if (item.Name == dish2.Name) dish = item;
                }
                var getDish = service.GetDishById(dish.Id);
                Assert.Equal(dish, getDish);
            }
        }
    }
}
