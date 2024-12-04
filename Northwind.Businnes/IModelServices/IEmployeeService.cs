using Northwind.Data.DTOs;
using Northwind.Data.Models;

namespace Northwind.Businnes.IModelServices
{
    public interface IEmployeeService
    {
        public int CreateEmployee(EmployeeDto employeeModel);
        public int UpdateEmployee(EmployeeDto employeeModel);
        public int DeleteEmployee(short employeeId);
        public List<Employee> GetAllEmployee();
        public Employee GetEmployeeById(short employeeId);
    }
}
