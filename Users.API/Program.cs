using Microsoft.AspNetCore.Identity;
using Serilog;
using Users.API.Extensions;
using Users.Core.Mappings;
using Users.Infrastructure;
using Users.Infrastructure.DataContext;
using Users.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
var services = builder.Services;

builder.RegisterServices();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(UserProfile));

//Registering Serilog as a log provider
var logger = new LoggerConfiguration()
                 .ReadFrom.Configuration(builder.Configuration)
                 .Enrich.FromLogContext()
                 .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var context = scope.ServiceProvider.GetRequiredService<UserDataContext>();
    await Preseeder.Seeder(context, roleManager, userManager);
}

app.MapControllers();

app.Run();
