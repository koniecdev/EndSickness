using Microsoft.EntityFrameworkCore;

namespace EndSickness.Application.UnitTests.Common;

public interface IDbContextMockFactory<TDbContext>
    where TDbContext : DbContext
{
    public Mock<TDbContext> Create();
    public void Destroy(TDbContext context);
}
