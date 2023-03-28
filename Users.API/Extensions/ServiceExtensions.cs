using Serilog;
using Users.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Users.Models;
using Users.Core.Services;
using Users.Infrastructure.Repository.Interfaces;
using Users.Infrastructure.Repository;
using Users.Core;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Users.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            var configuration = builder.Configuration;
            var connectionString = configuration.GetConnectionString("ServerConnection");

            builder.Services.AddDbContext<UserDataContext>(opt => opt.UseSqlServer(connectionString));

            builder.Services.AddIdentity<User, IdentityRole>()
                            .AddEntityFrameworkStores<UserDataContext>()
                            .AddDefaultTokenProviders();

            builder.Host.UseSerilog((ctx, config) =>
            {
                ctx.HostingEnvironment.IsDevelopment(); //for development sake
                config.MinimumLevel.Debug();
                config.WriteTo.Console();
            });

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddTransient<ITokenFactory, TokenFactory>();

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true, //formerly false
                    ValidateAudience = true, //formerly false
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding
                                                            .UTF8
                                                            .GetBytes(configuration["JWT:Secret"]))
                };
            });
        }
    }
}
