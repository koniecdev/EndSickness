﻿using EndSickness.Application.Common.Interfaces;
using EndSickness.Domain.Common;
using EndSickness.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EndSickness.Persistance.EndSicknessContext;

public class EndSicknessContext : DbContext, IEndSicknessContext
{
    public EndSicknessContext()
    {    
    }
    public EndSicknessContext(DbContextOptions<EndSicknessContext> options) : base(options)
    {
    }
     
    public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
    public DbSet<Medicine> Medicines => Set<Medicine>();
    public DbSet<MedicineList> MedicineLists => Set<MedicineList>();
    public DbSet<MedicineLog> MedicineLogs => Set<MedicineLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach(var item in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (item.State)
            {
                case EntityState.Added:
                    item.Entity.StatusId = 1;
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