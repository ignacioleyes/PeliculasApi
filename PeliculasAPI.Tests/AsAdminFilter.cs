using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace PeliculasAPI.Tests
{
    public class AsAdminFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, "5673b8cf-12de-44f6-92ad-fae4a77932ad"),
                    new Claim(ClaimTypes.Role, "Admin")
                }, "admin"));

            await next();
        }
    }
}
