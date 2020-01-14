using Domain;
using DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Client
{
    public class EFOrderService : IOrderService
    {
        public IQueryable<Order> Order => throw new NotImplementedException();

        public void CreateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public Order GetOrderById(int? id)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetOrders()
        {
            throw new NotImplementedException();
        }
    }
}
