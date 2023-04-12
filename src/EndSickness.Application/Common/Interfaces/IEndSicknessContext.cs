using EndSickness.Domain.Entities;

namespace EndSickness.Application.Common.Interfaces;

public interface IEndSicknessContext
{
    public DbSet<Medicine> Medicines { get; }
    public DbSet<MedicineLog> MedicineLogs { get; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
