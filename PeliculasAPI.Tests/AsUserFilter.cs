using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;


namespace PeliculasAPI.Tests
{
    public class AsUserFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, "d03cdd80-e8e9-4fc5-a2dc-176e99bb326a"),
                    new Claim(ClaimTypes.Role, "user")
                }, "user"));

            await next();
        }
    }
}
