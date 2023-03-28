using EndSickness.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EndSickness.Persistance.Configurations;

internal class MedicineListConfiguration : IEntityTypeConfiguration<MedicineList>
{
    public void Configure(EntityTypeBuilder<MedicineList> builder)
    {
        builder.Property(m => m.Name).HasMaxLength(200).IsRequired();
        builder.Property(m => m.AppUserId).IsRequired();
    }
}