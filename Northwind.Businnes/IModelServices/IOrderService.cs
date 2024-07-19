using Northwind.Data.DTOs;
using Northwind.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Businnes.IModelServices
{
    public interface IOrderService
    {
        public int CreateOrder(OrderDto orderModel);
        public int UpdateOrder(OrderDto orderModel);
        public int DeleteOrder(short orderID);
        public List<Order> GetAllOrder();
        public Order GetOrderById(short orderID);
    }
}
