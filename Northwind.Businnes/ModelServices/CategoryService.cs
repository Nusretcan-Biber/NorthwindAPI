using Microsoft.EntityFrameworkCore;
using Northwind.Businnes.IModelServices;
using Northwind.Data.Models;
using Northwind.Utils.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Businnes.ModelServices
{
    public class CategoryService : ICategoryService
    {
        public int CreateCategory(Category categoryModel)
        {
            using (var uow = new UnitOfWork<PostgresContext>())
            {
                uow.GetRepository<Category>().Add(categoryModel);
                return uow.SaveChanges();
            }
        }

        public int DeleteCategory(short categoryId)
        {
            using (var uow = new UnitOfWork<PostgresContext>())
            {
                var isExistItem = uow.GetRepository<Category>().Get(x => x.CategoryId.Equals(categoryId));
                if (isExistItem == null)
                    return 0;  // Silinmesi istenilen veri yoksa
                //ResponseModel eklendiğinde burada ResponseModel tipinde bir durum mesajı döndürülecek
                uow.GetRepository<Category>().Delete(isExistItem);
                if (uow.SaveChanges() < 0)
                    return -1; // Kullanıcı var ama silme işlemi gerçekleşmedi
                return 1; // Silme işlemi gerçekleşti
            }
        }

        public List<Category> GetAllCategory()
        {
            using (var uow = new UnitOfWork<PostgresContext>())
            {
                var CategoryList = uow.GetRepository<Category>().GetAll().AsNoTracking().ToList();
                if (CategoryList.Count < 0)
                    return null;
                return CategoryList;
            }
        }

        public Category GetCategoryById(short categoryId)
        {
            using (var uow = new UnitOfWork<PostgresContext>())
            {
                var isExistItem = uow.GetRepository<Category>().Get(x => x.CategoryId.Equals(categoryId));
                if (isExistItem == null)
                    return isExistItem; //ResponseModel eklendiğinde burada ResponseModel tipinde bir durum mesajı döndürülecek
                return isExistItem;
            }
        }

        public int UpdateCategory(Category categoryModel)
        {
            using (var uow = new UnitOfWork<PostgresContext>())
            {
                var isExistItem = uow.GetRepository<Category>().Get(x => x.CategoryId.Equals(categoryModel.CategoryId));
                if (isExistItem == null)
                    return 0; // Kullanıcı bulunamadı
                //Burada Girilen Null değerleri Veritabanındaki veriler ile eşlenecek kod gelebilir
                uow.GetRepository<Category>().Update(categoryModel);
                if (uow.SaveChanges() < 0)
                    return -1; // Kullanıcı güncellenemedi
                return 1; // Güncelleme işlemi Başarılı
            }
        }
    }
}
