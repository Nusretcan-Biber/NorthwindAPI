using Microsoft.AspNetCore.Mvc;
using northwind.Authorization;
using northwind.Enums;
using Northwind.Businnes.IModelServices;
using Northwind.Businnes.ModelServices;
using Northwind.Data.DTOs;
using Northwind.Data.Models;

namespace northwind.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryService categoryService = new CategoryService();

        [HttpPost(nameof(CategoryInsert))]
        [Authorization(UserType = UserTypeEnum.OWNER, MethodStatusType= MethodStatusTypeEnum.EQUAL)]
        public IActionResult CategoryInsert(CategoryDto model)
        {
            var result = categoryService.CreateCategory(model);
            if (result <= 0)
            {
                return BadRequest();
            }
            return Ok(result);
        }


        [HttpDelete(nameof(CategoryDelete))]
        [Authorization(UserType = UserTypeEnum.OWNER, MethodStatusType = MethodStatusTypeEnum.EQUAL)]
        public IActionResult CategoryDelete(short categoryId)
        {
            var result = categoryService.DeleteCategory(categoryId);
            if (result <= 0)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet(nameof(GetCategoryByID))]
        [Authorization(UserType = UserTypeEnum.GUEST, MethodStatusType = MethodStatusTypeEnum.EQUAL)]
        public IActionResult GetCategoryByID(short ID)
        {
            var result = categoryService.GetCategoryById(ID);
            if (result != null)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet(nameof(GetAllCategory))]
        [Authorization(UserType = UserTypeEnum.ADMIN, MethodStatusType = MethodStatusTypeEnum.GREATER_EQUAL)]
        public IActionResult GetAllCategory()
        {
            var result = categoryService.GetAllCategory();
            if (result == null)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut(nameof(UpdateCategory))]
        [Authorization(UserType = UserTypeEnum.OWNER, MethodStatusType = MethodStatusTypeEnum.EQUAL)]
        public IActionResult UpdateCategory([FromBody] CategoryDto categoryModel)
        {
            var result = categoryService.UpdateCategory(categoryModel);
            if (result > 0)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
