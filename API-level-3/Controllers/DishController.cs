using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Domain;
using DomainServices;
using Halcyon.HAL;
using Halcyon.Web.HAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_level_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/hal+json")]
    public class DishController : ControllerBase
    {
        private readonly IDishService _service;

        public DishController(IDishService service) => _service = service;


        // GET: api/Dish
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult<IEnumerable<Dish>> GetDishes()
        {
            List<Dish> dishes = _service.GetDishes();

            var data = new
            {
                DisCount = dishes.Count
            };

            var response = new HALResponse(data)
                .AddLinks(new Link[] { new Link("self", "https://easy-meal-sswp-api.azurewebsites.net/api/Dish/api/Dish") })
                .AddEmbeddedCollection("dishes", dishes, new Link[]
                {
                    new Link("self", "https://easy-meal-sswp-api.azurewebsites.net/api/Dish/api/Dish/{Id}")
                });

            return Ok(response);
        }

        // GET: api/Dish/5
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetDishById(int id)
        {
            var dish = _service.GetDishById(id);
            if (dish != null)
            {
                return this.HAL(dish, new Link[] {
                    new Link("self", "https://easy-meal-sswp-api.azurewebsites.net/api/Dish/" + id, "Dish type: " + dish.Type ),
                    });
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/Dish
        [HttpPost]
        public IActionResult CreateDish(Dish dish)
        {
            throw new NotImplementedException();
        }

        // PUT api/Dish
        [HttpPut]
        public IActionResult UpdateDish(Dish dish)
        {
            throw new NotImplementedException();
        }

        // PUT api/Dish/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateDishById(int id, Dish dish)
        {
            throw new NotImplementedException();
        }

        // DELETE api/Dish
        [HttpDelete]
        public IActionResult DeleteDish(Dish dish)
        {
            throw new NotImplementedException();
        }

        // DELETE api/Dish/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
