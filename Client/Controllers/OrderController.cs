using Client.Models.Account;
using Domain;
using DomainServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Client.Extentsions.Meal;
using Microsoft.AspNetCore.Authorization;
using Client.Models.Order;
using System.Diagnostics;
using Domain.Dishsize;
using Models.Order;

namespace Client.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IMealService _mealService;
        private readonly IClientService _clientService;
        private readonly IDishService _dishService;
        private readonly IOrderService _orderService;
        private readonly Cart _cart;

        public OrderController(IMealService service, IDishService dishService, IClientService clientService, IOrderService orderService, Cart cart)
        {
            _mealService = service;
            _dishService = dishService;
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
                DateTime startDate = DateTime.Parse(TempData["start"].ToString());
                DateTime endDate = DateTime.Parse(TempData["end"].ToString());

                // To check if the begin date / end date is in the same week
                if (MealMethods.Week(startDate) != MealMethods.Week(endDate))
                {
                    ModelState.AddModelError(string.Empty, "The given start date and end date were not in the same week!");
                    return View();
                }
                else if (startDate > endDate)
                {
                    ModelState.AddModelError(string.Empty, "You cannot choose an end data that is passed the given startdate!");
                    return View();
                }
                else if (MealMethods.Week(startDate) == MealMethods.Week(endDate))
                {
                    // For each meal in retrieved meals add it to the dictionary by specific day of week
                    foreach (var meal in GetAllWeekMeals(startDate))
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

                ModelState.AddModelError(string.Empty, "Application error: could not load the meals");
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
                    double mealPrice = 0;
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

        public ViewResult Checkout()
        {
            ViewBag.Lines = _cart.Lines;
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(CheckoutViewModel model)
        {
            ViewBag.Lines = _cart.Lines;


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
                Dictionary<Meal, DishSize> meals = new Dictionary<Meal, DishSize>();
                foreach (var item in model.CheckoutItems)
                {
                    foreach (var lineItem in _cart.Lines)
                    {
                        if (item.Key == lineItem.Meal.Id) meals.Add(lineItem.Meal, item.Value);
                    }
                }


                // Get Curren Client 
                Domain.Client client = _clientService.GetClientByEmail(User.Identity.Name);

                List<CartLine> lines = _cart.Lines;
                List<Dish> dishes = _dishService.GetDishes();
                foreach (var dish in dishes)
                {
                    foreach (var meal in lines)
                    {
                        foreach (var mealDish in meal.Meal.Dishes)
                        {
                            if (mealDish.DishId == dish.Id)
                            {
                                meal.Meal.MealDishes.Add(dish);
                            }
                        }
                    }
                }
                // Get Total price excluded from discounts
                double total = _cart.ComputeTotalValue(lines);

                //Check meal sizes to obtain 20 % or decrement 20 % of total price
                foreach (var item in meals)
                {
                    if (item.Value == DishSize.Large) total += (Domain.Extensions.MealMethods.GetMealPrice(item.Key) * 0.2);
                    if (item.Value == DishSize.Small) total -= (Domain.Extensions.MealMethods.GetMealPrice(item.Key) * 0.2);
                }

                // Check if one of the cart item is on the clients birthday
                foreach (var item in lines)
                {
                    if (client.Birthday == item.Meal.DateValid) total -= Domain.Extensions.MealMethods.GetMealPrice(item.Meal);
                }

                //// Check if the client has 15 orders already to give 10% discount
                var orderList = _orderService.GetOrders();
                int clientOrders = 0;
                foreach (var item in orderList)
                {
                    if (item.ClientId == client.Id) clientOrders++;
                }

                bool orderBool = clientOrders % 15 == 0 ? true : false;
                if (orderBool) total *= 0.9;

                // Setup a new OrderInvoice
                Order order = new Order
                {
                    OrderDate = DateTime.Now.Date,
                    ClientId = client.Id,
                    OrderMeals = meals,
                    TotalPrice = total
                };

                order.Client = client;
                _orderService.CreateOrder(order);
                client.Orders.Add(order);
                _cart.Clear();
                return RedirectToAction("Index", "Home");
            }
            return View();

        }



        public IEnumerable<Meal> GetAllWeekMeals(DateTime date)
        {
            List<Meal> allMeals = _mealService.GetMeals();

            return allMeals.Where(m => MealMethods.Week(m.DateValid) == MealMethods.Week(date));
        }
    }
}