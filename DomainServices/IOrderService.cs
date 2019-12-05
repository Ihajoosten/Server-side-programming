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

        Order GetOrderById(int? id);

        List<Order> GetOrders();

        void CreateOrder(Order order);

        void UpdateOrder(int? id);

        void DeleteOrder(int? id);
    }
}
