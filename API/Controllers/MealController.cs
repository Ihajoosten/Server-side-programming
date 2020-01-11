using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Domain;
using DomainServices;
using API_level_2.Models;

namespace API_level_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public ActionResult<IEnumerable<MealViewModel>> GetMeals()
        {
            var meals = _service.GetMeals();
            var mealModels = new List<MealViewModel>();
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
           
            return mealModels;
        }

        // GET: api/Meal/5
        [HttpGet("{id}")]
        public ActionResult<Meal> GetMealById(int id)
        {
            var meal = _service.GetMealById(id);
            if (meal == null) return NotFound();

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

            if (model != null)
            {
                return Ok(model);
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/Meal
        [HttpPut]
        public ActionResult UpdateMeal(Meal meal)
        {
            throw new NotImplementedException();
        }

        // PUT: api/Meal/5
        [HttpPut("{id}")]
        public IActionResult UpdateMealById(int id, Meal meal)
        {
            throw new NotImplementedException();
        }

        // POST: api/Meal
        [HttpPost]
        public ActionResult PostMeal(Meal meal)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/Meal
        [HttpDelete]
        public ActionResult DeleteMeal(Meal meal)
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
