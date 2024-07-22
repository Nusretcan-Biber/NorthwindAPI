using Northwind.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Businnes.IModelServices
{
    public interface IUserSerrvice
    {
        public int AddUser(User addUser);
        public int UpdateUser(User updateUser);
        public int DeleteUser(int userId);
        public User GetUser(int userId);
        public List<User> GetAllUser();
    }
}
