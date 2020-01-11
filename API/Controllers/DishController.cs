    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using DomainServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_level_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _service;

        public DishController(IDishService service) => _service = service;


        // GET: api/Dish
        [HttpGet]
        public ActionResult<IEnumerable<Dish>> GetDishes()
        {
            return Ok(_service.GetDishes());
        }

        // GET: api/Dish/5
        [HttpGet("{id}")]
        public ActionResult GetDishById(int id)
        {
            var dish = _service.GetDishById(id);
            if (dish != null)
            {
                return Ok(dish);
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
