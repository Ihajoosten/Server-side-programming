using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Domain;
using DomainServices;
using Cook.Models;

namespace Cook.Controllers
{
    public class MealsController : Controller
    {
        private readonly IDishService _dishService;
        private readonly IMealService _mealService;

        public MealsController(IDishService dishService, IMealService mealService)
        {
            _dishService = dishService;
            _mealService = mealService;
        }

        // GET: Meals
        public IActionResult Index()
        {
            List<MealDishes> mealDishes = new List<MealDishes>();
            List<Dish> allDishes = new List<Dish>();

            var dishes = _mealService.MealDish.ToList();
            var x = _dishService.GetDishes();

            foreach (var item in dishes)
            {
                mealDishes.Add(item);
            }
            foreach (var item in x)
            {
                allDishes.Add(item);
            }

            ViewBag.MealDishes = mealDishes;
            ViewBag.Dishes = allDishes;
            return View(_mealService.GetMeals());
        }

        // GET: Meals/Create
        public IActionResult Create()
        {
            List<Dish> starters = new List<Dish>();
            List<Dish> mains = new List<Dish>();
            List<Dish> desserts = new List<Dish>();
            var types = new List<String>();

            foreach (DishType type in Enum.GetValues(typeof(DishType)))
            {
                types.Add(type.ToString());
            }
            foreach (Dish dish in _dishService.GetDishes())
            {
                if (dish.Type.ToString() == types[0]) starters.Add(dish);
                else if (dish.Type.ToString() == types[1]) mains.Add(dish);
                else if (dish.Type.ToString() == types[2]) desserts.Add(dish);
            }
            ViewBag.Starters = starters;
            ViewBag.Mains = mains;
            ViewBag.Desserts = desserts;
            return View();
        }

        // POST: Meals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateMealModel model)
        {
            if (ModelState.IsValid)
            {
                Meal meal = new Meal() { DateValid = model.DateForMeal };
                Dish start = _dishService.Dish.First(d => d.Id == model.StarterId);
                Dish main = _dishService.Dish.First(d => d.Id == model.MainId);
                Dish dessert = _dishService.Dish.First(d => d.Id == model.DessertId);
                var dishes = new Dish[] { start, main, dessert };
                _mealService.CreateMeal(meal, dishes);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Meals/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                throw new KeyNotFoundException();
            }

            var meal = _mealService.GetMealById(id);
            var mealDishes = _mealService.MealDish.Select(md => md.MealId == id);
            if (meal == null)
            {
                throw new KeyNotFoundException();
            }

            List<Dish> starters = new List<Dish>();
            List<Dish> mains = new List<Dish>();
            List<Dish> desserts = new List<Dish>();
            var types = new List<String>();

            foreach (DishType type in Enum.GetValues(typeof(DishType)))
            {
                types.Add(type.ToString());
            }
            foreach (Dish dish in _dishService.GetDishes())
            {
                if (dish.Type.ToString() == types[0]) starters.Add(dish);
                else if (dish.Type.ToString() == types[1]) mains.Add(dish);
                else if (dish.Type.ToString() == types[2]) desserts.Add(dish);
            }
            ViewBag.Starters = starters;
            ViewBag.Mains = mains;
            ViewBag.Desserts = desserts;

            List<MealDishes> mealDish = new List<MealDishes>();

            foreach (var item in _mealService.GetAllMealDishes())
            {
                if (item.MealId == id) mealDish.Add(item);
            }

            Dish starter = new Dish();
            Dish mainer = new Dish();
            Dish dessert = new Dish();

            foreach (var item in mealDish)
            {
                if (item.Dish.Type.ToString() == types[0]) starter = _dishService.GetDishById(item.DishId);
                if (item.Dish.Type.ToString() == types[1]) mainer = _dishService.GetDishById(item.DishId);
                if (item.Dish.Type.ToString() == types[2]) dessert = _dishService.GetDishById(item.DishId);
            }

            if (starter == null) starter.Id = 0;
            if (mainer == null) mainer.Id = 0;
            if (dessert == null) dessert.Id = 0;


            EditMealModel model = new EditMealModel()
            {
                Id = meal.Id,
                DateForMeal = meal.DateValid,
                StarterId = starter.Id,
                MainId = mainer.Id,
                DessertId = dessert.Id
            };

            return View(model);
        }

        // POST: Meals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditMealModel model)
        {
            if (ModelState.IsValid)
            {
                Meal meal = new Meal() { Id = model.Id, DateValid = model.DateForMeal };
                Dish start = _dishService.Dish.First(d => d.Id == model.StarterId);
                Dish main = _dishService.Dish.First(d => d.Id == model.MainId);
                Dish dessert = _dishService.Dish.First(d => d.Id == model.DessertId);
                var dishes = new Dish[] { start, main, dessert };


                _mealService.UpdateMeal(meal, dishes);

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Meals/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                throw new KeyNotFoundException();
            }

            var types = new List<String>();

            foreach (DishType type in Enum.GetValues(typeof(DishType)))
            {
                types.Add(type.ToString());
            }

            var mealDishes = new List<Dish>();

            foreach (var item in _mealService.GetAllMealDishes())
            {
                if (item.MealId == id)
                {
                    foreach (var dish in _dishService.GetDishes())
                    {
                        if (item.DishId == dish.Id && dish.Type.ToString() == types[0]) mealDishes.Add(dish);
                        else if (item.DishId == dish.Id && dish.Type.ToString() == types[1]) mealDishes.Add(dish);
                        else if (item.DishId == dish.Id && dish.Type.ToString() == types[2]) mealDishes.Add(dish);
                    }
                }
            }

            ViewBag.MealDishes = mealDishes;

            var meal = _mealService.Meal.FirstOrDefault(m => m.Id == id);

            if (meal == null)
            {
                throw new NullReferenceException();
            }
            return View(meal);
        }

        // POST: Meals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var meal = _mealService.GetMealById(id);
            _mealService.DeleteMeal(meal);
            return RedirectToAction(nameof(Index));
        }

        private bool MealExists(int id)
        {
            return _mealService.Meal.Any(e => e.Id == id);
        }

        public IActionResult Details(int? id)
        {
            List<MealDishes> mealDishes = new List<MealDishes>();
            List<Dish> allDishes = new List<Dish>();

            var dishes = _mealService.MealDish.ToList();
            var x = _dishService.GetDishes();

            foreach (var item in dishes)
            {
                mealDishes.Add(item);
            }
            foreach (var item in x)
            {
                allDishes.Add(item);
            }

            ViewBag.MealDishes = mealDishes;
            ViewBag.Dishes = allDishes;

            var meal = _mealService.GetMealById(id);
            return View(meal);
        }
    }
}
