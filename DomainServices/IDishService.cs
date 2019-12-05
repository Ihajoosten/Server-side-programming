using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainServices
{
    public interface IDishService
    {
        IQueryable<Dish> Dish { get; }

        // As a Cook I want to get a Dish by ID to see the details
        Dish GetDishById(int? id);

        // As a Cook I want to see a list of Dishes
        List<Dish> GetDishes();

        // As a Cook I want to create a new Dish
        void CreateDish(Dish dish);

        // As a Cook I want to update an existing Dish
        void UpdateDish(int? id);

        // As a Cook I want to delete incorrect Dishes
        void DeleteDish(int? id);

    }
}
