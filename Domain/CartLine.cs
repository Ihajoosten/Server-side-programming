using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class CartLine
    {
        public Meal Meal { get; set; }

        public DayOfWeek DayOfWeek { get; set; }
    }
}
