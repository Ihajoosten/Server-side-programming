using Domain;
using Domain.Dishsize;
using Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests.Extensions
{
    public class Extensions
    {
        public static Dish dish = new Dish()
        {
            Id = 1,
            Name = "Test dish",
            Description = "Test description",
            Size = DishSize.Large,
            Price = 5.95
        };

        public static Dish dish2 = new Dish()
        {
            Id = 2,
            Name = "Test DISH",
            Description = "Test description",
            Size = DishSize.Medium,
            Price = 5.95
        };

        public static Dish dish3 = new Dish()
        {
            Id = 2,
            Name = "Test DISH",
            Description = "Test description",
            Size = DishSize.Small,
            Price = 5.95
        };

        public static Meal meal1 = new Meal
        {
            Id = 1,
            DateValid = DateTime.Now.Date,
            Dishes = new List<MealDishes>()
            {

                new MealDishes {MealId = 1, DishId = 1},
                new MealDishes {MealId = 1, DishId = 2}

            }
        };

        public static Meal meal2 = new Meal
        {
            Id = 2,
            DateValid = DateTime.Now.Date,

        };


        [Fact]
        public void GetDishPriceExtensionsMethod()
        {
            double dishPrice = DishMethods.GetDishPrice(dish);
            double dish2Price = DishMethods.GetDishPrice(dish2);
            double dish3Price = DishMethods.GetDishPrice(dish3);

            Assert.Equal((dish.Price * 1.2), dishPrice);
            Assert.Equal((dish2.Price), dish2Price);
            Assert.Equal((dish3.Price * 0.8), dish3Price);

        }

        [Fact]
        public void GetMealPriceExtensionMethod()
        {
            meal1.MealDishes = new List<Dish>()
            {
                dish, dish2, dish3
            };
            meal2.MealDishes = new List<Dish>()
            {
                dish, dish2, dish3
            };

            double meal1Price = MealMethods.GetMealPrice(meal1);
            double meal2Price = MealMethods.GetMealPrice(meal1);

            Assert.Equal((17.85), meal1Price);
            Assert.Equal((17.85), meal2Price);

        }
    }
}
