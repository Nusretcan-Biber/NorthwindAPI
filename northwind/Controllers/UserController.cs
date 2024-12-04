using Microsoft.AspNetCore.Mvc;
using Northwind.Businnes.IModelServices;
using Northwind.Businnes.ModelServices;
using Northwind.Data.DTOs;
using Northwind.Data.Models;

namespace northwind.Controllers
{
    public class UserController : ControllerBase
    {
        IUserSerrvice userSerrvice = new UserService();

        [HttpPost(nameof(AddUser))]
        public IActionResult AddUser (User model)
        {
            var result = userSerrvice.AddUser(model);
            if (result <= 0)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpDelete(nameof(UserDelete))]
        public IActionResult UserDelete(short shipperid)
        {
            var result = userSerrvice.DeleteUser(shipperid);
            if (result <= 0)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet(nameof(GetUserByID))]
        public IActionResult GetUserByID(short ID)
        {
            var result = userSerrvice.GetUser(ID);
            if (result != null)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet(nameof(GetAllUser))]
        public IActionResult GetAllUser()
        {
            var result = userSerrvice.GetAllUser();
            if (result == null)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut(nameof(UpdateUser))]
        public IActionResult UpdateUser([FromBody] User userModel)
        {
            var result = userSerrvice.UpdateUser(userModel);
            if (result > 0)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
