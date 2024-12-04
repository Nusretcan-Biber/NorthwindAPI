using Microsoft.EntityFrameworkCore;
using Northwind.Businnes.IModelServices;
using Northwind.Data.DTOs;
using Northwind.Data.Models;
using Northwind.Utils.AutoMapper;
using Northwind.Utils.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Businnes.ModelServices
{
    public class OrderService : IOrderService
    {

        public int CreateOrder(OrderDto orderModel)
        {
            using (var uow = new UnitOfWork<PostgresContext>())
            {
                var createOrder = MappingProfile<OrderDto, Order>.Instance.Mapper.Map<Order>(orderModel);
                uow.GetRepository<Order>().Add(createOrder);
                var result = uow.SaveChanges();
                return result;
            }
        }

        public int DeleteOrder(short orderID)
        {
            using (var uow = new UnitOfWork<PostgresContext>())
            {
                var isExistItem = uow.GetRepository<Order>().Get(x => x.OrderId.Equals(orderID));
                if (isExistItem == null)
                {
                    return 0; // Silinmesi istenilen veri yoksa
                }
                uow.GetRepository<Order>().Delete(isExistItem);
                if (uow.SaveChanges() < 0)
                    return -1; // Kullanıcı var ama silme işlemi gerçekleşmedi
                return 1; // Silme işlemi gerçekleşti
            }
        }

        public List<Order> GetAllOrder()
        {
            using (var uow = new UnitOfWork<PostgresContext>())
            {
                var orderList = uow.GetRepository<Order>().GetAll().AsNoTracking().ToList();
                if (orderList.Count < 0)
                    return null;
                return orderList;
            }
        }

        public Order GetOrderById(short orderID)
        {
            using (var uow = new UnitOfWork<PostgresContext>())
            {
                var isExistItem = uow.GetRepository<Order>().Get(x => x.OrderId.Equals(orderID));
                if (isExistItem == null)
                    return isExistItem; //ResponseModel eklendiğinde burada ResponseModel tipinde bir durum mesajı döndürülecek
                return isExistItem;
            }
        }

        public int UpdateOrder(OrderDto orderModel)
        {
            using (var uow = new UnitOfWork<PostgresContext>())
            {
                var isExistItem = uow.GetRepository<Order>().Get(x => x.OrderId.Equals(orderModel.OrderId));
                if (isExistItem == null)
                    return 0; // Kullanıcı bulunamadı
                //Burada Girilen Null değerleri Veritabanındaki veriler ile eşlenecek kod gelebilir
                var updateOrder = MappingProfile<OrderDto, Order>.Instance.Mapper.Map<Order>(orderModel);
                uow.GetRepository<Order>().Update(updateOrder);
                if (uow.SaveChanges() < 0)
                    return -1; // Kullanıcı güncellenemedi
                return 1; // Güncelleme işlemi Başarılı
            }
        }
    }
}
