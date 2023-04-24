using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace IdentityServer.Data;
public class DbInitializer
{
    private readonly ModelBuilder _modelBuilder;
    private readonly UserManager<IdentityUser> _userManager;

    public DbInitializer(ModelBuilder modelBuilder, UserManager<IdentityUser> userManager)
    {
        _modelBuilder = modelBuilder;
        _userManager = userManager;
    }

    public async Task Seed()
    {
        _modelBuilder.Entity<IdentityRole>().HasData(
               new IdentityRole("member") { Id = Guid.NewGuid().ToString() },
               new IdentityRole("admin") { Id = Guid.NewGuid().ToString() }
        );
        var defaultUser = new IdentityUser() { Id = Guid.NewGuid().ToString(), Email = "defaultAdmin@koniec.dev", UserName = "DefaultAdmin" };
        await _userManager.CreateAsync(defaultUser);
        await _userManager.AddPasswordAsync(defaultUser, "DefaultPassword123#");
        await _userManager.AddToRoleAsync(defaultUser, "admin");
    }
}
