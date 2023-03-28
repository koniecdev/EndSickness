﻿using EndSickness.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EndSickness.Application.Common.Interfaces;

public interface IEndSicknessContext
{
    public DbSet<ApplicationUser> ApplicationUsers { get; }
    public DbSet<Medicine> Medicines { get; }
    public DbSet<MedicineList> MedicineLists { get; }
    public DbSet<MedicineLog> MedicineLogs { get; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
