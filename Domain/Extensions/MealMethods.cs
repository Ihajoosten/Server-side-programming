using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Extensions
{
    public static class MealMethods
    {
        public static double GetMenuPrice(this Meal meal)
        {
            double price = 0;

            foreach (var dish in meal.Dishes)
            {
                price += dish.Dish.Price;
            }

            foreach (var dish in meal.Dishes)
            {
                if (dish.Dish.Size == DishSize.Large)
                {
                    return price * 1.2;
                }
                else if (dish.Dish.Size == DishSize.Small)
                {
                    return price * 0.8;
                }
                else
                {
                    return price;
                }
            }
            return price;
        }
    }
}
