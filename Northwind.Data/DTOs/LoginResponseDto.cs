using Northwind.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Data.DTOs
{
    public class LoginResponseDto
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public UserTypeEnum UserTypeEnum { get; set; }
        public string AuthToken { get; set; }
        public string? AppRegisterToken { get; set; }
    }
}
