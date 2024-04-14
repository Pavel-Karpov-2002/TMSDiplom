using PostMicroService.SignalRHubs;
using Microsoft.EntityFrameworkCore;
using PostMicroService.DbStuff;
using PostMicroService.DbStuff.Repositories;
using PostMicroService.Services;

var builder = WebApplication.CreateBuilder(args);


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
builder.Services.AddSignalR();


var connectionString = builder.Configuration.GetConnectionString("WebDb");
builder.Services.AddDbContext<PostNetworkWebDbContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddScoped<PostRepository>();
builder.Services.AddScoped<PostBuilder>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseCors();

app.MapHub<PostHub>("/posts");

app.MapGet("/", () => "Micro Sevice posts start!");

app.Run();
