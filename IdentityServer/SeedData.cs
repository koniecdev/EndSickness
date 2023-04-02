using IdentityServer.Data;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IdentityServer;

public class SeedData
{
    public static void EnsureSeedData(string connectionString)
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddDbContext<ApplicationDbContext>(options =>
           options.UseSqlServer(connectionString));

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        using var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
        context.Database.Migrate();

        var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var memberRole = roleMgr.FindByNameAsync("member").Result;
        if (memberRole == null)
        {
            memberRole = new IdentityRole("name");
            var result = roleMgr.CreateAsync(memberRole).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            Log.Debug("member role created");
        }
        else
        {
            Log.Debug("member role already exists");
        }

        var adminRole = roleMgr.FindByNameAsync("admin").Result;
        if (adminRole == null)
        {
            adminRole = new IdentityRole("admin");
            var result = roleMgr.CreateAsync(adminRole).Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            Log.Debug("admin role created");
        }
        else
        {
            Log.Debug("admin role already exists");
        }

        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var defaultAdmin = userMgr.FindByEmailAsync("defaultAdmin@koniec.dev").Result;
        if (defaultAdmin == null)
        {
            defaultAdmin = new ApplicationUser
            {
                Id = new Guid().ToString(), //zero only
                UserName = "DefaultAdmin",
                Email = "defaultAdmin@koniec.dev",
                EmailConfirmed = true,
            };
            var result = userMgr.CreateAsync(defaultAdmin, "Pass123$").Result;
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
            Log.Debug("DefaultAdmin");

            var roleResult = userMgr.AddToRoleAsync(defaultAdmin, adminRole.Name).Result;
            if (!roleResult.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
        }
        else
        {
            Log.Debug("DefaultAdmin already exists");
        }
    }
}
