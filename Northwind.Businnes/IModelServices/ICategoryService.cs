using Northwind.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Businnes.IModelServices
{
    public interface ICategoryService
    {
        public int CreateCategory(Category categoryModel);
        public int UpdateCategory(Category categoryModel);
        public int DeleteCategory(short categoryId);
        public List<Category> GetAllCategory();
        public Category GetCategoryById(short categoryId);
    }
}
