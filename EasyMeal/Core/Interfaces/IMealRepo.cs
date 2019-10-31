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

        bool CreateMeal(Meal dish, Dish[] dishes);

        void UpdateMeal(int? id);

        void DeleteMeal(int? id);
    }
}
