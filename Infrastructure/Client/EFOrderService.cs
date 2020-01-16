using Domain;
using DomainServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Client
{
    public class EFOrderService : IOrderService
    {
        private readonly ClientDbContext _context;

        public EFOrderService(ClientDbContext context) => _context = context;

        public IQueryable<Order> Order => _context.Order
            .Include(o => o.OrderMeals);

        public void CreateOrder(Order order)
        {
            if (order.Id == 0)
            {
                _context.Order.Add(order);
            }
            _context.SaveChanges();
        }

        public Order GetOrderById(int? id) => _context.Order.Single(o => o.Id == id);


        public List<Order> GetOrders() => _context.Order.ToList();
    }
}
