using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests.Cart
{
    public class CartLineTotalPrice
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

        [Fact]
        public void Calculate_Cart_Total_Of_Single_Item()
        {
            meal2.MealDishes.Add(dish);
            meal2.MealDishes.Add(dish2);
            meal3.MealDishes.Add(dish);
            meal3.MealDishes.Add(dish2);

            CartLine line1 = new CartLine
            {
                DayOfWeek = DayOfWeek.Monday,
                Meal = meal2
            };

            CartLine line2 = new CartLine
            {
                DayOfWeek = DayOfWeek.Monday,
                Meal = meal3
            };

            Domain.Cart singleCart = new Domain.Cart();

            singleCart.Lines.Add(line1);
            singleCart.Lines.Add(line2);
            var lines = singleCart.Lines;

            double price = singleCart.ComputeTotalValue(lines);
            double assertPrice = (dish.Price + dish2.Price) * 2;

            Assert.Equal(assertPrice, price);
        }
    }
}
