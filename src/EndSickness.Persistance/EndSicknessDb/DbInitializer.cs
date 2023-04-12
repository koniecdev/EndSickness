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
    }
}