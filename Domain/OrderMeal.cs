using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class OrderMeal
    {
        [Key]
        public int Id { get; set; }

        public int MealId { get; set; }

        public int OrderId { get; set; }

        [DataType(DataType.Date)]
        public DateTime MealDate { get; set; }

        public bool birthdayMeal { get; set; }

        [Required]
        public MealSize MealSize { get; set; }

        public List<OrderMealDish> Dishes { get; set; } = new List<OrderMealDish>();
    }
}
