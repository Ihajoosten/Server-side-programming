using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Extensions
{
    public static class CartMethods
    {
        public static double GetOrderPrice(this Order order)
        {
            double price = 0;

            foreach (var meal in order.OrderMeals)
            {
                foreach (var dish in meal.Dishes)
                {
                    price = +dish.Dish.Price;
                }
            }

            return price;
        }
    }
}
