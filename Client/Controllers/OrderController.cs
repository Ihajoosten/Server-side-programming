using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class OrderController : Controller
    {
        //public IActionResult Cart()
        //{
        //    return View();
        //}

        public IActionResult OrderDetail()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult ChooseWeek()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            } else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public IActionResult Order()
        {
            if (!User.Identity.IsAuthenticated)
            {
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
            RedirectToAction("CheckOut", "Order");
            return View(model);
        }


    }
}
