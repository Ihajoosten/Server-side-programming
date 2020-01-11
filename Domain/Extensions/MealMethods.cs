using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Extensions
{
    public static class MealMethods
    {
        public static double GetMealPrice(this Meal meal)
        {
            double price = 0;

            foreach (var item in meal.Dishes)
            {
                price = +item.Dish.Price;
            }

            return price;
        }
    }
}
