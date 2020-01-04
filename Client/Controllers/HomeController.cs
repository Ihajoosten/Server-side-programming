using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Client.Models;
using Client.Extentsions.Dish;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            // Fetching Dishes into local JArray
            JArray array = await DishMethods.GetDishes();
            // Converting JArray items to Collection object of given type
            IList<Domain.Dish> dishes = array.ToObject<IList<Domain.Dish>>();

            // Fetching Dish into local JObject
            JObject objct = await DishMethods.GetDishById(1);
            // Converting JObject item to new Type Object
            Domain.Dish dish = objct.ToObject<Domain.Dish>();
                        
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
