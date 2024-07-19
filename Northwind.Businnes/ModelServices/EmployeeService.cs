using Microsoft.EntityFrameworkCore;
using Northwind.Businnes.IModelServices;
using Northwind.Data.DTOs;
using Northwind.Data.Models;
using Northwind.Utils.AutoMapper;
using Northwind.Utils.UnitOfWorks;

namespace Northwind.Businnes.ModelServices
{
    public class EmployeeService : IEmployeeService
    {
        public int CreateEmployee(EmployeeDto employeeModel)
        {
            using (var uow = new UnitOfWork<PostgresContext>())
            {
                var createEmployee = MappingProfile<EmployeeDto, Employee>.Instance.Mapper.Map<Employee>(employeeModel);
                uow.GetRepository<Employee>().Add(createEmployee);
                var result = uow.SaveChanges();
                return result;
            }
        }

        public int DeleteEmployee(short employeeId)
        {
            using (var uow = new UnitOfWork<PostgresContext>())
            {
                var isExistItem = uow.GetRepository<Employee>().Get(x => x.EmployeeId.Equals(employeeId));
                if (isExistItem == null)
                {
                    return 0; // Silinmesi istenilen veri yoksa
                }
                uow.GetRepository<Employee>().Delete(isExistItem);
                if (uow.SaveChanges() < 0)
                    return -1; // Kullanıcı var ama silme işlemi gerçekleşmedi
                return 1; // Silme işlemi gerçekleşti
            }
        }

        public List<Employee> GetAllEmployee()
        {
            using (var uow = new UnitOfWork<PostgresContext>())
            {
                var orderList = uow.GetRepository<Employee>().GetAll().AsNoTracking().ToList();
                if (orderList.Count < 0)
                    return null;
                return orderList;
            }
        }

        public Employee GetEmployeeById(short employeeId)
        {
            using (var uow = new UnitOfWork<PostgresContext>())
            {
                var isExistItem = uow.GetRepository<Employee>().Get(x => x.EmployeeId.Equals(employeeId));
                if (isExistItem == null)
                    return isExistItem; //ResponseModel eklendiğinde burada ResponseModel tipinde bir durum mesajı döndürülecek
                return isExistItem;
            }
        }

        public int UpdateEmployee(EmployeeDto employeeModel)
        {
            using (var uow = new UnitOfWork<PostgresContext>())
            {
                var isExistItem = uow.GetRepository<Employee>().Get(x => x.EmployeeId.Equals(employeeModel.EmployeeId));
                if (isExistItem == null)
                    return 0; // Kullanıcı bulunamadı
                //Burada Girilen Null değerleri Veritabanındaki veriler ile eşlenecek kod gelebilir
                var updateEmployee = MappingProfile<EmployeeDto, Employee>.Instance.Mapper.Map<Employee>(employeeModel);
                uow.GetRepository<Employee>().Update(updateEmployee);
                if (uow.SaveChanges() < 0)
                    return -1; // Kullanıcı güncellenemedi
                return 1; // Güncelleme işlemi Başarılı
            }
        }
    }
}
