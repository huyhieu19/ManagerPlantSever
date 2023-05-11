﻿using ManagerServer.Database.Entity;
using ManagerServer.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ManagerServer.Service.VisitorServices;
using ManagerServer.Service.SlaveServices;
using ManagerServer.Service.FarmerService;
using ManagerServer.FarmerService;
using ManagerServer.Service.SmallHoldingServices;
using ManagerServer.Service;
using MyProject.Services;
using ManagerServer.Service.ProcessDataService;

namespace ManagerServer.StartUp
{
    public static class StartUp
    {
        public static WebApplicationBuilder AddServicesBase(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IVisitorService, VisitorService>();
            builder.Services.AddScoped<UserManager<AppUser>>();
            builder.Services.AddScoped<SignInManager<AppUser>>();
            builder.Services.AddScoped<ISlaveService, SlaveService>();
            builder.Services.AddScoped<RoleManager<IdentityRole>>();
            builder.Services.AddScoped<IFarmerService, FarmarService>();
            builder.Services.AddScoped<ISmallHoldingService, SmallHoldingService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });
           
            return builder;
        }
        public static WebApplicationBuilder AddBackgroundServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddHostedService<ListeningService>();
            builder.Services.AddHostedService<ProcessDataService>();
            return builder;
        }
        public static WebApplicationBuilder AddServicesContext(this WebApplicationBuilder builder) {
            builder.Services.AddDbContext<ManagerDbContext>(options =>
                  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            
            return builder;
        }
        public static WebApplicationBuilder AddServicesIdentity(this WebApplicationBuilder builder) {
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<ManagerDbContext>().AddDefaultTokenProviders();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        builder.Configuration["JWT:Secret"]))
                };
            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("User", policy => policy.RequireRole("User"));
            });
            return builder;
        }
        public static WebApplication UsesService(this WebApplication app)
        {

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors("AllowAllHeaders");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            return app;
        }
    }
}