using Domain.Dishsize;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Extensions
{
    public static class DishMethods
    {
        public static double GetDishPrice(this Dish dish)
        {
            double price = dish.Price;


            if (dish.Size == DishSize.Large)
            {
                return price * 1.2;
            }
            else if (dish.Size == DishSize.Small)
            {
                return price * 0.8;
            }
            else
            {
                return price;
            }
        }
    }
}
