using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Interfaces
{
    public interface IMealRepo
    {

        IQueryable<Meal> Dishes { get; }

        List<Meal> GetMeals();

        Meal GetMealById(int? id);

        bool CreateMeal(Meal meal, Dish[] dishes);

        void UpdateMeal(Meal meal);

        void DeleteMeal(Meal meal);
    }
}
