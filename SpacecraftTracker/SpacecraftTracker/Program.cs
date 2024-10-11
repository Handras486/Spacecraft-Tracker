
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SpacecraftTracker.Persistence;
using SpacecraftTracker.Service;
using SpacecraftTracker.Service.Interfaces;
using SpacecraftTracker.WebAPI.Mappings;
using Microsoft.IdentityModel.Tokens;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

namespace SpacecraftTracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<SpacecraftTrackerDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SpacecraftTrackerConnectionString"),
                    b => b.MigrationsAssembly("SpacecraftTracker.Persistence"));
            });


            builder.Services.AddScoped<ISpacecraftService, SpacecraftService>();
            builder.Services.AddScoped<IFlightService, FlightService>();
            builder.Services.AddScoped<ITokenService, TokenService>();

            builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

            //AUTHENTICATION
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                });

            //AUTHORIZATION
            builder.Services.AddIdentityCore<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("SpacecraftTracker")
                .AddEntityFrameworkStores<SpacecraftTrackerDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredUniqueChars = 1;
                options.User.RequireUniqueEmail = true;
            });

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
            app.MapControllers();

            //Run Migrations on startup
            //var scope = app.Services.CreateScope();
            //var dbContext = scope.ServiceProvider.GetRequiredService<SpacecraftTrackerDbContext>();
            //dbContext.Database.MigrateAsync();

            app.Run();
        }
    }
}
