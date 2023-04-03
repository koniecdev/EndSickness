using EndSickness.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EndSickness.Persistance.Configurations;

internal class MedicineLogConfiguration : IEntityTypeConfiguration<MedicineLog>
{
    public void Configure(EntityTypeBuilder<MedicineLog> builder)
    {
        builder.Property(m => m.LastlyTaken).IsRequired();
        builder.Property(m => m.MedicineId).IsRequired();
        builder.Property(m => m.AppUserId).IsRequired();
    }
}