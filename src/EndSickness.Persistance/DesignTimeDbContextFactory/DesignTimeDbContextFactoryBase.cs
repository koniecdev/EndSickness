//using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace EndSickness.Persistance.DesignTimeDbContextFactory;

public abstract class DesignTimeDbContextFactoryBase<TContext> :
        IDesignTimeDbContextFactory<TContext> where TContext : DbContext
{
    private readonly string _connectionStringName = "DefaultDatabase";

    public TContext CreateDbContext(string[] args)
    {
        var environmentName =
              Environment.GetEnvironmentVariable(
                  "ASPNETCORE_ENVIRONMENT");

        var dir = Directory.GetParent(AppContext.BaseDirectory);
        
        if (Environments.Development.Equals(environmentName,
            StringComparison.OrdinalIgnoreCase))
        {
            var depth = 0;
            do
            {
                dir = dir?.Parent;
            }
            while (++depth < 5 && dir?.Name != "bin");
            dir = dir?.Parent;
        }

        var basePath = dir?.FullName;

        ArgumentNullException.ThrowIfNull(basePath);
        ArgumentNullException.ThrowIfNull(environmentName);

        return Create(basePath, environmentName);
    }

    protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

    private TContext Create(string basePath, string environmentName)
    {

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.Local.json", optional: true)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString(_connectionStringName);

        return Create(connectionString);
    }

    private TContext Create(string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentException($"Connection string '{_connectionStringName}' is null or empty.", nameof(connectionString));
        }

        Console.WriteLine($"DesignTimeDbContextFactoryBase.Create(string): Connection string: '{connectionString}'.");

        var optionsBuilder = new DbContextOptionsBuilder<TContext>();

        optionsBuilder.UseSqlServer(connectionString);

        return CreateNewInstance(optionsBuilder.Options);
    }
}