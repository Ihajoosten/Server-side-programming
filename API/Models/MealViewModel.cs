using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_level_2.Models
{
    public class MealViewModel
    {

        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateValid { get; set; }

        public List<Dish> Dishes { get; set; } = new List<Dish>();
    }
}
