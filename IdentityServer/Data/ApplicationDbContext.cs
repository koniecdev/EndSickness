using IdentityServer.Models;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		public DbSet<PersistedGrant> PersistedGrants { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder builder)
		{
            builder.Entity<PersistedGrant>(entity =>
            {
                entity.HasKey(x => x.Key);
                entity.Property(x => x.Key).HasMaxLength(200);
                entity.Property(x => x.Type).HasMaxLength(50);
                entity.Property(x => x.SubjectId).HasMaxLength(200);
                entity.Property(x => x.ClientId).HasMaxLength(200).IsRequired();
                entity.Property(x => x.CreationTime).IsRequired();
                entity.Property(x => x.Expiration).IsRequired();
                entity.Property(x => x.Data).HasMaxLength(50000).IsRequired();
            });
            base.OnModelCreating(builder);
        }
	}
}
