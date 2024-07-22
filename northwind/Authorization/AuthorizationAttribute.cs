using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Northwind.Data.Models;
using northwind.Enums;
using Northwind.Utils.TokenFactory;

namespace northwind.Authorization
{
    public class AuthorizationAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        public UserTypeEnum UserType;

        public MethodStatusTypeEnum MethodStatusType { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authToken = context.HttpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authToken))
            {
                context.HttpContext.Response.Headers["WWW-Authenticate"] = $"Auth Error";
                context.Result = new UnauthorizedResult();
                return;
            }

            var token = authToken.Split();
            if (token.Length == 1)
            {
                var resultValidate = JWTTOkenFactory.Instance.IsTokenValid(token[0]);
                if (resultValidate == null)
                {
                    context.HttpContext.Response.Headers["WWW-Authenticate"] = $"Auth Error";
                    context.Result = new UnauthorizedResult();
                    return;
                }


                switch (MethodStatusType)
                {
                    case MethodStatusTypeEnum.GREATER_THAN:
                    case MethodStatusTypeEnum.GREATER_EQUAL:
                        if (resultValidate < UserType)
                        {
                            context.HttpContext.Response.Headers["WWW-Authenticate"] = $"Auth Error";
                            context.Result = new ForbidResult();
                            return;
                        }
                        break;
                    case MethodStatusTypeEnum.LESS_THAN:
                    case MethodStatusTypeEnum.LESS_EQUAL:
                        if (resultValidate > UserType)
                        {
                            context.HttpContext.Response.Headers["WWW-Authenticate"] = $"Auth Error";
                            context.Result = new ForbidResult();
                            return;
                        }
                        break;
                    case MethodStatusTypeEnum.EQUAL:
                        if (UserType != resultValidate)
                        {
                            context.HttpContext.Response.Headers["WWW-Authenticate"] = $"Auth Error";
                            context.Result = new ForbidResult();
                            return;
                        }
                        break;
                }
            }
            else
            {
                context.HttpContext.Response.Headers["WWW-Authenticate"] = $"Auth Error";
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}