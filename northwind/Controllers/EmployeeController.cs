using Microsoft.AspNetCore.Mvc;
using Northwind.Businnes.IModelServices;
using Northwind.Businnes.ModelServices;
using Northwind.Data.DTOs;
using Northwind.Data.Models;

namespace northwind.Controllers
{
    public class EmployeeController : ControllerBase
    {
        IEmployeeService employeeService = new EmployeeService();

        [HttpPost(nameof(EmployeeInsert))]
        public IActionResult EmployeeInsert(EmployeeDto model)
        {
            var result = employeeService.CreateEmployee(model);
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpDelete(nameof(EmployeeDelete))]
        public IActionResult EmployeeDelete(short categoryId)
        {
            var result = employeeService.DeleteEmployee(categoryId);
            if (result <= 0)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet(nameof(GetEmployeeByID))]
        public IActionResult GetEmployeeByID(short ID)
        {
            var result = employeeService.GetEmployeeById(ID);
            if (result != null)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet(nameof(GetAllEmployee))]
        public IActionResult GetAllEmployee()
        {
            var result = employeeService.GetAllEmployee();
            if (result == null)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut(nameof(UpdateEmployee))]
        public IActionResult UpdateEmployee([FromBody] EmployeeDto employeeModel)
        {
            var result = employeeService.UpdateEmployee(employeeModel);
            if (result > 0)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
