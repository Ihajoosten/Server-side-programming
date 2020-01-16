using Domain;
using Domain.Dishsize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models.Order
{
    public class OrderMealItem
    {
        public Meal MealItem { get; set; }

        public DishSize MealSize { get; set; }
    }
}
