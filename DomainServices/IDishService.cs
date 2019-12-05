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

        Dish GetDishById(int? id);

        List<Dish> GetDishes();

        void CreateDish(Dish dish);

        void UpdateDish(int? id);

        void DeleteDish(int? id);

    }
}
