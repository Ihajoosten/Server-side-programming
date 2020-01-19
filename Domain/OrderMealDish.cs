using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class OrderMealDish
    {
        [Key]
        public int Id { get; set; }

        public int OrderMealId { get; set; }

        public int MealId { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

    }
}
