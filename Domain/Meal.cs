using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Meal
    {
        public Meal()
        {

        }

        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateValid { get; set; }

        [Required]
        public ICollection<MealDishes> Dishes { get; set; } = new List<MealDishes>();

    }
}