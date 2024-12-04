using Microsoft.Extensions.DependencyInjection;
using Northwind.Businnes.IModelServices;
using Northwind.Businnes.ModelServices;
using Northwind.Utils.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Businnes.Helpers
{
    public static class ServiceCollectors
    {
        public static void AddMyServices(this IServiceCollection services)
        {
            services.AddScoped<ILoginServices, LoginService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IShipperService, ShipperService>();
            services.AddScoped<IUserSerrvice, UserService>();
            services.AddScoped<IMailHelper, MailHelper>();

        }
    }
}
