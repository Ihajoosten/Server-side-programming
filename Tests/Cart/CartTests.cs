﻿using Domain;
using Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Cart
{
    public class CartTests
    {

        public static Dish dish = new Dish()
        {
            Id = 1,
            Name = "Test dish",
            Description = "Test description",
            Price = 5.95
        };

        public static Dish dish2 = new Dish()
        {
            Id = 2,
            Name = "Test DISH",
            Description = "Test description",
            Price = 5.95
        };

        public static Dish dish3 = new Dish()
        {
            Id = 3,
            Name = "Test DISH",
            Description = "Test description lol",
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

        public static Meal meal3 = new Meal
        {
            Id = 3,
            DateValid = DateTime.Now.Date,
        };

        public static Meal meal4 = new Meal
        {
            Id = 3,
            DateValid = DateTime.Now.Date,
        };

        [Fact]
        public void Calculate_Cart_Total()
        {

            Domain.Cart target = new Domain.Cart();

            meal2.MealDishes.Add(dish);
            meal2.MealDishes.Add(dish2);
            meal3.MealDishes.Add(dish);
            meal3.MealDishes.Add(dish2);


            target.AddItem(meal2, DateTime.Now.DayOfWeek);
            target.AddItem(meal3, DateTime.Now.DayOfWeek);
            var lines = target.Lines;

            double price = target.ComputeTotalValue(lines);

            Assert.Equal(59.5, price);
        }



        [Fact]
        public void Can_Add_New_Lines()
        {
            Domain.Cart target = new Domain.Cart();

            target.AddItem(meal1, DateTime.Now.DayOfWeek);
            target.AddItem(meal2, DateTime.Now.DayOfWeek);

            CartLine[] results = target.Lines.ToArray();

            Assert.Equal(2, results.Length);
            Assert.Equal(meal1, results[0].Meal);
            Assert.Equal(meal2, results[1].Meal);
            target.Clear();
        }

        [Fact]
        public void Can_Delete_Meal_From_Line()
        {
            Domain.Cart target = new Domain.Cart();

            target.AddItem(meal1, DateTime.Now.DayOfWeek);
            target.AddItem(meal2, DateTime.Now.DayOfWeek);
            target.AddItem(meal3, DateTime.Now.DayOfWeek);


            CartLine[] results = target.Lines.ToArray();

            Assert.Equal(3, results.Length);
            Assert.Equal(meal1, results[0].Meal);
            Assert.Equal(meal2, results[1].Meal);
            Assert.Equal(meal3, results[2].Meal);

            target.RemoveLine(meal2);
            Assert.Equal(0, target.Lines.Count(meal => meal.Meal == meal2));
            Assert.Equal(2, target.Lines.Count);
            target.Clear();
        }


        [Fact]
        public void Can_Clear_Cart()
        {

            Domain.Cart target = new Domain.Cart();


            target.AddItem(meal2, DateTime.Now.DayOfWeek);
            target.AddItem(meal3, DateTime.Now.DayOfWeek);

            target.Clear();

            Assert.Empty(target.Lines);
        }

        [Fact]

        public void Not_4_days_Included()
        {

            Domain.Cart target = new Domain.Cart();


            target.AddItem(meal2, DateTime.Now.DayOfWeek);
            target.AddItem(meal3, DateTime.Now.DayOfWeek);

            var result = target.IsValid();

            Assert.False(result);

        }

        [Fact]

        public void Not_4_days_Included_With_2_Meals_On_Same_Day()
        {

            Domain.Cart target = new Domain.Cart();

            target.AddItem(meal2, DateTime.Now.DayOfWeek);
            target.AddItem(meal3, DateTime.Now.DayOfWeek);
            target.AddItem(meal1, DateTime.Now.DayOfWeek);
            target.AddItem(meal3, DateTime.Now.DayOfWeek);

            var result = target.IsValid();

            Assert.False(result);

        }

        [Fact]
        public void Has_4_days_Included()
        {

            Domain.Cart target = new Domain.Cart();

            target.AddItem(meal2, DateTime.Now.DayOfWeek);
            target.AddItem(meal3, DateTime.Now.DayOfWeek);
            target.AddItem(meal1, DateTime.Now.DayOfWeek);
            target.AddItem(meal4, DateTime.Now.DayOfWeek);

            var result = target.IsValid();

            Assert.False(result);

        }

        [Fact]
        public void Has_BirthDay_Discount()
        {
            Domain.Cart target = new Domain.Cart();
            meal1.MealDishes = new List<Dish>()
            {
                dish, dish2, dish3
            };
            meal2.MealDishes = new List<Dish>()
            {
                dish, dish2, dish3
            };
            meal3.MealDishes = new List<Dish>()
            {
                dish, dish2, dish3
            };

            target.AddItem(meal2, DateTime.Now.DayOfWeek);
            target.AddItem(meal3, DateTime.Now.DayOfWeek);
            target.AddItem(meal1, DateTime.Now.DayOfWeek);

            var lines = target.Lines;
            double total = target.ComputeTotalValue(lines);
            Assert.Equal((17.85 * 3), total);

           Domain.Client client = new Domain.Client()
            {
                FirstName = "Tester",
                LastName = "Test",
                Birthday = DateTime.Now.Date,
                Email = "Test@gmail.com"
            };

            bool sameDate = target.MealOnBirthDay(client.Birthday);

            Assert.True(sameDate);

            if (sameDate)
            {
                total -= MealMethods.GetMealPrice(meal1);
            }

            Assert.Equal(35.7, total);
        }
    }
}
