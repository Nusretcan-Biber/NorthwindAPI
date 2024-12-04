using Microsoft.AspNetCore.Mvc;
using Northwind.Businnes.IModelServices;
using Northwind.Businnes.ModelServices;
using Northwind.Data.DTOs;

namespace northwind.Controllers
{
    public class LoginController : ControllerBase
    {
        //ILoginServices loginService= new LoginService();
        private readonly ILoginServices _loginServices;
        public LoginController(ILoginServices loginService)
        {
            this._loginServices = loginService;
        }

        [HttpPost("confirm")]

        public IActionResult Confirm([FromForm] int ID, [FromForm] string password)
        {
            var result = _loginServices.Confirm(ID, password);
            return Ok(result);
        }

    }
}
