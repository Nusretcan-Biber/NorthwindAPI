using Northwind.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Utils.TokenFactory
{
    public interface IToken
    {
        public string CreateToken(User userModel);
        public bool IsTokenValid(string token);
    }
}
