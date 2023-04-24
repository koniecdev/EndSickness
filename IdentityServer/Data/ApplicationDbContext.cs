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
		//public DbSet<PersistedGrant> PersistedGrants { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
        }
	}
}
