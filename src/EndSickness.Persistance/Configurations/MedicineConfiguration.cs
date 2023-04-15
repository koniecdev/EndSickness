using EndSickness.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EndSickness.Persistance.Configurations;

internal class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
{
    public void Configure(EntityTypeBuilder<Medicine> builder)
    {
        builder.Property(m => m.StatusId).HasDefaultValue(1);
        builder.Property(m => m.Name).HasMaxLength(200).HasDefaultValue(string.Empty).IsRequired();
        builder.Property(m => m.Cooldown).IsRequired();
        builder.Property(m => m.MaxDailyAmount).IsRequired();
        builder.Property(m => m.MaxDaysOfTreatment).IsRequired();
    }
}