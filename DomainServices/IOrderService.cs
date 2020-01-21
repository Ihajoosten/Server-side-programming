using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainServices
{
    public interface IOrderService
    {

        IQueryable<Order> Order { get; }

        IQueryable<OrderMeal> OrderMeals { get; }

        IQueryable<OrderMealDish> OrderMealsDishes { get; }


        // As a customer I want to see the order details
        Order GetOrderById(int? id);

        // As a customer I want to see a list with orders
        List<Order> GetOrders();

        // As a customer I want to create a new order
        void CreateOrder(Order order);

        List<OrderMeal> GetOrderMeals();

        List<OrderMealDish> GetOrderMealDishes();

    }
}
