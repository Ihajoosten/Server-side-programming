using Domain;
using Domain.Dishsize;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.Order
{
    public class CheckoutViewModel
    {
        public MealSize Size { get; set; }

        public Dictionary<int, MealSize> CheckoutItems { get; set; } = new Dictionary<int, MealSize>();


    }
}
