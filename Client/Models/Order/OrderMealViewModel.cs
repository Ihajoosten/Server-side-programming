using System.Collections.Generic;

namespace Client.Models.Order
{
    public class OrderMealViewModel
    {
        public Dictionary<int, KeyValuePair<int, int>> DayMeals { get; set; } = new Dictionary<int, KeyValuePair<int, int>>();
    }
}