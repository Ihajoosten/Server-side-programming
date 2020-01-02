using Domain;
using DomainServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Cook
{
    public class EFMenuService : IMenuService
    {
        private readonly CookDbContext _context;

        public EFMenuService(CookDbContext context) => _context = context;

        public IQueryable<Menu> Menu => _context.Menu;

        public IQueryable<MenuMeals> MenuMeal => _context.MenuMeal;

        public List<MenuMeals> GetAllMenuMeals() => _context.MenuMeal.ToList();

        public void CreateMenu(Menu menu, Meal[] meals)
        {
            if (menu == null) throw new OperationCanceledException();

            if (meals != null)
            {
                try
                {
                    _context.Add(menu);
                    foreach (Meal item in meals)
                    {
                        _context.Add(new MenuMeals { Menu = menu, Meal = item });
                    }
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }
        }

        public void DeleteMenu(Menu menu)
        {
            if (menu == null) throw new NullReferenceException();
            var entry = _context.Menu.FirstOrDefault(m => m.Id == menu.Id);
            _context.Menu.Remove(entry);
            _context.SaveChanges();
        }

        public Menu GetMenuById(int? id)
        {
            if (id == null) throw new NullReferenceException();
            return _context.Menu.FirstOrDefault(m => m.Id == id);
        }

        public List<Menu> GetMenus()
        {
            var menus = _context.Menu
               .Include(m => m.Meals)
               .ToList();
            menus.ForEach(m =>
            {
                var updatedMeals = new List<MenuMeals>();
                for (var i = 0; i < m.Meals.Count; i++)
                {
                    var localMeal = m.Meals.ToList()[i];
                    localMeal.Menu = null;
                    updatedMeals.Add(localMeal);
                }
                m.Meals = updatedMeals;
            });

            return menus;
        }

        public void UpdateMenu(Menu menu)
        {
            if (menu == null) throw new NullReferenceException();
            _context.Menu.Update(menu);
            _context.SaveChanges();
        }
    }
}
