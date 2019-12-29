using Domain;
using DomainServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Cook
{
    public class EFMealService : IMealService
    {
        protected readonly CookDbContext _context;

        public EFMealService(CookDbContext context) => _context = context;

        public IQueryable<Meal> Meal => _context.Meal;

        public IQueryable<MealDishes> MealDish => _context.MealDish;

        public List<MealDishes> GetAllMealDishes() => _context.MealDish.ToList();

        public void CreateMeal(Meal meal, Dish[] dishes)
        {
            if (meal == null) throw new OperationCanceledException();

            if (dishes.Length == 3)
            {
                try
                {
                    _context.Add(meal);
                    foreach (Dish item in dishes)
                    {
                        _context.Add(new MealDishes { Meal = meal, Dish = item });
                    }
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }
        }

        public void DeleteMeal(Meal meal)
        {
            if (meal == null) throw new NullReferenceException();
            var entry = _context.Meal.FirstOrDefault(m => m.Id == meal.Id);
            _context.Meal.Remove(entry);
            _context.SaveChangesAsync();
        }

        public Meal GetMealById(int? id)
        {
            if (id == null) throw new NullReferenceException();
            return _context.Meal.FirstOrDefault(m => m.Id == id);
        }

        public List<Meal> GetMeals() => _context.Meal.ToList();

        public void UpdateMeal(Meal meal, Dish[] dishes)
        {
            if (meal == null) throw new NullReferenceException();

            if (dishes.Length == 3)
            {
                try
                {

                    //foreach (var item in _context.MealDish.ToList())
                    //{
                    //    if (item.MealId == meal.Id)
                    //    {
                    //        _context.MealDish.Remove(item);
                    //    }
                    //}

                    //_context.Meal.Update(meal);

                    //foreach (var dish in dishes)
                    //{
                    //    _context.MealDish.Add(new MealDishes { Dish = dish, Meal = meal });
                    //}

                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }

            _context.SaveChangesAsync();
        }
    }
}
