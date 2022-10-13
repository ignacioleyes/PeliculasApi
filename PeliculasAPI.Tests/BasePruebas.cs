using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;

using PeliculasApi;


namespace PeliculasAPI.Tests
{
    public class BasePruebas<TStartup>: WebApplicationFactory<TStartup> where TStartup : class
    {
        protected WebApplicationFactory<Program> BuildWebApplicationFactory(string nameDB, bool ignoreSecurity = true, bool asAdmin = false)
        {
            var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var descriptorDBContext = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                    if (descriptorDBContext != null) services.Remove(descriptorDBContext);

                    services.AddDbContextPool<ApplicationDbContext>(options =>
                        options.UseInMemoryDatabase(nameDB));
                    var sp = services.BuildServiceProvider();

                    using (var scope = sp.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var db = scopedServices.GetRequiredService<ApplicationDbContext>();
                        db.Database.EnsureCreated();
                    }

                    if (ignoreSecurity)
                    {
                        services.AddScoped<IAuthorizationHandler, AllowAnonymusHandler>();
                        if (asAdmin)
                        {
                            services.AddControllers(options =>
                            {
                                options.Filters.Add(new AsAdminFilter());
                            });
                        }
                        else
                        {
                            services.AddControllers(options =>
                            {
                                options.Filters.Add(new AsUserFilter());
                            });
                        }
                    }
                });
            });

            return factory;
        }
    }
}
