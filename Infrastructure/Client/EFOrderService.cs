using Domain;
using DomainServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Client
{
    public class EFOrderService : IOrderService
    {
        private readonly ClientDbContext _context;

        public EFOrderService(ClientDbContext context) => _context = context;

        public IQueryable<Order> Order => _context.Order.Include(o => o.Meals);

        public IQueryable<OrderMeal> OrderMeals => _context.Ordermeals;

        public IQueryable<OrderMealDish> OrderMealsDishes => _context.OrderMealDishes;

        public void CreateOrder(Order order)
        {
            if (order.Id == 0)
            {
                _context.Order.Add(order);

                foreach (var meal in order.Meals)
                {
                    _context.Ordermeals.Add(meal);

                    foreach (var dish in meal.Dishes)
                    {
                        _context.Add(dish);
                    }
                }
                _context.SaveChanges();
            }
        }

        public Order GetOrderById(int? id) => _context.Order.FirstOrDefault(o => o.Id == id);


        public List<Order> GetOrders() => _context.Order.ToList();

        public List<OrderMeal> GetOrderMeals() => _context.Ordermeals.ToList();


        public List<OrderMealDish> GetOrderMealDishes() => _context.OrderMealDishes.ToList();
    }
}
