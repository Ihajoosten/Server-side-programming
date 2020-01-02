using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cook.Models.Menu
{
    public class CreateMenuViewModel
    {
        public int Week { get; set; }

        public int Year { get; set; }

        public Dictionary<int, int> Days { get; set; } = new Dictionary<int, int>();
    }
}
