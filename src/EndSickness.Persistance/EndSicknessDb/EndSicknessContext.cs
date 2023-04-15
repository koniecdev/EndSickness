using EndSickness.Application.Common.Exceptions;
using EndSickness.Application.Common.Interfaces;
using EndSickness.Domain.Common;
using EndSickness.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EndSickness.Persistance.EndSicknessDb;

public class EndSicknessContext : DbContext, IEndSicknessContext
{
    private readonly ICurrentUserService? _currentUserService;

    public EndSicknessContext()
    {    
    }
    public EndSicknessContext(DbContextOptions<EndSicknessContext> options) : base(options)
    {
    }
    public EndSicknessContext(DbContextOptions<EndSicknessContext> options, ICurrentUserService currentUserService) : base(options)
    {
        _currentUserService = currentUserService;
    }

    public DbSet<Medicine> Medicines => Set<Medicine>();
    public DbSet<MedicineLog> MedicineLogs => Set<MedicineLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        new DbInitializer(modelBuilder).Seed();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach(var item in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (item.State)
            {
                case EntityState.Added:
                    item.Entity.StatusId = 1;
                    item.Entity.OwnerId = _currentUserService?.AppUserId ?? throw new UserNotResolvedException();
                    break;
                case EntityState.Deleted:
                    item.Entity.StatusId = 0;
                    item.State = EntityState.Modified;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}