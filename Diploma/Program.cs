using Diploma.DbStuff;
using Diploma.DbStuff.Repositories;
using Diploma.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

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

var typeOfBaseServices = typeof(IService);
AppDomain.CurrentDomain
    .GetAssemblies()
    .SelectMany(s => s.GetTypes())
    .Where(p => typeOfBaseServices.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract)
    .ToList()
    .ForEach(serviceType => builder.Services.AddScoped(serviceType));

var typeOfBaseRepository = typeof(BaseRepository<>);
Assembly
    .GetAssembly(typeOfBaseRepository)
    .GetTypes()
    .Where(x => x.BaseType?.IsGenericType ?? false
        && x.BaseType.GetGenericTypeDefinition() == typeOfBaseRepository)
    .ToList()
    .ForEach(repositoryType => builder.Services.AddScoped(repositoryType));

var app = builder.Build();

app.UseCors();

SeedExtention.Seed(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=News}/{action=Index}/{id?}");

app.Run();
