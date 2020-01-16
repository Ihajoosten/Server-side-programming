using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public List<Meal> OrderMeals { get; set; } = new List<Meal>();

        [Required]
        public double TotalPrice { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; } = DateTime.Now.Date;

        public List<Meal> GetMealsWithMonth(int month)
        {
            List<Meal> result = new List<Meal>();

            foreach (Meal meal in OrderMeals)
            {
                if (meal.DateValid.Month == month)
                {
                    result.Add(meal);
                }
            }
            return result;
        }
    }
}
