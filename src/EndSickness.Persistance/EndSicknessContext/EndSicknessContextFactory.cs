using EndSickness.Application.Common.Interfaces;
using EndSickness.Persistance.DesignTimeDbContextFactory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EndSickness.Persistance.EndSicknessContext;
public class EndSicknessContextFactory : DesignTimeDbContextFactoryBase<EndSicknessContext>
{
    public EndSicknessContextFactory(IConfiguration configuration) : base(configuration)
    {
    }

    protected override EndSicknessContext CreateNewInstance(DbContextOptions<EndSicknessContext> options)
    {
        var db = new EndSicknessContext(options);
        return db;
    }
}
