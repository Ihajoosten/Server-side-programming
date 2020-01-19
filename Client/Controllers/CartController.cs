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
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Client.Extentsions.Dish;
using System.Collections.Generic;
using Client.Extentsions.Meal;

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


        public async Task<ViewResult> Cart()
        {
            // Fetching Dishes into local JArray
            JArray dishArray = await DishMethods.GetDishes();
            // Converting JArray items to Collection object of given type
            List<Dish> dishes = dishArray.ToObject<List<Dish>>();

            ViewBag.Dishes = dishes;
            return View(new CartViewModel
            {
                Cart = _cart,
                ReturnUrl = "Cart/Cart/"
            });
        }

        public RedirectToActionResult AddToCart(int mealId, string returnUrl)
        {
            //JObject mealObject = await MealMethods.GetMealById(mealId);
            //Meal meal = mealObject.ToObject<Meal>();

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
            //JObject mealObject = await MealMethods.GetMealById(mealId);
            //Meal meal = mealObject.ToObject<Meal>();

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
            //JObject mealObject = await MealMethods.GetMealById(mealId);
            //Meal meal = mealObject.ToObject<Meal>();

            //JObject dishObject = await DishMethods.GetDishById(dishId);
            //Dish meal = dishObject.ToObject<Dish>();

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
