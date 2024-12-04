using Microsoft.EntityFrameworkCore;
using Northwind.Businnes.IModelServices;
using Northwind.Data.DTOs;
using Northwind.Data.Models;
using Northwind.Utils.AutoMapper;
using Northwind.Utils.UnitOfWorks;

namespace Northwind.Businnes.ModelServices
{
    public class CategoryService : ICategoryService
    {
        public int CreateCategory(CategoryDto categoryModel)
        {
            using (var uow = new UnitOfWork<PostgresContext>())
            {
                var createCategory = MappingProfile<CategoryDto, Category>.Instance.Mapper.Map<Category>(categoryModel);
                uow.GetRepository<Category>().Add(createCategory);
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

        public int UpdateCategory(CategoryDto categoryModel)
        {
            using (var uow = new UnitOfWork<PostgresContext>())
            {
                var isExistItem = uow.GetRepository<Category>().Get(x => x.CategoryId.Equals(categoryModel.CategoryId));
                if (isExistItem == null)
                    return 0; // Kullanıcı bulunamadı
                //Burada Girilen Null değerleri Veritabanındaki veriler ile eşlenecek kod gelebilir
                var updateCartegory = MappingProfile<CategoryDto, Category>.Instance.Mapper.Map<Category>(categoryModel);
                uow.GetRepository<Category>().Update(updateCartegory);
                if (uow.SaveChanges() < 0)
                    return -1; // Kullanıcı güncellenemedi
                return 1; // Güncelleme işlemi Başarılı
            }
        }
    }
}
