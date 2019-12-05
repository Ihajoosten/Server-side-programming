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

        // As a Cook I want to get a Menu by ID to see the details
        Menu GetMenuById(int? id);

        // As a Cook I want to see a list of Menus to select for my weekplan
        List<Menu> GetMenus();

        // As a Cook I want to create a new Menu
        void CreateMenu(Menu menu);

        // As a Cook I want to update an existing Menu
        void UpdateMenu(int? id);

        // As a Cook I want to delete incorrect Menus
        void DeleteMenu(int? id);
    }
}
