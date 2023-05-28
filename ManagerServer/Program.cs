using ManagerServer.Database;
using ManagerServer.Database.Entity;
using ManagerServer.Service;
using ManagerServer.Service.VisitorServices;
using ManagerServer.StartUp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args)
            .AddServicesContext()
            .AddServicesBase()
            .AddServicesIdentity()
            .AddBackgroundServices()
            ;
        var app = builder.Build();
        app.UsesService();
        
        app.Run();

        
    }
}