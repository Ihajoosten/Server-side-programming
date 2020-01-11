using Client.Extentsions.Dish;
using Client.Models.Account;
using Domain;
using DomainServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Client.Extentsions.Meal;

namespace Client.Controllers
{
    public class OrderController : Controller
    {
        private readonly IMealService _mealService;

        public OrderController(IMealService service) => _mealService = service;


        public IActionResult Checkout()
        {
            return View();
        }

        /** WERKT **/
        public IActionResult ChooseWeek() => View();

        /** WERKT **/
        [HttpPost]
        public IActionResult ChooseWeek(ChooseWeekViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["start"] = model.Start;
                TempData["end"] = model.End;
                return RedirectToAction("Order", "Order");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        /** WERKT **/
        public async Task<IActionResult> Order()
        {
            // Fetching Dishes into local JArray
            JArray dishArray = await DishMethods.GetDishes();
            // Converting JArray items to Collection object of given type
            List<Dish> allDishes = dishArray.ToObject<List<Dish>>();

            Dictionary<int, List<Meal>> dict = new Dictionary<int, List<Meal>>();

            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
                dict.Add((int)day, new List<Meal>());

            DateTime startDate = DateTime.Parse(TempData["start"].ToString());

            var meals = await MealMethods.GetAllWeekMeals(startDate);

            foreach (var meal in meals)
            {
                var day = meal.DateValid.DayOfWeek;
                dict[(int)day].Add(meal);
            }

            List<MealDishes> mealDishes = new List<MealDishes>();
    
            var dishes = _mealService.MealDish.ToList();

            foreach (var item in dishes)
            {
                mealDishes.Add(item);
            }
           
            if (!User.Identity.IsAuthenticated)
            {
                ViewBag.MealDishes = mealDishes;
                ViewBag.Dishes = allDishes;
                ViewBag.Dictionary = dict;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public IActionResult Order(OrderMealViewModel model)
        {
            // posting new order and redirect to order detail with details of the meal dishes when state is valid
            RedirectToAction("OrderDetail", "Order");
            return View(model);
        }


    }
}