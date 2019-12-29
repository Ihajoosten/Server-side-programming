    using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cook.Models;
using DomainServices;
using Domain;

namespace Cook.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDishService _dishService;
        private readonly IMealService _mealService;
        private readonly IMenuService _menuService;

        public HomeController(IDishService dishService, IMealService mealService, IMenuService menuService)
        {
            _dishService = dishService;
            _mealService = mealService;
            _menuService = menuService;
        }

        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Dashboard", "Home");
            }
            return View();
        }

        public ViewResult Dashboard()
        {
            List<Menu> menus = _menuService.GetMenus();
            List<Meal> meals = _mealService.GetMeals();
            List<Dish> dishes = _dishService.GetDishes();
            ViewBag.Menus = menus;
            ViewBag.Meals = meals;
            ViewBag.Dishes = dishes;
            return View("Dashboard");
        }

        public ViewResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
