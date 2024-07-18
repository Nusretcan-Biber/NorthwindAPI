using Northwind.Utils.UnitOfWorks;
using Northwind.Businnes.IModelServices;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwind.Data.Models;
using Microsoft.EntityFrameworkCore;
using Northwind.Utils.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Northwind.Businnes.ModelServices
{
    public class ShipperService : IShipperService
    {
        public int CreateShipper(Shipper shipperModel)
        {
            using (var uow = new UnitOfWork<PostgresContext>())
            {
                uow.GetRepository<Shipper>().Add(shipperModel);
                var result = uow.SaveChanges();
                return result;
            }
        }

        public int DeleteShipper(short shipperId)
        {
            using (var uow = new UnitOfWork<PostgresContext>())
            {
                var isExist = uow.GetRepository<Shipper>().Get(x => x.ShipperId.Equals(shipperId));
                if (isExist == null)
                    return 0;  // Silinmesi istenilen veri yoksa
                //ResponseModel eklendiğinde burada ResponseModel tipinde bir durum mesajı döndürülecek
                uow.GetRepository<Shipper>().Delete(isExist);
                if (uow.SaveChanges() < 0)
                    return -1; // Kullanıcı var ama silme işlemi gerçekleşmedi
                return 1; // Silme işlemi gerçekleşti


            }
        }

        public List<Shipper> GetAllShippers()
        {
            using (var uow = new UnitOfWork<PostgresContext>())
            {
                var shipperList = uow.GetRepository<Shipper>().GetAll().AsNoTracking().ToList();
                if (shipperList.Count < 0)
                    return null;
                return shipperList;

            }
        }

        public Shipper GetShipperById(short shipperId)
        {
            using (var uow = new UnitOfWork<PostgresContext>())
            {
                //var deneme = uow.GetRepository<Shipper>().GetByIdiki(shipperId);
                var isExist = uow.GetRepository<Shipper>().Get(x => x.ShipperId.Equals(shipperId) );
                
                if (isExist == null)
                    return isExist; //ResponseModel eklendiğinde burada ResponseModel tipinde bir durum mesajı döndürülecek
                return isExist;
            }
        }

        public int UpdateShipper(Shipper shipperModel)
        {
            using (var uow = new UnitOfWork<PostgresContext>())
            {
                var isExist = uow.GetRepository<Shipper>().Get(x => x.ShipperId.Equals(shipperModel.ShipperId));
                if (isExist == null)
                    return 0; // Kullanıcı bulunamadı
                //Burada Girilen Null değerleri Veritabanındaki veriler ile eşlenecek kod gelebilir
                uow.GetRepository<Shipper>().Update(shipperModel);
                if (uow.SaveChanges() < 0)
                    return -1; // Kullanıcı güncellenemedi
                return 1; // Güncelleme işlemi Başarılı

            }
        }
    }
}
