using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace PeliculasApi
{
    public static class ContextSeeding
    {
        public static async Task SeedTestData(this ApplicationDbContext context)
        {

            //var rolAdminId = "9aae0b6d-d50c-4d0a-9b90-2a6873e3845d";
            //var rolUserId = "3aecc270-8e17-4ab2-a8d0-5ea7e920d65a";
            //var usuarioAdminId = "5673b8cf-12de-44f6-92ad-fae4a77932ad";
            //var usuarioUserId = "8e19fb2b-c13a-4549-a4fa-9325bd387815";

            //var rolAdmin = new IdentityRole()
            //{
            //    Id = rolAdminId,
            //    Name = "Admin",
            //    NormalizedName = "Admin"
            //};  
            
            //var rolUser = new IdentityRole()
            //{
            //    Id = rolUserId,
            //    Name = "User",
            //    NormalizedName = "User"
            //};

            //var passwordHasher = new PasswordHasher<IdentityUser>();

            //var username = "nacho@gmail.com";
            //var usernameUser = "user@gmail.com";

            //var usuarioAdmin = new IdentityUser()
            //{
            //    Id = usuarioAdminId,
            //    UserName = username,
            //    NormalizedUserName = username,
            //    Email = username,
            //    NormalizedEmail = username,
            //    PasswordHash = passwordHasher.HashPassword(null, "Aa5075!")
            //};  
            
            //var usuarioUser = new IdentityUser()
            //{
            //    Id = usuarioUserId,
            //    UserName = usernameUser,
            //    NormalizedUserName = username,
            //    Email = username,
            //    NormalizedEmail = username,
            //    PasswordHash = passwordHasher.HashPassword(null, "Aa5075!")
            //};

            //context.Add(usuarioAdmin);
            //context.Add(usuarioUser);

            //context.Add(rolAdmin);
            //context.Add(rolUser);

            //context.Add(new IdentityUserClaim<string>()
            //    {
            //        Id = 1,
            //        ClaimType = ClaimTypes.Role,
            //        UserId = usuarioAdminId,
            //        ClaimValue = "Admin"
            //    });    
            //context.Add(new IdentityUserClaim<string>()
            //    {
            //        Id = 1,
            //        ClaimType = ClaimTypes.Role,
            //        UserId = usuarioUserId,
            //        ClaimValue = "User"
            //    });

            //await context.SaveChangesAsync();
        }
    }
}
