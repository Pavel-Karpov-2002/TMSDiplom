using Diploma.CustomMiddlewares;
using Diploma.DbStuff;
using Diploma.DbStuff.Repositories;
using Diploma.Services;
using Diploma.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = AuthService.AUTH_KEY;
    })
    .AddCookie(AuthService.AUTH_KEY, option =>
    {
        option.AccessDeniedPath = "/auth/deny";
        option.LoginPath = "/Auth/Login";
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.SetIsOriginAllowed(url => true);
        policy.AllowCredentials();
    });
});

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("WebDb");

builder.Services.AddDbContext<SocialNetworkWebDbContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// DI repository
var typeOfBaseRepository = typeof(BaseRepository<>);
Assembly
    .GetAssembly(typeOfBaseRepository)
    .GetTypes()
    .Where(x => x.BaseType?.IsGenericType ?? false
        && x.BaseType.GetGenericTypeDefinition() == typeOfBaseRepository)
    .ToList()
    .ForEach(repositoryType => builder.Services.AddScoped(repositoryType));

// DI service
var typeOfBaseServices = typeof(IService);
AppDomain.CurrentDomain
    .GetAssemblies()
    .SelectMany(s => s.GetTypes())
    .Where(p => typeOfBaseServices.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract)
    .ToList()
    .ForEach(serviceType => builder.Services.AddScoped(serviceType));

var app = builder.Build();

app.UseCors();

// Create seeds
SeedExtention.Seed(app);

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<CustomLocalizationMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();
