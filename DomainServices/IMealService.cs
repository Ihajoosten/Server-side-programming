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

        // As a Cook I want to get a Meal by ID to see the details
        Meal GetMealById(int? id);

        // As a Cook I want to see a list of Meals
        List<Meal> GetMeals();

        // As a Cook I want to create a new Meal
        void CreateMeal(Meal meal, Dish[] dishes);

        // As a Cook I want to update an existing Meal
        void UpdateMeal(Meal meal);

        // As a Cook I want to delete incorrect Meals
        void DeleteMeal(Meal meal);

        // As a Cook I want to get all dishes for a meal
        IEnumerable<Dish> GetMealDishes(Meal meal);
    }
}
