﻿// <auto-generated />
using System;
using Infrastructure.Cook;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations.CookDb
{
    [DbContext(typeof(CookDbContext))]
    [Migration("20200116194712_changedModels")]
    partial class changedModels
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Dish", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<byte[]>("Image");

                    b.Property<int?>("MealId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<double>("Price");

                    b.Property<int>("Restriction");

                    b.Property<int>("Size");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("MealId");

                    b.ToTable("Dish");
                });

            modelBuilder.Entity("Domain.Meal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateValid");

                    b.HasKey("Id");

                    b.ToTable("Meal");
                });

            modelBuilder.Entity("Domain.MealDishes", b =>
                {
                    b.Property<int>("DishId");

                    b.Property<int>("MealId");

                    b.HasKey("DishId", "MealId");

                    b.HasIndex("MealId");

                    b.ToTable("MealDish");
                });

            modelBuilder.Entity("Domain.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Week");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("Domain.MenuMeals", b =>
                {
                    b.Property<int>("MenuId");

                    b.Property<int>("MealId");

                    b.HasKey("MenuId", "MealId");

                    b.HasIndex("MealId");

                    b.ToTable("MenuMeal");
                });

            modelBuilder.Entity("Domain.Dish", b =>
                {
                    b.HasOne("Domain.Meal")
                        .WithMany("MealDishes")
                        .HasForeignKey("MealId");
                });

            modelBuilder.Entity("Domain.MealDishes", b =>
                {
                    b.HasOne("Domain.Dish", "Dish")
                        .WithMany()
                        .HasForeignKey("DishId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Meal", "Meal")
                        .WithMany("Dishes")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.MenuMeals", b =>
                {
                    b.HasOne("Domain.Meal", "Meal")
                        .WithMany()
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Menu", "Menu")
                        .WithMany("Meals")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
