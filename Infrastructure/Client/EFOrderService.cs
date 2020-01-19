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

        public IQueryable<OrderMeal> orderMeals => _context.Ordermeals;

        public IQueryable<OrderMealDish> orderMealsDishes => _context.OrderMealDishes;

        public void CreateOrder(Order order)
        {
            if (order.Id == 0)
            {
                _context.Order.Add(order);

                foreach (var meal in order.Meals)
                {
                    _context.Ordermeals.Add(meal);
                }
                _context.SaveChanges();


                for (int i = 0; i < order.Meals.Count; i++)
                {
                    foreach (var item in order.Meals[i].Dishes.ToList())
                    {
                        if (item.OrderMealId != order.Meals[i].Id)
                        _context.OrderMealDishes.Add(new OrderMealDish { Name = item.Name, Price = item.Price, OrderMealId = order.Meals[i].Id });
                    }
                }
            }
        }

        public Order GetOrderById(int? id) => _context.Order.FirstOrDefault(o => o.Id == id);


        public List<Order> GetOrders() => _context.Order.ToList();

        public List<OrderMeal> GetOrderMeals() => _context.Ordermeals.ToList();


        public List<OrderMealDish> GetOrderMealDishes() => _context.OrderMealDishes.ToList();
    }
}
