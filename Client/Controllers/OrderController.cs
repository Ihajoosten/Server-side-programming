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

namespace Client.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IMealService _mealService;
        private readonly IClientService _clientService;
        private readonly IOrderService _orderService;
        private readonly Cart _cart;

        public OrderController(IMealService service, IClientService clientService, IOrderService orderService, Cart cart)
        {
            _mealService = service;
            _clientService = clientService;
            _orderService = orderService;
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
                if (ModelState.IsValid)
                {
                    TempData["start"] = model.Start;
                    TempData["end"] = model.End;
                    return RedirectToAction("Order", "Order");
                }

                ModelState.AddModelError(string.Empty, "Please fill in both dates to continue");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        /** WERKT **/
        public async Task<IActionResult> Order()
        {
            if (User.Identity.IsAuthenticated)
            {

                // Fetching Dishes into local JArray
                JArray dishArray = await DishMethods.GetDishes();
                // Converting JArray items to Collection object of given type
                List<Dish> allDishes = dishArray.ToObject<List<Dish>>();

                Dictionary<int, List<Meal>> dict = new Dictionary<int, List<Meal>>();

                foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
                    dict.Add((int)day, new List<Meal>());

                // Parsing the given dates through TempData
                DateTime startDate = DateTime.Parse(TempData["start"].ToString());
                DateTime endDate = DateTime.Parse(TempData["end"].ToString());

                IEnumerable<Meal> meals = null;
                // To check if the begin date / end date is in the same week
                if (MealMethods.Week(startDate) == MealMethods.Week(endDate)) meals = await MealMethods.GetAllWeekMeals(startDate);

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
        public async Task<IActionResult> Order(OrderMealViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                // Fetching Dishes into local JArray
                JArray mealArray = await MealMethods.GetMeals();
                // Converting JArray items to Collection object of given type
                List<Meal> allMeals = mealArray.ToObject<List<Meal>>();

                if (ModelState.IsValid)
                {
                    foreach (var item in model.DayMeals)
                    {

                        foreach (var meal in allMeals)
                        {
                            if (meal.Id == item.Value && item.Value != 0) _cart.AddItem(meal, (DayOfWeek)item.Key);

                        }
                    }
                    return RedirectToAction("Cart", "Cart");
                }
                return RedirectToAction("ChooseWeek", "Order");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ViewResult CheckOut()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (!_cart.Lines.Any())
            {
                ModelState.AddModelError(string.Empty, "Sorry, your shoppingcart is empty!");
            }

            if (!_cart.IsValid())
            {
                ModelState.AddModelError(string.Empty, "You need to order at least 4 meals between monday and friday!");
            }
            if (ModelState.IsValid)
            {
                Domain.Client client = _clientService.GetClientByEmail(User.Identity.Name);
                order.Client = client;
                _orderService.CreateOrder(order);
                client.Orders.Add(order);
                _cart.Clear();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(order);
            }
        }
    }
}