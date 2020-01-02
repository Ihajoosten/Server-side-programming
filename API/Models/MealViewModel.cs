using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class MealViewModel
    {

        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateValid { get; set; }

        public ICollection<Dish> Dishes { get; set; } = new List<Dish>();
    }
}
