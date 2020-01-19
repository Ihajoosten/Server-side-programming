using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IOrderService _orderService;
        private readonly IMealService _mealService;

        public InvoiceController(IOrderService orderservice, IClientService clientService, IMealService mealService)
        {
            _clientService = clientService;
            _orderService = orderservice;
            _mealService = mealService;
        }

        public IActionResult Index()
        {
            Domain.Client client = _clientService.GetClientByEmail(User.Identity.Name);
            List<Order> orders = new List<Order>();
            foreach (var item in _orderService.GetOrders())
            {
                if (item.Client == client) orders.Add(item);
            }

            List<OrderMeal> orderMeals = _orderService.GetOrderMeals();
            List<OrderMealDish> mealDishes = _orderService.GetOrderMealDishes();
            List<Meal> meals = _mealService.GetMeals();
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