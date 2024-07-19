using Northwind.Data.DTOs;
using Northwind.Data.Models;

namespace Northwind.Businnes.IModelServices
{
    public interface ICategoryService
    {
        public int CreateCategory(CategoryDto categoryModel);
        public int UpdateCategory(CategoryDto categoryModel);
        public int DeleteCategory(short categoryId);
        public List<Category> GetAllCategory();
        public Category GetCategoryById(short categoryId);
    }
}
