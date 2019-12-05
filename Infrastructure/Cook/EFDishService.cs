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

        public async void CreateDish(Dish dish)
        {
            var entry = _context.Dish.FirstOrDefault(p => p.Name == dish.Name);
            if (entry != null)
            {
                entry.Name = dish.Name;
                entry.Image = dish.Image;
                entry.Description = dish.Description;
                entry.Price = dish.Price;
                entry.Restriction = dish.Restriction;
                entry.Size = dish.Size;
                entry.Type = dish.Type;
                entry.Cook = dish.Cook;
            }
            await _context.SaveChangesAsync();
        }

        public async void DeleteDish(int? id)
        {
            if (id == null) throw new KeyNotFoundException();

            var entry = await _context.Dish.FindAsync(id);
            _context.Dish.Remove(entry);
            await _context.SaveChangesAsync();
        }

        public Dish GetDishByName(string name)
        {
            if (name == null) throw new KeyNotFoundException();

            var dish = _context.Dish.SingleOrDefault(d => d.Name == name);
            return dish;
        }

        public List<Dish> GetDishes() => _context.Dish.ToList();

        public async void UpdateDish(Dish dish)
        {
           _context.Dish.Update(dish);
           await _context.SaveChangesAsync();
        }
    }
}
