using Domain;
using Domain.Dishsize;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models.Order
{
    public class CheckoutViewModel
    {
        public DishSize Size { get; set; }

        public Dictionary<int, DishSize> CheckoutItems { get; set; } = new Dictionary<int, DishSize>();


    }
}
