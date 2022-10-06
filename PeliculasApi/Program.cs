using AutoMapper;
using NetTopologySuite.Geometries;
using NetTopologySuite;
using PeliculasApi;
using PeliculasApi.Servicios;
using PeliculasApi.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PeliculasApi.Servicios.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IActoresServices, ActoresServices>();
builder.Services.AddScoped<ICuentasServices, CuentasServices>();
builder.Services.AddScoped<ICustomBaseServices, CustomBaseServices>();
builder.Services.AddScoped<IPeliculasServices, PeliculasServices>();
builder.Services.AddScoped<IReviewServices, ReviewServices>();
builder.Services.AddScoped<ISalasDeCineServices, SalasDeCineServices>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosAzure>();

builder.Services.AddSingleton<GeometryFactory>(NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326));

builder.Services.AddScoped<PeliculaExisteAttribute>();

builder.Services.AddSingleton(provider =>

new MapperConfiguration(config =>
{
    var geometryFactory = provider.GetRequiredService<GeometryFactory>();
    config.AddProfile(new AutoMapperProfiles(geometryFactory));
}).CreateMapper()
);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
          options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
          sqlServerOptions => sqlServerOptions.UseNetTopologySuite()
          ));

builder.Services.AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();


string jwtKey = Environment.GetEnvironmentVariable("PELICULAS_API_JWT_KEY");
if (jwtKey == null) throw new Exception("PELICULAS_API_JWT_KEY environment variable not set");
builder.Services.AddAuthentication()
    .AddJwtBearer(options =>
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                ClockSkew = TimeSpan.Zero
            });

builder.Services.AddCors(opciones =>
opciones.AddDefaultPolicy(builder =>
{
    builder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
}));


var app = builder.Build();


app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});



app.Run();