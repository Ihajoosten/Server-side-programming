using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Chef
{
    public class EFMealRepo : IMealRepo
    {
        protected readonly ChefDbContext _context;

        public EFMealRepo(ChefDbContext context)
        {
            _context = context;
        }
        public IQueryable<Meal> Meals => _context.Meals;

        public IQueryable<MealDishes> MealDishes => _context.MealDishes;


        public void DeleteMeal(Meal meal)
        {
            Meal dbEntry = _context.Meals.FirstOrDefault(m => m.Id == meal.Id);
            _context.Meals.Remove(dbEntry);
            _context.SaveChanges();
        }

        public List<Meal> GetMeals()
        {

            return _context.Meals.ToList();
        }

        public Meal GetMealById(int? id)
        {
            if (id == null)
            {
                throw new KeyNotFoundException();
            }
            return _context.Meals.Find(id);
        }

        public IEnumerable<Dish> GetAllDishesForMeal(Meal meal)
        {
            return _context.MealDishes.Where(md => md.MealId == meal.Id).Select(d => d.Dish);

        }

        public bool CreateMeal(Meal meal, Dish[] dishes)
        {
            if (dishes.Length == 3)
            {
                try
                {
                    _context.Add(meal);
                    foreach (Dish item in dishes)
                    {
                        _context.Add(new MealDishes { Dish = item, Meal = meal });
                        // meal.Dishes.Add(new MealDishes {Dish = item });
                    }

                    _context.SaveChanges();
                    return true;

                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public void UpdateMeal(Meal meal)
        {
            Meal dbEntry = _context.Meals.FirstOrDefault(m => m.Id == meal.Id);
            _context.Meals.Update(dbEntry);
            _context.SaveChanges();
        }
    }
}

