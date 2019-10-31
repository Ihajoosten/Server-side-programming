using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Chef
{
    public class ChefDbContext : DbContext
    {
        public ChefDbContext(DbContextOptions<ChefDbContext> options)
            : base(options)
        {
        }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<MealDishes> MealDishes { get; set; }
        public DbSet<Meal> Meals { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dish>();
            modelBuilder.Entity<MealDishes>().HasKey(md => new { md.MealId, md.DishId });
            modelBuilder.Entity<Meal>();

        }
    }
}
