using Domain;
using DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Cook
{
    public class EFDishService : IDishService
    {
        protected readonly CookDbContext _context;

        public EFDishService(CookDbContext context)
        {
            _context = context;
        }

        public IQueryable<Dish> Dish => _context.Dish;

        public void CreateDish(Dish dish)
        {
            if (dish.Id == 0)
            {
                _context.Dish.Add(dish);
            }
            else
            {
                Dish entry = _context.Dish.FirstOrDefault(p => p.Id == dish.Id);
                if (entry != null)
                {
                    entry.Name = dish.Name;
                    entry.Image = dish.Image;
                    entry.Description = dish.Description;
                    entry.Price = dish.Price;
                    entry.Restriction = dish.Restriction;
                    entry.Size = dish.Size;
                    entry.Type = dish.Type;
                    //entry.Cook = dish.Cook;
                }
            }
            _context.SaveChanges();
        }

        public void DeleteDish(Dish dish)
        {
            if (dish == null) throw new NullReferenceException();
            var entry = _context.Dish.FirstOrDefault(d => d.Id == dish.Id);
            _context.Dish.Remove(entry);
            _context.SaveChanges();
        }

        public Dish GetDishById(int? id)
        {
            if (id == null) throw new KeyNotFoundException();
            var dish = _context.Dish.SingleOrDefault(d => d.Id == id);
            return dish;
        }

        public List<Dish> GetDishes() => _context.Dish.ToList();

        public void UpdateDish(Dish dish)
        {
            _context.Dish.Update(dish);
            _context.SaveChanges();
        }
    }
}
