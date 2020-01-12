using Domain;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Cook
{
    public class CookDbContext : DbContext
    {

        public CookDbContext(DbContextOptions<CookDbContext> options) : base(options) { }

        public CookDbContext() { }

        public DbSet<Dish> Dish { get; set; }
        public DbSet<Meal> Meal { get; set; }
        public DbSet<MealDishes> MealDish { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<MenuMeals> MenuMeal { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dish>();
            modelBuilder.Entity<Meal>();
            modelBuilder.Entity<MealDishes>().HasKey(md => new { md.DishId, md.MealId });
            modelBuilder.Entity<Menu>();
            modelBuilder.Entity<MenuMeals>().HasKey(mm => new { mm.MenuId, mm.MealId });

        }
    }
}
