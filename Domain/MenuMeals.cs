using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class MenuMeals
    {
        public int MenuId { get; set; }

        public Menu Menu { get; set; }

        public int MealId { get; set; }

        public Meal Meal{ get; set; }
    }
}
