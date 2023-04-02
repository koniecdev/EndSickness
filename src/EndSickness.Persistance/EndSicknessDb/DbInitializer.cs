using EndSickness.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace EndSickness.Persistance.EndSicknessDb;

public class DbInitializer
{
    private readonly ModelBuilder modelBuilder;

    public DbInitializer(ModelBuilder modelBuilder)
    {
        this.modelBuilder = modelBuilder;
    }

    public void Seed()
    {
        modelBuilder.Entity<AppUser>().HasData(
               new AppUser(new Guid().ToString(), "defaultAdmin@koniec.dev", "DefaultAdmin") { Id = 1, StatusId = 1 }
        );
    }
}