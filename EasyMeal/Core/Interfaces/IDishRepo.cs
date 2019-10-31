﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Interfaces
{
    public interface IDishRepo
    {
        IQueryable<Dish> Dishes { get; }

        List<Dish> GetDishes();

        Dish GetDishById(int? id);

        void CreateDish(Dish dish);

        void UpdateDish(Dish dish);

        void DeleteDish(Dish dish);
    }
}