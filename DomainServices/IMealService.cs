using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainServices
{
    public interface IMealService
    {
        IQueryable<Meal> Meal { get; }

        Meal GetMealById(int? id);

        List<Meal> GetMeals();

        void CreateMeal(Meal meal);

        void UpdateMeal(int? id);

        void DeleteMeal(int? id);
    }
}
