using Diploma.DbStuff.Models;
using Diploma.DbStuff.Repositories;
using Diploma.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Diploma.Controllers.CustomAuthAttributes
{
    public class RoleAttribute : Attribute, IAuthorizationFilter
    {
        private Roles[] _roleNames;

        public RoleAttribute(params Roles[] roleNames)
        {
            _roleNames = roleNames;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authUserRole = context.HttpContext.RequestServices.GetService<RoleRepository>();
            var role = authUserRole!.GetCurrentUserRoles();
            var isValidRole = _roleNames.Any(roleName => role.Any(r => r.Name == roleName.ToString()));
            if (!isValidRole)
            {
                context.Result = new ForbidResult(AuthService.AUTH_KEY);
            }
        }
    }
}
