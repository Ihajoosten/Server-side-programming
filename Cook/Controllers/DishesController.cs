using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using Infrastructure.Cook;
using DomainServices;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Cook.Controllers
{
    public class DishesController : Controller
    {
        private readonly IDishService _service;

        public DishesController(IDishService service)
        {
            _service = service;
        }

        // GET: Dishes
        public ViewResult Index() => View(_service.Dish.ToList());
       

        // GET: Dishes/Details/5
        public ViewResult Details(int? id)
        {
            var types = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Select Type",
                    Value = ""
                }
            };
            foreach (DishType type in Enum.GetValues(typeof(DishType)))
            {
                types.Add(new SelectListItem { Text = Enum.GetName(typeof(DishType), type), Value = type.ToString() });
            }
            ViewBag.Types = types;

            var restrictions = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Select Restrictions",
                    Value = ""
                }
            };
            foreach (DietRestriction restriction in Enum.GetValues(typeof(DietRestriction)))
            {
                restrictions.Add(new SelectListItem { Text = Enum.GetName(typeof(DietRestriction), restriction), Value = restriction.ToString() });
            }
            ViewBag.Restrictions = restrictions;

            var sizes = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Select Size",
                    Value = ""
                }
            };
            foreach (DishSize size in Enum.GetValues(typeof(DishSize)))
            {
                sizes.Add(new SelectListItem { Text = Enum.GetName(typeof(DishSize), size), Value = size.ToString() });
            }
            ViewBag.Sizes = sizes;

            if (id == null)
            {
                throw new KeyNotFoundException();
            }

            var dish = _service.Dish
           .FirstOrDefault(m => m.Id == id);
            if (dish == null)
            {
                throw new NullReferenceException();
            }
            return View(dish);
        }

        // GET: Dishes/Create
        public ViewResult Create()
        {
            var types = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Select Type",
                    Value = ""
                }
            };
            foreach (DishType type in Enum.GetValues(typeof(DishType)))
            {
                types.Add(new SelectListItem { Text = Enum.GetName(typeof(DishType), type), Value = type.ToString() });
            }
            ViewBag.Types = types;

            var restrictions = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Select Restriction",
                    Value = ""
                }
            };
            foreach (DietRestriction restriction in Enum.GetValues(typeof(DietRestriction)))
            {
                restrictions.Add(new SelectListItem { Text = Enum.GetName(typeof(DietRestriction), restriction), Value = restriction.ToString() });
            }
            ViewBag.Restrictions = restrictions;

            var sizes = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Select Size",
                    Value = ""
                }
            };
            foreach (DishSize size in Enum.GetValues(typeof(DishSize)))
            {
                sizes.Add(new SelectListItem { Text = Enum.GetName(typeof(DishSize), size), Value = size.ToString() });
            }
            ViewBag.Sizes = sizes;
            return View(new Dish());
        }

        // POST: Dishes/Create
        [HttpPost]
        public IActionResult Create([Bind("Id,Name,Description,Image,Type,Restriction,Size,Price")] Dish dish, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    if (image.Length > 0)
                    //Convert Image to byte and save to database
                    {
                        using (var stream = new MemoryStream())
                        {
                            image.CopyTo(stream);
                            dish.Image = stream.ToArray();
                        }
                    }
                }
                _service.CreateDish(dish);
                return RedirectToAction(nameof(Index));
            }
            return View(dish);
        }

        // GET: Dishes/Edit/5
        public ViewResult Edit(int? id)
        {
            var types = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Select Type",
                    Value = ""
                }
            };
            foreach (DishType type in Enum.GetValues(typeof(DishType)))
            {
                types.Add(new SelectListItem { Text = Enum.GetName(typeof(DishType), type), Value = type.ToString() });
            }
            ViewBag.Types = types;

            var restrictions = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Select Restrictions",
                    Value = ""
                }
            };
            foreach (DietRestriction restriction in Enum.GetValues(typeof(DietRestriction)))
            {
                restrictions.Add(new SelectListItem { Text = Enum.GetName(typeof(DietRestriction), restriction), Value = restriction.ToString() });
            }
            ViewBag.Restrictions = restrictions;

            var sizes = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Select Size",
                    Value = ""
                }
            };
            foreach (DishSize size in Enum.GetValues(typeof(DishSize)))
            {
                sizes.Add(new SelectListItem { Text = Enum.GetName(typeof(DishSize), size), Value = size.ToString() });
            }
            ViewBag.Sizes = sizes;

            if (id == null)
            {
                throw new KeyNotFoundException();
            }

            var dish = _service.Dish
                .FirstOrDefault(m => m.Id == id);

            if (dish == null)
            {
                throw new NullReferenceException();
            }

            return View(dish);
        }

        // POST: Dishes/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id,Name,Description,Image,Type,Restriction,Size,Price")] Dish dish, IFormFile image)
        {
            if (id != dish.Id)
            {
                throw new KeyNotFoundException();
            }
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    if (image.Length > 0)
                    //Convert Image to byte and save to database
                    {
                        using (var stream = new MemoryStream())
                        {
                            image.CopyTo(stream);
                            dish.Image = stream.ToArray();
                        }
                    }
                }
                _service.UpdateDish(dish);

                return RedirectToAction(nameof(Index));
            }
            return View(dish);
        }

        // GET: Dishes/Delete/5
        public ViewResult Delete(int? id)
        {
            Dish dish = _service.Dish
                .FirstOrDefault(m => m.Id == id);


            return View(dish);
        }

        // POST: Dishes/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var x = _service.GetDishById(id);
            _service.DeleteDish(x);
            return RedirectToAction("Index");
        }


        private bool DishExists(int id)
        {
            return _service.Dish.Any(e => e.Id == id);
        }
    }
}
