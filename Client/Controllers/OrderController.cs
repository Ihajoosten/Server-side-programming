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
using Models.Order;
using static Domain.Order;

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
                    bool nextWeekStart = MealMethods.Week(model.Start) != (MealMethods.Week(DateTime.Now.Date) + 1) ? true : false;
                    bool nextWeekEnd = MealMethods.Week(model.End) != (MealMethods.Week(DateTime.Now.Date) + 1) ? true : false;

                    // To check if the begin date / end date is in the same week
                    if (MealMethods.Week(model.Start) != MealMethods.Week(model.End))
                    {
                        ModelState.AddModelError(string.Empty, "The given start date and end date were not in the same week!");
                        return View();
                    }
                    else if (model.Start > model.End)
                    {
                        ModelState.AddModelError(string.Empty, "You cannot choose an end data that is passed the given start date!");
                        return View();
                    }
                    //else if (nextWeekStart && nextWeekEnd)
                    //{
                    //    ModelState.AddModelError(string.Empty, "You cannot order for this week only for the next week after the current week!");
                    //    return View();
                    //}
                    else if (model.Start.DayOfWeek != DayOfWeek.Monday || model.End.DayOfWeek != DayOfWeek.Sunday)
                    {
                        ModelState.AddModelError(string.Empty, "You can only order for a full week!");
                        return View();
                    }
                    else
                    {
                        TempData["start"] = model.Start;
                        TempData["end"] = model.End;
                        return RedirectToAction("Order", "Order");
                    }
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

                if (MealMethods.Week(startDate) == MealMethods.Week(endDate))
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
                return View();
            }

            if (!_cart.IsValid())
            {
                ModelState.AddModelError(string.Empty, "You need to order at least 4 meals between monday and friday!");
                return View();
            }
            if (ModelState.IsValid)
            {
                Dictionary<Meal, MealSize> meals = new Dictionary<Meal, MealSize>();
                List<CartLine> lines = _cart.Lines;
                List<Dish> dishes = _dishService.GetDishes();
                Domain.Client client = _clientService.GetClientByEmail(User.Identity.Name);

                foreach (var item in model.CheckoutItems)
                {
                    foreach (var lineItem in _cart.Lines)
                    {
                        if (item.Key == lineItem.Meal.Id) meals.Add(lineItem.Meal, item.Value);
                    }
                }


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
                double total = _cart.ComputeTotalValue(lines);


                //Check meal sizes to obtain 20 % or decrement 20 % of total price
                foreach (var item in meals)
                {
                    if (item.Value == MealSize.Large)
                        total += (Domain.Extensions.MealMethods.GetMealPrice(item.Key) * 0.2);

                    if (item.Value == MealSize.Small)
                        total -= (Domain.Extensions.MealMethods.GetMealPrice(item.Key) * 0.2);
                }

                
                List<OrderMeal> orderMeals = new List<OrderMeal>();
                List<OrderMealDish> orderMealDishes = new List<OrderMealDish>();
                foreach (var item in meals)
                {
                    bool bdm = item.Key.DateValid == client.Birthday ? true : false;
                    foreach (var dish in item.Key.MealDishes)
                    {
                        orderMealDishes.Add(new OrderMealDish { Name = dish.Name, Price = dish.Price, MealId = item.Key.Id });
                    }
                    orderMeals.Add(new OrderMeal
                    {
                        MealId = item.Key.Id,
                        MealSize = item.Value,
                        Dishes = orderMealDishes,
                        MealDate = item.Key.DateValid,
                        birthdayMeal = bdm                        
                    });
                }


                Order order = new Order()
                {
                    Client = client,
                    Meals = orderMeals,
                    TotalPrice = total
                };

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