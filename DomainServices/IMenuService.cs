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

        Menu GetMenuById(int? id);

        List<Menu> GetMenus();

        void CreateMenu(Menu menu);

        void UpdateMenu(int? id);

        void DeleteMenu(int? id);
    }
}
