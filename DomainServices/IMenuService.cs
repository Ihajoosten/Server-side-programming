using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainServices
{
    public interface IMenuService
    {

        IQueryable<Menu> Menu { get; }

        IQueryable<MenuMeals> MenuMeal { get; }

        // As a Cook I want to get a Menu by ID to see the details
        Menu GetMenuById(int? id);

        // As a Cook I want to see a list of Menus to select for my weekplan
        List<Menu> GetMenus();

        // As a Cook I want to create a new Menu
        void CreateMenu(Menu menu, Meal[] meals);

        // As a Cook I want to update an existing Menu
        void UpdateMenu(Menu menu);

        // As a Cook I want to delete incorrect Menus
        void DeleteMenu(Menu menu);

        // As a Cook I want to get all meals for a menu
        List<MenuMeals> GetAllMenuMeals();
    }
}
