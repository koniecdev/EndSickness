using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EndSickness.Persistance.DesignTimeDbContextFactory;

internal abstract class DesignTimeDbContextFactoryBase<TContext> : IDesignTimeDbContextFactory<TContext>
    where TContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DesignTimeDbContextFactoryBase(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public TContext CreateDbContext(string[] args)
    {
        var connectionString = _configuration.GetConnectionString("DefaultDatabase");
        return Create(connectionString);
    }

    protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

    private TContext Create(string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new NullReferenceException("ConnectionString is null or empty!");
        }
        var dbOptionsBuilder = new DbContextOptionsBuilder<TContext>();
        dbOptionsBuilder.UseSqlServer(connectionString);
        return CreateNewInstance(dbOptionsBuilder.Options);
    }
}
