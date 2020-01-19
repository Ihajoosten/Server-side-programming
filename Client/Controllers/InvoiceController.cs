using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Client.Extentsions.Meal;
using Domain;
using DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Client.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IOrderService _orderService;

        public InvoiceController(IOrderService orderservice, IClientService clientService)
        {
            _clientService = clientService;
            _orderService = orderservice;
        }

        public async Task<IActionResult> Index()
        {
            // Fetching Dishes into local JArray
            JArray mealArray = await MealMethods.GetMeals();
            // Converting JArray items to Collection object of given type
            List<Meal> meals = mealArray.ToObject<List<Meal>>();

            Domain.Client client = _clientService.GetClientByEmail(User.Identity.Name);
            List<Order> orders = new List<Order>();
            foreach (var item in _orderService.GetOrders())
            {
                if (item.Client == client) orders.Add(item);
            }

            List<OrderMeal> orderMeals = _orderService.GetOrderMeals();
            List<OrderMealDish> mealDishes = _orderService.GetOrderMealDishes();          


            double birthdayDiscount = 0;
            ViewBag.OrderMeals = orderMeals;
            ViewBag.MealDishes = mealDishes;
            ViewBag.Meals = meals;
            ViewBag.Birthday = birthdayDiscount;
            ViewBag.Client = client;
            return View(orders);
        }
    }
}