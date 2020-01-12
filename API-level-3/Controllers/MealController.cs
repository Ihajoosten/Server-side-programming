using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Domain;
using DomainServices;
using API_level_3.Models;
using System.Net;
using Halcyon.Web.HAL;
using Halcyon.HAL;


namespace API_level_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/hal+json")]
    public class MealController : ControllerBase
    {
        private readonly IMealService _service;
        private readonly IDishService _dishService;

        public MealController(IMealService service, IDishService dishService)
        {
            _service = service;
            _dishService = dishService;
        }

        // GET: api/Meal
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult<List<Meal>> GetMeals()
        {
            List<Meal> meals = _service.GetMeals();
            List<MealViewModel> mealModels = new List<MealViewModel>();
            meals.ForEach(m =>
            {
                var dishes = new List<Dish>();
                foreach (var md in m.Dishes)
                {
                    dishes.Add(_dishService.GetDishById(md.DishId));
                }
                var model = new MealViewModel { DateValid = m.DateValid, Id = m.Id, Dishes = dishes };
                mealModels.Add(model);
            });

            int count = 0;
            foreach (var item in mealModels)
            {
                foreach (var dish in item.Dishes)
                {
                    count++;
                }
            }

            var data = new
            {
                MealCount = mealModels.Count,
                IncludedDishes = count
            };

            var response = new HALResponse(data)
                .AddLinks(new Link[] { new Link("self", "https://easy-meal-sswp-api.azurewebsites.net/api/Dish/api/Meals") })
                .AddEmbeddedCollection("meals", mealModels, new Link[]
                {
                    new Link("self", "https://easy-meal-sswp-api.azurewebsites.net/api/Dish/api/Meal/{Id}")
                });

            return Ok(response);
        }

        // GET: api/Meal/5
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetMealById(int id)
        {

            Meal meal = _service.GetMealById(id);
            if (meal != null)
            {
                List<MealDishes> mealDishes = new List<MealDishes>();
                var dishes = _service.GetAllMealDishes();

                foreach (var dish in dishes)
                {
                    if (dish.MealId == meal.Id) mealDishes.Add(dish);
                }

                MealViewModel model = new MealViewModel()
                {
                    Id = meal.Id,
                    DateValid = meal.DateValid
                };

                foreach (var item in mealDishes)
                {
                    var dish = _dishService.GetDishById(item.DishId);
                    if (dish != null) model.Dishes.Add(dish);
                }

                Dish start = new Dish();
                Dish main = new Dish();
                Dish dessert = new Dish();

                foreach (var item in model.Dishes)
                {
                    if (item.Type == DishType.Starter) start = item;
                    if (item.Type == DishType.Main) main = item;
                    if (item.Type == DishType.Dessert) dessert = item;

                }

                Meal returnMeal = new Meal { Id = meal.Id, DateValid = meal.DateValid };
                return this.HAL(returnMeal, new Link[] {
                    new Link("self", "https://easy-meal-sswp-api.azurewebsites.net/api/Meal/" + id, "Meal date: " + meal.DateValid.ToString("dd MMM yyyy")),
                    new Link("starter", "https://easy-meal-sswp-api.azurewebsites.net/api/Dish/" + start.Id, "Dish name: " + start.Name),
                    new Link("main", "https://easy-meal-sswp-api.azurewebsites.net/api/Dish/" + main.Id, "Dish name: " + main.Name),
                    new Link("dessert", "https://easy-meal-sswp-api.azurewebsites.net/api/Dish/" + dessert.Id, "Dish name: " + dessert.Name)
                });

            }
            return Ok();
        }

        // PUT: api/Meal
        [HttpPut]
        public ActionResult PutUpdateMeal()
        {
            throw new NotImplementedException();
        }

        // PUT: api/Meal/5
        [HttpPut("{id}")]
        public IActionResult PutUpdateMealById(int id)
        {
            throw new NotImplementedException();
        }

        // PATCH: api/Meal
        [HttpPatch]
        public ActionResult PatchUpdateMeal()
        {
            throw new NotImplementedException();
        }

        // PATCH: api/Meal/5
        [HttpPatch("{id}")]
        public IActionResult PatchUpdateMealById(int id)
        {
            throw new NotImplementedException();
        }

        // POST: api/Meal
        [HttpPost]
        public ActionResult PostMeal()
        {
            throw new NotImplementedException();
        }

        // DELETE: api/Meal
        [HttpDelete]
        public ActionResult DeleteMeal()
        {
            throw new NotImplementedException();
        }

        // DELETE: api/Meal/5
        [HttpDelete("{id}")]
        public ActionResult DeleteMealById(int id)
        {
            throw new NotImplementedException();
        }


    }
}
