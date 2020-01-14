using Domain;
using DomainServices;
using Microsoft.AspNetCore.Http;
using Domain.Extensions.Session;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Client.Models.Cart;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System;

namespace Client.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private Cart _cart;

        public CartController(Cart cart)
        {
            _cart = cart;

        }

        //public ViewResult Cart()
        //{
        //    return View();
        //}

        public ViewResult Cart(string returnUrl)
        {
            var dict = TempData["dict"];
            
            Debug.WriteLine("-------------- Dict ------------------------> " + dict);
            return View(new CartViewModel
            {
                Cart = GetCart(),
                ReturnUrl = "/Cart/Cart"
            });
        }

        public RedirectToActionResult AddToCart(Meal meal, DayOfWeek dayOfWeek)
        {
            if (meal != null)
            {
                _cart.AddItem(meal, dayOfWeek);
                SaveCart(_cart);
            }
            return RedirectToAction("Cart", "Cart");
        }

        //public RedirectToActionResult RemoveFromCart(int mealId,
        //    string returnUrl)
        //{
        //    Meal meal = _mealService.Meal
        //        .FirstOrDefault(p => p.Id == mealId);
        //    if (meal != null)
        //    {
        //        Cart cart = GetCart();
        //        cart.RemoveLine(meal);
        //        SaveCart(cart);
        //    }
        //    return RedirectToAction("Index", new { returnUrl });
        //}


        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
    }
}
