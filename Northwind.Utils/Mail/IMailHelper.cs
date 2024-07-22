using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Utils.Mail
{
    public interface IMailHelper
    {
        public Task SendMailAsync(string subject, string body, string receptients);
    }
}
