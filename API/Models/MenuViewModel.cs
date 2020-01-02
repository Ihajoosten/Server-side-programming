using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class MenuViewModel
    {
        public int Id { get; set; }

        public int Week { get; set; }

        public int Year { get; set; }

        public ICollection<Meal> Meals { get; set; } = new List<Meal>();
    }
}
