using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Cook
{
    public class CookDbContext : DbContext
    {

        public CookDbContext(DbContextOptions<CookDbContext> options) : base(options){}

        public DbSet<Dish> Dish { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dish>();
        }
    }
}
