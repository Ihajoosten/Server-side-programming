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
using Cook.Models.Menu;
using System.Diagnostics;

namespace Cook.Controllers
{
    public class MenusController : Controller
    {
        private readonly CookDbContext _context;
        private readonly IMenuService _menuService;
        private readonly IMealService _mealService;
        private readonly IDishService _dishService;

        public MenusController(CookDbContext context, IMenuService menuService, IMealService mealService, IDishService dishService)
        {
            _context = context;
            _menuService = menuService;
            _mealService = mealService;
            _dishService = dishService;
        }

        // GET: Menus
        public IActionResult Index()
        {
            List<MenuMeals> menuMeals = new List<MenuMeals>();
            List<Meal> allMeals = new List<Meal>();

            var menus = _menuService.MenuMeal.ToList();
            var meals = _mealService.GetMeals();

            foreach (var item in menus)
            {
                menuMeals.Add(item);
            }
            foreach (var item in meals)
            {
                allMeals.Add(item);
            }

            ViewBag.MenuMeals = menuMeals;
            ViewBag.Meals = allMeals;
            return View(_menuService.GetMenus());
        }

        // GET: Menus/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null) throw new KeyNotFoundException();

            List<MenuMeals> menuMeals = new List<MenuMeals>();
            List<Meal> allMeals = new List<Meal>();

            var menus = _menuService.MenuMeal.ToList();
            var meals = _mealService.GetMeals();

            foreach (var item in menus)
            {
                menuMeals.Add(item);
            }
            foreach (var item in meals)
            {
                allMeals.Add(item);
            }

            ViewBag.MenuMeals = menuMeals;
            ViewBag.Meals = allMeals;

            var menu = _menuService.GetMenuById(id);

            if (menu == null) throw new KeyNotFoundException();

            return View(menu);
        }

        // GET: Menus/Create
        public IActionResult Create()
        {
            Dictionary<int, List<Meal>> dict = new Dictionary<int, List<Meal>>();

            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
                dict.Add((int)day, new List<Meal>());

            foreach (Meal meal in _mealService.GetMeals())
            {
                var day = meal.DateValid.DayOfWeek;
                dict[(int)day].Add(meal);
            }

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

            ViewBag.Dictionary = dict;

            return View();
        }

        // POST: Menus/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateMenuViewModel model)
        {
            Menu menu = new Menu() { Week = model.Week, Year = model.Year };
            Meal monday = _mealService.GetMeals().First(m => m.Id == model.Days[1]);
            Meal tuesday = _mealService.GetMeals().First(m => m.Id == model.Days[2]);
            Meal wednesday = _mealService.GetMeals().First(m => m.Id == model.Days[3]);
            Meal thursday = _mealService.GetMeals().First(m => m.Id == model.Days[4]);
            Meal friday = _mealService.GetMeals().First(m => m.Id == model.Days[5]);
            Meal saturday = _mealService.GetMeals().First(m => m.Id == model.Days[6]);
            Meal sunday = _mealService.GetMeals().First(m => m.Id == model.Days[0]);

            var meals = new Meal[] { monday, tuesday, wednesday, thursday, friday, saturday, sunday };
            _menuService.CreateMenu(menu, meals);
            return RedirectToAction(nameof(Index));
        }

        // GET: Menus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menu.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Week,Year")] Menu menu)
        {
            if (id != menu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.Id))
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
            return View(menu);
        }

        // GET: Menus/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<MenuMeals> menuMeals = new List<MenuMeals>();
            List<Meal> allMeals = new List<Meal>();

            var menus = _menuService.MenuMeal.ToList();
            var meals = _mealService.GetMeals();

            foreach (var item in menus)
            {
                menuMeals.Add(item);
            }
            foreach (var item in meals)
            {
                allMeals.Add(item);
            }

            ViewBag.MenuMeals = menuMeals;
            ViewBag.Meals = allMeals;

            var menu = _menuService.GetMenuById(id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var menu = _menuService.GetMenuById(id);
            _menuService.DeleteMenu(menu);
            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(int id)
        {
            return _menuService.Menu.Any(e => e.Id == id);
        }
    }
}
