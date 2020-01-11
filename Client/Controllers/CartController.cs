using Domain;
using DomainServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Client.Extentsions.Meal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class CartController : Controller
    {
        private readonly Cart _cart;

        public CartController(Cart cart)
        {
            _cart = cart;
        }
        public async Task<RedirectToActionResult> AddToCart(int id, DayOfWeek dayOfWeek)
        {
            var result = await MealMethods.GetMealById(id);

            Meal meal = result.ToObject<Meal>();

            if (meal != null)
            {
                _cart.AddItem(meal, dayOfWeek);
            }
            return RedirectToAction("Index", "Cart");
        }

        //public ViewResult Index(string returnUrl)
        //{
        //    return View(new CartIndexViewModel
        //    {
        //        Cart = _cart,
        //        ReturnUrl = returnUrl
        //    });
        //}

    }
}
