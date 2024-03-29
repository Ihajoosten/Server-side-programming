﻿// <auto-generated />
using System;
using Infrastructure.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ClientDbContext))]
    [Migration("20200119133110_addedPropToModel")]
    partial class addedPropToModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Addition");

                    b.Property<DateTime>("Birthday");

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<bool>("Diabetes");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<bool>("Gluten");

                    b.Property<int>("HouseNumber");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("PostalCode")
                        .IsRequired();

                    b.Property<bool>("Salt");

                    b.Property<string>("Street")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Client");
                });

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

                    b.ToTable("MealDishes");
                });

            modelBuilder.Entity("Domain.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ClientId");

                    b.Property<DateTime>("OrderDate");

                    b.Property<double>("TotalPrice");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Domain.OrderMeal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("MealDate");

                    b.Property<int>("MealId");

                    b.Property<int>("MealSize");

                    b.Property<int>("OrderId");

                    b.Property<bool>("birthdayMeal");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Ordermeals");
                });

            modelBuilder.Entity("Domain.OrderMealDish", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MealId");

                    b.Property<string>("Name");

                    b.Property<int>("OrderMealId");

                    b.Property<double>("Price");

                    b.HasKey("Id");

                    b.HasIndex("OrderMealId");

                    b.ToTable("OrderMealDishes");
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

            modelBuilder.Entity("Domain.Order", b =>
                {
                    b.HasOne("Domain.Client", "Client")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId");
                });

            modelBuilder.Entity("Domain.OrderMeal", b =>
                {
                    b.HasOne("Domain.Order")
                        .WithMany("Meals")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.OrderMealDish", b =>
                {
                    b.HasOne("Domain.OrderMeal")
                        .WithMany("Dishes")
                        .HasForeignKey("OrderMealId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
