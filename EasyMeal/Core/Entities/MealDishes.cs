using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
   public  class MealDishes
    {
        public int DishId { get; set; }
        public Dish Dish { get; set; }

        public int MealId { get; set; }
        public Meal Meal { get; set; }
    }
}
