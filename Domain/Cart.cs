using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Cart
    {
        private List<CartLine> _lineCollection = new List<CartLine>();

        public virtual void AddItem(Meal meal, DayOfWeek dayOfWeek)
        {
            CartLine line = _lineCollection.Where(m => m.Meal.Id == meal.Id && m.DayOfWeek == dayOfWeek).FirstOrDefault();

            if (line == null)
            {
                _lineCollection.Add(new CartLine { Meal = meal, DayOfWeek = dayOfWeek });
            }
        }

        public virtual void RemoveLine(Meal meal) => _lineCollection.RemoveAll(l => l.Meal.Id == meal.Id);


        public virtual void Clear() => _lineCollection.Clear();

        public virtual IEnumerable<CartLine> Lines => _lineCollection;

       

       

        // Prijs op basis van gerecht prijs en de grootte per week bestelling
        //public decimal ComputeTotalValue() =>
            
    }


}
