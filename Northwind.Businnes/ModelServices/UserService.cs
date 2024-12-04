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
    public class UserService : IUserSerrvice
    {
        public int AddUser(User addUser)
        {
            using (var uow = new UnitOfWork<PostgresContext>())
            {
                uow.GetRepository<User>().Add(addUser);
                var result = uow.SaveChanges();
                return result;
            }
        }

        public int DeleteUser(int userId)
        {
            using (var uow = new UnitOfWork<PostgresContext>())
            {
                var isExist = uow.GetRepository<User>().Get(x => x.UserID.Equals(userId));
                if (isExist == null)
                    return 0;  // Silinmesi istenilen veri yoksa
                //ResponseModel eklendiğinde burada ResponseModel tipinde bir durum mesajı döndürülecek
                uow.GetRepository<User>().Delete(isExist);
                if (uow.SaveChanges() < 0)
                    return -1; // Kullanıcı var ama silme işlemi gerçekleşmedi
                return 1; // Silme işlemi gerçekleşti


            }
        }

        public List<User> GetAllUser()
        {
            using (var uow = new UnitOfWork<PostgresContext>())
            {
                var shipperList = uow.GetRepository<User>().GetAll().AsNoTracking().ToList();
                if (shipperList.Count < 0)
                    return null;
                return shipperList;

            }
        }

        public User GetUser(int userId)
        {
            using (var uow = new UnitOfWork<PostgresContext>())
            {
                
                var isExist = uow.GetRepository<User>().Get(x => x.UserID.Equals(userId));

                if (isExist == null)
                    return isExist; //ResponseModel eklendiğinde burada ResponseModel tipinde bir durum mesajı döndürülecek
                return isExist;
            }
        }

        public int UpdateUser(User updateUser)
        {
            using (var uow = new UnitOfWork<PostgresContext>())
            {
                var isExist = uow.GetRepository<User>().Get(x => x.UserID.Equals(updateUser));
                if (isExist == null)
                    return 0; // Kullanıcı bulunamadı
                              //Burada Girilen Null değerleri Veritabanındaki veriler ile eşlenecek kod gelebilir

              

                uow.GetRepository<User>().Update(updateUser);
                if (uow.SaveChanges() < 0)
                    return -1; // Kullanıcı güncellenemedi
                return 1; // Güncelleme işlemi Başarılı

            }
        }
    }
}
