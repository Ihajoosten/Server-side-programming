using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Extensions;

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

        public virtual void RemoveDishFromMeal(Meal meal, Dish dish)
        {
            var gettingMeal = _lineCollection.Find(m => m.Meal.Id == meal.Id);
            _lineCollection.Remove(gettingMeal);

            var gettingMealDish = gettingMeal.Meal.Dishes.FirstOrDefault(md => md.DishId == dish.Id);
            gettingMeal.Meal.Dishes.Remove(gettingMealDish);

            _lineCollection.Add(new CartLine { Meal = gettingMeal.Meal, DayOfWeek = gettingMeal.DayOfWeek });
        }


        public virtual void Clear() => _lineCollection.Clear();

        public virtual List<CartLine> Lines => _lineCollection;

        public virtual double ComputeTotalValue(List<CartLine> lines)
        {
            double price = 0;
            foreach (var item in lines)
            {
                price += MealMethods.GetMealPrice(item.Meal);
            }
            return price;
        }

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

        public bool MealOnBirthDay(DateTime date)
        {
            foreach (var item in Lines)
            {
                if (item.Meal.DateValid.Date == date.Date) return true;
            }
            return false;
        }
    }
}
