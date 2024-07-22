using Northwind.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Businnes.IModelServices
{
    public interface ILoginServices
    {
        public LoginResponseDto Confirm(int ID, string password);
    }
}
