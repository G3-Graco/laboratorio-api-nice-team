using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Core.Entidades;


namespace Web.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var ok = (bool?)context.HttpContext.Items["ok"];
            if (ok == null)
            {
                context.Result = new JsonResult(new { message = "Token inválido" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}