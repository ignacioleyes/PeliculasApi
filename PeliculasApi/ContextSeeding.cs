using Microsoft.AspNetCore.Identity;
using PeliculasApi.Entidades;
using System.Security.Claims;

namespace PeliculasApi
{
    public static class ContextSeeding
    {
        public static async Task SeedTestData(this ApplicationDbContext context)
        {

            var usuarioAdminId = "5673b8cf-12de-44f6-92ad-fae4a77932ad";
            var usuarioUserId = "8e19fb2b-c13a-4549-a4fa-9325bd387815";

            var passwordHasher = new PasswordHasher<IdentityUser>();

            var username = "nacho@gmail.com";
            var usernameUser = "user@gmail.com";

            var usuarioAdmin = new IdentityUser()
            {
                Id = usuarioAdminId,
                UserName = username,
                NormalizedUserName = username,
                Email = username,
                NormalizedEmail = username,
                PasswordHash = passwordHasher.HashPassword(null, "Aa5075!")
            };  
            
            var usuarioUser = new IdentityUser()
            {
                Id = usuarioUserId,
                UserName = usernameUser,
                NormalizedUserName = username,
                Email = username,
                NormalizedEmail = username,
                PasswordHash = passwordHasher.HashPassword(null, "Aa5075!")
            };

            var adminClaim = new IdentityUserClaim<string>()
            {
                Id = new Random().Next(),
                ClaimType = ClaimTypes.Role,
                UserId = usuarioAdminId,
                ClaimValue = "Admin"
            };
            var userClaim = new IdentityUserClaim<string>()
            {
                Id = new Random().Next(),
                ClaimType = ClaimTypes.Role,
                UserId = usuarioUserId,
                ClaimValue = "User"
            };

            var actor = new Actor()
            {
                Id = 1,
                Nombre = "Tom Holland",
                FechaNacimiento = DateTime.ParseExact("01/06/2006 00:00:00", "dd/MM/yyyy HH:mm:ss", null),
            };

            await context.UserClaims.AddRangeAsync(new IdentityUserClaim<string>[] { userClaim, adminClaim });
            await context.Users.AddRangeAsync(new IdentityUser[] { usuarioAdmin, usuarioUser });

            await context.Actores.AddAsync(actor);

            await context.SaveChangesAsync();
        }
    }
}
