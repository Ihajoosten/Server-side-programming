using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Menu
    {
        public Menu() { }

        [Key]
        public int Id { get; set; }

        [Required]
        public int Week { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public ICollection<Meal> Meals { get; set; } = new List<Meal>();
    }
}
