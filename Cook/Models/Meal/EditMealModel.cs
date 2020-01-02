using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cook.Models
{
    public class EditMealModel
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateForMeal { get; set; }

        public int StarterId { get; set; }

        public int MainId { get; set; }

        public int DessertId { get; set; }
    }
}
