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
using Client.Models.Order;

namespace Client.Controllers
{
    public class OrderController : Controller
    {
        private readonly IMealService _mealService;

        public OrderController(IMealService service) => _mealService = service;

        public IActionResult OrderDetail()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult ConfirmCheckOut()
        {
            return View();
        }

        /** WERKT **/
        public ViewResult ChooseWeek() => View();

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
        public async Task<ViewResult> Order()
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

            ViewBag.MealDishes = mealDishes;
            ViewBag.Dishes = allDishes;
            ViewBag.Dictionary = dict;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Order(OrderMealViewModel model)
        {

            foreach (var item in model.DayMeals)
            {
                Debug.WriteLine("----------KEY---------> " + item.Key.ToString() + " " + "----------VALUE-------> " + item.Value.ToString());
            }
            // posting new order and redirect to order detail with details of the meal dishes when state is valid
            //RedirectToAction("OrderDetail", "Order");
            //return View(model);
            return View(model);
        }


    }
}