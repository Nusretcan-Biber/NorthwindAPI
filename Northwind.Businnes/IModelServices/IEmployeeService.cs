using Northwind.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Businnes.IModelServices
{
    public interface IEmployeeService
    {
        public int CreateEmployee(Employee employeeModel);
        public int UpdateEmployee(Employee employeeModel);
        public int DeleteEmployee(short employeeId);
        public List<Employee> GetAllEmployee();
        public Employee GetEmployeeById(short employeeId);
    }
}
