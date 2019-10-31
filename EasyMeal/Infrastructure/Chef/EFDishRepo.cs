using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Chef
{
    public class EFDishRepo : IDishRepo
    {
        protected readonly ChefDbContext _context;

        public EFDishRepo(ChefDbContext context)
        {
            _context = context;
        }

        public IQueryable<Dish> Dishes => _context.Dishes;

        public void CreateDish(Dish dish)
        {
            if (dish.Id == 0)
            {
                _context.Dishes.Add(dish);
            }
            else
            {
                Dish dbEntry = _context.Dishes
                    .FirstOrDefault(p => p.Id == dish.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = dish.Name;
                    dbEntry.Description = dish.Description;
                    dbEntry.Image = dish.Image;
                    dbEntry.Type = dish.Type;
                    dbEntry.Restriction = dish.Restriction;
                    dbEntry.Price = dish.Price;
                }
            }
            _context.SaveChanges();
        }

        public void DeleteDish(Dish dish)
        {
            Dish dbEntry = _context.Dishes.FirstOrDefault(d => d.Id == dish.Id);

            _context.Dishes.Remove(dbEntry);
            _context.SaveChanges();
        }

        public List<Dish> GetDishes() => _context.Dishes.ToList();

        public Dish GetDishById(int? id) => _context.Dishes.SingleOrDefault(d => d.Id == id);

        public void UpdateDish(Dish dish)
        {
            var x = _context.Dishes.FirstOrDefault(d => d.Id == dish.Id);
            _context.Dishes.Update(x);
            _context.SaveChanges();
        }
    }
}
