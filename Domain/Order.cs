using Domain.Dishsize;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class Order
    {
        public Order() { }

        [Key]
        public int Id { get; set; }

        public int ClientId { get; set; }

        [Required]
        public Client Client { get; set; }

        [Required]
        [NotMapped]
        public Dictionary<Meal, DishSize> OrderMeals { get; set; } = new Dictionary<Meal, DishSize>();

        [Required]
        public double TotalPrice { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; } = DateTime.Now.Date;

        public Dictionary<CartLine, DishSize> GetMealsWithMonth(int month)
        {
            Dictionary<CartLine, DishSize> result = new Dictionary<CartLine, DishSize>();

            foreach (var meal in OrderMeals)
            {
                if (meal.Key.DateValid.Month == month)
                {
                    result.Add(new CartLine { Meal = meal.Key, DayOfWeek = meal.Key.DateValid.DayOfWeek}, meal.Value);
                }
            }
            return result;
        }
    }
}
