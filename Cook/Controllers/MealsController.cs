using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using Infrastructure.Cook;
using DomainServices;
using Cook.Models;

namespace Cook.Controllers
{
    public class MealsController : Controller
    {
        private readonly CookDbContext _context;
        private readonly IDishService _dishService;
        private readonly IMealService _mealService;

        public MealsController(CookDbContext context, IDishService dishService, IMealService mealService)
        {
            _context = context;
            _dishService = dishService;
            _mealService = mealService;
        }

        // GET: Meals
        public IActionResult Index()
        {
            //List<Dish> selectedDishes = new List<Dish>();
            //foreach (var dish in selectedDishes)
            //{
            //    var optionalDish = _dishService.Dish.Where(d => d.Id == dish.Id).First();
            //    if (optionalDish != null) selectedDishes.Add(optionalDish);
            //}

            List<MealDishes> mealDishes = new List<MealDishes>();
            List<Dish> allDishes = new List<Dish>();

            var dishes = _context.MealDish.ToList();
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

        // GET: Meals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meal.FindAsync(id);
            if (meal == null)
            {
                return NotFound();
            }
            return View(meal);
        }

        // POST: Meals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateValid")] Meal meal)
        {
            if (id != meal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealExists(meal.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(meal);
        }

        // GET: Meals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        // POST: Meals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meal = await _context.Meal.FindAsync(id);
            _context.Meal.Remove(meal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MealExists(int id)
        {
            return _context.Meal.Any(e => e.Id == id);
        }
    }
}
