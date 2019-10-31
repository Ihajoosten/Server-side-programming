using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    public class Meal
    {

        [Required]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateValid { get; set; }

        [Required]
        [Column("MealDishes")]
        public ICollection<MealDishes> Dishes { get; set; } = new List<MealDishes>();
    }
}
