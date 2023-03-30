using EndSickness.Persistance.DesignTimeDbContextFactory;
using Microsoft.EntityFrameworkCore;

namespace EndSickness.Persistance.EndSicknessDb;
public class EndSicknessContextFactory : DesignTimeDbContextFactoryBase<EndSicknessContext>
{
    protected override EndSicknessContext CreateNewInstance(DbContextOptions<EndSicknessContext> options)
    {
        var db = new EndSicknessContext(options);
        return db;
    }
}
