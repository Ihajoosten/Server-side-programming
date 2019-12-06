using Domain;
using DomainServices;
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

        public async void CreateMenu(Menu menu, Meal[] meals)
        {
            if (menu == null) throw new OperationCanceledException();

            if (meals != null)
            {
                try
                {
                    foreach (Meal item in meals)
                    {
                        menu.Meals.Add(item);
                    }
                    await _context.AddAsync(menu);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }
        }

        public async void DeleteMenu(Menu menu)
        {
            if (menu == null) throw new NullReferenceException();
            var entry = _context.Menu.FirstOrDefault(m => m.Id == menu.Id);
            _context.Menu.Remove(entry);
            await _context.SaveChangesAsync();
        }

        public Menu GetMenuById(int? id)
        {
            if (id == null) throw new NullReferenceException();
            return _context.Menu.FirstOrDefault(m => m.Id == id);
        }

        public List<Menu> GetMenus() => _context.Menu.ToList();

        public async void UpdateMenu(Menu menu)
        {
            if (menu == null) throw new NullReferenceException();
            _context.Menu.Update(menu);
            await _context.SaveChangesAsync();
        }
    }
}
