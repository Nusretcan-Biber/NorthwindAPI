using Northwind.Businnes.IModelServices;
using Northwind.Data.DTOs;
using Northwind.Data.Models;
using Northwind.Utils.TokenFactory;
using Northwind.Utils.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Businnes.ModelServices
{
    public class LoginService : ILoginServices
    {
        public LoginResponseDto Confirm(int ID, string password)
        {
            using (UnitOfWork<PostgresContext> uow = new())
            {
                var user = uow.GetRepository<User>().Get(x => x.UserID.Equals(ID) && x.Password.Equals(password));
                if (user != null)
                {
                    return new LoginResponseDto
                    {
                        UserID = user.UserID,
                        Name = user.Name,
                        Password = user.Password,
                        UserTypeEnum = user.UserTypeEnum,
                        AppRegisterToken = JWTTOkenFactory.Instance.CreateToken(user),
                        AuthToken = JWTTOkenFactory.Instance.CreateToken(user),
                    };
                }
                return null;
            }
        }
    }
}
