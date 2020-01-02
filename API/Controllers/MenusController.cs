using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Domain;
using DomainServices;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMealService _service;
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService, IMealService service)
        {
            _service = service;
            _menuService = menuService;
        }

        // GET: api/Menu
        [HttpGet]
        public ActionResult<IEnumerable<MenuViewModel>> GetMenus()
        {            
            var menus = _menuService.GetMenus();
            var menuMeals = new List<MenuViewModel>();
            menus.ForEach(m =>
            {
                var meals = new List<Meal>();
                foreach (var mm in m.Meals)
                {
                    meals.Add(_service.GetMealById(mm.MealId));
                }             
                
                var model = new MenuViewModel { Id = m.Id, Week = m.Week, Year = m.Year, Meals = meals };
                               
                menuMeals.Add(model);

            });
            return Ok(menuMeals);
        }

        // GET: api/Menu/5
        [HttpGet("{id}")]
        public ActionResult<Meal> GetMenuById(int id)
        {
            var menu = _menuService.GetMenuById(id);
            if (menu == null) return NotFound();

            List<MenuMeals> menuMeals = new List<MenuMeals>();
            var meals = _menuService.GetAllMenuMeals();

            foreach (var meal in meals)
            {
                if (meal.MenuId == menu.Id) menuMeals.Add(meal);
            }

            MenuViewModel model = new MenuViewModel()
            {
                Id = menu.Id,
                Week = menu.Week,
                Year = menu.Year
            };

            foreach (var item in menuMeals)
            {
                var meal = _service.GetMealById(item.MealId);
                if (meal != null) model.Meals.Add(meal);
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

        // PUT: api/Menu
        [HttpPut]
        public ActionResult UpdateMenu(Menu menu)
        {
            throw new NotImplementedException();
        }

        // PUT: api/Menu/5
        [HttpPut("{id}")]
        public IActionResult UpdateMenuById(int id, Meal meal)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/Menu
        [HttpDelete]
        public ActionResult CreateMenu(Menu menu)
        {
            throw new NotImplementedException();
        }

        // POST: api/Menu
        [HttpPost]
        public ActionResult DeleteMenu(Menu menu)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/Menu/5
        [HttpDelete("{id}")]
        public ActionResult DeleteMenuById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
