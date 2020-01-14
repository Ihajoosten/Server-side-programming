using Client.Extentsions.Dish;
using Client.Models.Account;
using Domain;
using DomainServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Extentsions.Meal;
using Microsoft.AspNetCore.Authorization;
using Client.Models.Order;
using System.Diagnostics;
using Client.Models.Cart;
using Domain.Extensions.Session;

namespace Client.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IMealService _mealService;
        private readonly IDishService _dishService;
        private readonly IClientService _clientService;
        private readonly Cart _cart;

        public OrderController(IMealService service, IDishService dishService, IClientService clientService, Cart cart)
        {
            _mealService = service;
            _dishService = dishService;
            _clientService = clientService;
            _cart = cart;
        }

        /** WERKT **/
        public IActionResult ChooseWeek() => View();

        /** WERKT **/
        [HttpPost]
        public IActionResult ChooseWeek(ChooseWeekViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                //TempData["start"] = model.Start;
                //TempData["end"] = model.End;
                return RedirectToAction("Order", "Order");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        /** WERKT **/
        public IActionResult Order()
        {
            if (User.Identity.IsAuthenticated)
            {

                //// Fetching Dishes into local JArray
                //JArray dishArray = await DishMethods.GetDishes();
                //// Converting JArray items to Collection object of given type
                //List<Dish> allDishes = dishArray.ToObject<List<Dish>>();

                List<Dish> allDishes = _dishService.GetDishes();


                Dictionary<int, List<Meal>> dict = new Dictionary<int, List<Meal>>();

                foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
                    dict.Add((int)day, new List<Meal>());

                // Parsing the given dates through TempData
                DateTime startDate = DateTime.Parse("2020-01-13");
                DateTime endDate = DateTime.Parse("2020-01-19");

                IEnumerable<Meal> meals = null;
                // To check if the begin date / end date is in the same week
                if (MealMethods.Week(startDate) == MealMethods.Week(endDate)) meals = (IEnumerable<Meal>)GetAllWeekMeals(startDate);

                // For each meal in retrieved meals add it to the dictionary by specific day of week
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

                //Domain.Client client = _clientService.GetClientByEmail(User.Identity.Name);
                //bool salt = client.Salt;
                //bool diabetes = client.Diabetes;
                //bool gluten = client.Gluten;

                //bool mealSalt = true;
                //bool mealDiabetes = true;
                //bool mealGluten = true;

                //Dictionary<Meal, List<bool>> mealRestriction = new Dictionary<Meal, List<bool>>();
                //bool available = true;
                //for (int i = 0; i < dict.Count; i++)
                //{
                //    foreach (var meal in dict[i])
                //    {
                //        mealRestriction.Add((Meal)meal, new List<bool>());

                //        foreach (var mealDish in meal.Dishes)
                //        {
                //            foreach (var dish in allDishes)
                //            {
                //                if (dish.Id == mealDish.DishId)
                //                {
                //                    if (dish.Restriction == DietRestriction.Saltless) mealRestriction[(Meal)meal].Add(mealSalt);
                //                    if (dish.Restriction == DietRestriction.Gluten) mealRestriction[(Meal)meal].Add(mealGluten);
                //                    if (dish.Restriction == DietRestriction.Diabetes) mealRestriction[(Meal)meal].Add(mealDiabetes);
                //                }
                //            }
                //        }
                //    }
                //}

                ViewBag.MealDishes = mealDishes;
                ViewBag.Dishes = allDishes;
                ViewBag.Dictionary = dict;
                //ViewBag.Available = available;
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
            if (User.Identity.IsAuthenticated)
            {
                List<Meal> allMeals = _mealService.GetMeals();
                if (ModelState.IsValid)
                {
                    foreach (var item in model.DayMeals)
                    {
                        Debug.WriteLine("DICTIONARY ITEMS -------------------------------> " + item.Key + ", " + item.Value);
                        foreach (var meal in allMeals)
                        {
                            if (meal.Id == item.Value && item.Value != 0) _cart.AddItem(meal, (DayOfWeek)item.Key);
                        }
                    }
                    foreach (var item in _cart.Lines)
                    {
                        Debug.WriteLine("CART ITEMS -------------------------------> " + "------- Day Of Week------>" + item.DayOfWeek + " ------Meal Id-> " + item.Meal.Id)
                    }
                    return RedirectToAction("Cart", "Cart");
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public IEnumerable<Meal> GetAllWeekMeals(DateTime date)
        {
            List<Meal> allMeals = _mealService.GetMeals();
            return allMeals.Where(m => MealMethods.Week(m.DateValid) == MealMethods.Week(date));
        }
    }
}