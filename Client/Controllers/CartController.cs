using Domain;
using DomainServices;
using Microsoft.AspNetCore.Http;
using Domain.Extensions.Session;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Models.Cart;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System;

namespace Client.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private Cart _cart;
        private readonly IMealService _mealService;
        private readonly IDishService _dishService;

        public CartController(Cart cart, IMealService service, IDishService dishService)
        {
            _cart = cart;
            _mealService = service;
            _dishService = dishService;
        }


        public ViewResult Cart()
        {
            ViewBag.Dishes = _dishService.GetDishes();
            return View(new CartViewModel
            {
                Cart = _cart,
                ReturnUrl = "Cart/Cart/"
            });
        }

        public RedirectToActionResult AddToCart(int mealId, string returnUrl)
        {
            Meal meal = _mealService.GetMealById(mealId);
            if (meal != null)
            {
                _cart.AddItem(meal, meal.DateValid.DayOfWeek);
                SaveCart(_cart);
            }
            return RedirectToAction("Cart", "Cart");
        }

        public RedirectToActionResult RemoveFromCart(int mealId)
        {
            Meal meal = _mealService.GetMealById(mealId);
            if (meal != null)
            {
                _cart.RemoveLine(meal);
                SaveCart(_cart);
            }
            return RedirectToAction("Cart", "Cart");
        }

        public RedirectToActionResult RemoveDish(int mealId, int dishId)
        {
            Meal meal = _mealService.GetMealById(mealId);
            Dish dish = _dishService.GetDishById(dishId);
            if (meal != null && dish != null)
            {
                _cart.RemoveDishFromMeal(meal, dish);
                SaveCart(_cart);
            }

            return RedirectToAction("Cart", "Cart");
        }


        private Cart GetCart() { Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart(); return cart; }
        private void SaveCart(Cart cart) { HttpContext.Session.SetJson("Cart", cart); }
    }
}
