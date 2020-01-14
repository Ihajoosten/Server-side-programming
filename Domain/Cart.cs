using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Extensions;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Cart
    {
        //private readonly List<Meal> lineCollection = new List<Meal>();

        //public virtual void AddItem(Meal meal)
        //{
        //    lineCollection.Add(meal);
        //}

        //public virtual void RemoveLine(Meal meal) => lineCollection.RemoveAll(l => l.Id == meal.Id);


        //public virtual void Clear() => lineCollection.Clear();

        //public virtual List<Meal> Lines => lineCollection;
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

        public virtual List<CartLine> Lines => _lineCollection;

        public bool IsValid()
        {
            int weekday = 0;
            int weekendday = 0;

            foreach (CartLine m in Lines)
            {
                if (m.Meal.DateValid.DayOfWeek == DayOfWeek.Monday) weekday++;
                if (m.Meal.DateValid.DayOfWeek == DayOfWeek.Tuesday) weekday++;
                if (m.Meal.DateValid.DayOfWeek == DayOfWeek.Wednesday) weekday++;
                if (m.Meal.DateValid.DayOfWeek == DayOfWeek.Thursday) weekday++;
                if (m.Meal.DateValid.DayOfWeek == DayOfWeek.Friday) weekday++;
                if (m.Meal.DateValid.DayOfWeek == DayOfWeek.Saturday) weekendday++;
                else weekendday++;
            }

            if (weekday > 3) return true;
            return false;
        }

        public virtual double ComputeTotalValue()
        {
            double price = 0.00;
            foreach (var item in _lineCollection)
            {
                foreach (var meal in item.Meal.Dishes)
                {

                    price = +DishMethods.GetDishPrice(meal.Dish);
                }
            }
            return price;
        }


        public virtual string StartDate { get; set; }

        public virtual string EndDate { get; set; }

        public virtual void Save()
        {

        }


        // Prijs op basis van gerecht prijs en de grootte per week bestelling
        //public decimal ComputeTotalValue() =>

    }
}
