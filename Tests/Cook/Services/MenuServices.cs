using Domain;
using Infrastructure.Cook;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Tests.Cook.Services
{
    public class MenuServices
    {

        public static Menu menu = new Menu()
        {
            Week = 12,
            Year = DateTime.Now.Year
        };

        public static Meal meal = new Meal()
        {
            DateValid = DateTime.Now.Date
        };

        public static Meal meal2 = new Meal()
        {
            DateValid = DateTime.Now.Date
        };
        public static Meal meal3 = new Meal()
        {
            DateValid = DateTime.Now.Date
        };


        [Fact]
        public void CreateMenu()
        {
            var options = new DbContextOptionsBuilder<CookDbContext>()
                .UseInMemoryDatabase(databaseName: "CookTest")
                .Options;

            // Run the test against one instance of the context
            using (var context = new CookDbContext(options))
            {

                var service = new EFMenuService(context);
                Meal[] meals = new Meal[] { meal, meal2, meal3 };
                service.CreateMenu(menu, meals);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new CookDbContext(options))
            {
                bool menuBool = context.Menu.FirstOrDefault(m => m.Week == menu.Week) != null ? true : false;
                Assert.True(menuBool);
                Assert.NotNull(context.Menu.FirstOrDefault());
            }
        }

        [Fact]
        public void GetAllMenus()
        {
            var options = new DbContextOptionsBuilder<CookDbContext>()
                .UseInMemoryDatabase(databaseName: "CookTest")
                .Options;


            // Use a separate instance of the context to verify correct data was saved to database
            var context = new CookDbContext(options);
            var service = new EFMenuService(context);
            var meals = service.GetMenus();

            Assert.Equal(meals.Count, context.Menu.Count());
        }

        [Fact]
        public void GetMenuById()
        {
            var options = new DbContextOptionsBuilder<CookDbContext>()
                .UseInMemoryDatabase(databaseName: "CookTest")
                .Options;

            // Use a separate instance of the context to verify correct data was saved to database
            var context = new CookDbContext(options);
            var service = new EFMenuService(context);

            Menu menu = new Menu();
            var menus = service.GetMenus();
            foreach (var item in menus)
            {
                if (item.Week == menu.Week) menu = item;
            }
            bool mealBool = service.Menu.Where(m => m.Week == 12) != null ? true : false;
            Assert.True(mealBool);
        }

        //[Fact]
        //public void UpdateMeal()
        //{
        //    var options = new DbContextOptionsBuilder<CookDbContext>()
        //        .UseInMemoryDatabase(databaseName: "CookTest")
        //        .Options;

        //    // Run the test against one instance of the context
        //    using (var context = new CookDbContext(options))
        //    {
        //        var service = new EFMenuService(context);

        //        menu.Meals.Add(new MenuMeals { Meal = meal, Menu = menu });
        //        menu.Meals.Add(new MenuMeals { Meal = meal2, Menu = menu });
        //        menu.Meals.Add(new MenuMeals { Meal = meal3, Menu = menu });

        //        service.UpdateMenu(menu);
        //    }

        //    // Use a separate instance of the context to verify correct data was saved to database
        //    using (var context = new CookDbContext(options))
        //    {
        //        var service = new EFMenuService(context);

        //        Menu menu = new Menu();
        //        var menus = service.GetMenus();
        //        foreach (var item in menus)
        //        {
        //            if (item.Week == 12) menu = item;
        //        }
        //        bool mealBool = service.Menu.Where(m => m.Week == 12) != null ? true : false;
        //        Assert.True(mealBool);

        //        var getMenu = service.GetMenuById(menu.Id);
        //        var menuMeals = service.GetAllMenuMeals().Count();
        //        Assert.NotNull(getMenu);
        //        Assert.Equal(3, menuMeals);
        //    }
        //}
    }
}
