using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Meal
    {

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateValid { get; set; }

        [Required]
        [Column("MealDishes")]
        public ICollection<Dish> Dishes { get; set; } = new List<Dish>();

        [Required]
        public Cook Cook { get; set; }
    }
}