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
        private readonly CookDbContext _context;

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
            _context.SaveChanges();
        }

        public Meal GetMealById(int? id)
        {
            if (id == null) throw new NullReferenceException();
            return _context.Meal.FirstOrDefault(m => m.Id == id);
        }

        public Dish GetDish(int? id)
        {
            if (id == null) throw new NullReferenceException();
            return _context.Dish.FirstOrDefault(d => d.Id == id);
        }

        public List<Meal> GetMeals()
        {
            var meals = _context.Meal
                .Include(m => m.Dishes)
                .ToList();
            meals.ForEach(m =>
            {
                var updatedDishes = new List<MealDishes>();
                for (var i = 0; i < m.Dishes.Count; i++)
                {
                    var localDish = m.Dishes.ToList()[i];
                    localDish.Meal = null;
                    updatedDishes.Add(localDish);
                }
                m.Dishes = updatedDishes;
            });

            return meals;
        }

        public void UpdateMeal(Meal meal, Dish[] dishes)
        {
            if (meal == null) throw new NullReferenceException();

            if (dishes.Length == 3)
            {
                IEnumerable<MealDishes> mealDishes = _context.MealDish.Where(md => md.MealId == meal.Id);

                mealDishes.ToList().ForEach(md =>
                {
                    md.Dish = GetDish(md.DishId);
                    md.Meal = GetMealById(md.MealId);
                    _context.MealDish.Remove(md);
                });

                try
                {
                    foreach (Dish dish in dishes)
                    {
                        _context.Add(new MealDishes { Dish = dish, Meal = meal });
                    }
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }
        }
    }
}
