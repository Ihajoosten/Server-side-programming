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
    public class MealServices
    {

        public static Meal meal = new Meal()
        {
            DateValid = DateTime.Now.Date
        };

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
        public void CreateMeal()
        {
            var options = new DbContextOptionsBuilder<CookDbContext>()
                .UseInMemoryDatabase(databaseName: "CookTest")
                .Options;

            // Run the test against one instance of the context
            using (var context = new CookDbContext(options))
            {

                var service = new EFMealService(context);
                Dish[] dishes = new Dish[] { dish, dish2, dish3 };
                service.CreateMeal(meal, dishes);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new CookDbContext(options))
            {
                bool mealBool = context.Meal.FirstOrDefault(m => m.DateValid == meal.DateValid) != null ? true : false;
                Assert.True(mealBool);
                Assert.NotNull(context.Meal.FirstOrDefault());
            }
        }

        [Fact]
        public void GetAllMeals()
        {
            var options = new DbContextOptionsBuilder<CookDbContext>()
                .UseInMemoryDatabase(databaseName: "CookTest")
                .Options;


            // Use a separate instance of the context to verify correct data was saved to database
            using var context = new CookDbContext(options);
            var service = new EFMealService(context);
            var meals = service.GetMeals();

            Assert.Equal(meals.Count, context.Meal.Count());
       }

        [Fact]
        public void GetMealById()
        {
            var options = new DbContextOptionsBuilder<CookDbContext>()
                .UseInMemoryDatabase(databaseName: "CookTest")
                .Options;

            // Use a separate instance of the context to verify correct data was saved to database
            using var context = new CookDbContext(options);
            var service = new EFMealService(context);

            Meal meall = new Meal();
            var meals = service.GetMeals();
            foreach (var item in meals)
            {
                if (item.DateValid == meal.DateValid) meall = item;
            }
            bool mealBool = service.GetMealById(meall.Id) != null ? true : false;
            Assert.True(mealBool);
        }

        [Fact]
        public void UpdateMeal()
        {
            var options = new DbContextOptionsBuilder<CookDbContext>()
                .UseInMemoryDatabase(databaseName: "CookTest")
                .Options;

            // Run the test against one instance of the context
            using (var context = new CookDbContext(options))
            {
                var service = new EFMealService(context);

                meal.Dishes.Add(new MealDishes { Meal = meal, Dish = dish });
                meal.Dishes.Add(new MealDishes { Meal = meal, Dish = dish2 });
                meal.Dishes.Add(new MealDishes { Meal = meal, Dish = dish3 });
                Dish[] dishes = new Dish[] { dish, dish2, dish3 };

                service.UpdateMeal(meal, dishes);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new CookDbContext(options))
            {
                var service = new EFMealService(context);

                Meal meall = new Meal();
                var meals = service.GetMeals();
                foreach (var item in meals)
                {
                    if (item.DateValid == meal.DateValid) meall = item;
                }
                bool mealBool = service.GetMealById(meall.Id) != null ? true : false;
                Assert.True(mealBool);

                var getMeal = service.GetMealById(meall.Id);
                var mealDishes = service.GetAllMealDishes().Count();
                Assert.NotNull(getMeal);
                Assert.Equal(3, mealDishes);
            }
        }

        //[Fact]
        //public void DeleteMeal()
        //{
        //    var options = new DbContextOptionsBuilder<CookDbContext>()
        //        .UseInMemoryDatabase(databaseName: "CookTest")
        //        .Options;

        //    // Run the test against one instance of the context
        //    using (var context = new CookDbContext(options))
        //    {
        //        var service = new EFMealService(context);

        //        meal.Dishes.Add(new MealDishes { Meal = meal, Dish = dish });
        //        meal.Dishes.Add(new MealDishes { Meal = meal, Dish = dish2 });
        //        meal.Dishes.Add(new MealDishes { Meal = meal, Dish = dish3 });

        //        service.DeleteMeal(meal);
        //    }

        //    // Use a separate instance of the context to verify correct data was saved to database
        //    using (var context = new CookDbContext(options))
        //    {
        //        var service = new EFMealService(context);
        //        bool mealBool = service.GetMeals().Count() == 0 ? true : false;
        //        Assert.True(mealBool);
        //    }
        //}
    }
}
