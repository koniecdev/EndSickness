using EndSickness.Application.Common.Interfaces;
using EndSickness.Domain.Entities;

namespace EndSickness.Application.UnitTests.Common;

public class EndSicknessContextMockFactory : IDbContextMockFactory<EndSicknessContext>
{
    public Mock<EndSicknessContext> Create()
    {
        var options = new DbContextOptionsBuilder<EndSicknessContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        var currentUserMock = new Mock<ICurrentUserService>();
        currentUserMock.Setup(m => m.AppUserId).Returns("validUserId");
        currentUserMock.Setup(m => m.IsAuthorized).Returns(true);

        var mock = new Mock<EndSicknessContext>(options, currentUserMock.Object) { CallBase = true };

        var context = mock.Object;

        context.Database.EnsureCreated();

        //Remember - Seed of database are also applied - keep tracking of int IDs

        var medicine = new Medicine("Nurofen", TimeSpan.FromHours(4), 3, 7) { Id = 1, StatusId = 1, OwnerId = "validUserId" };
        context.Medicines.Add(medicine);

        var medicine1 = new Medicine("Voltaren", TimeSpan.FromHours(12), 2, 7) { Id = 2, StatusId = 1, OwnerId = "validUserId" };
        context.Medicines.Add(medicine1);

        var medicine2 = new Medicine("APAP", TimeSpan.FromHours(3), 2, 6) { Id = 3, StatusId = 0, OwnerId = "secondUserId" };
        context.Medicines.Add(medicine2);

        var log1 = new MedicineLog(medicine1.Id, new DateTime(2023, 3, 12, 13, 50, 0)) { Id = 1, StatusId = 1, OwnerId = "validUserId" };
        context.MedicineLogs.Add(log1);
        var log2 = new MedicineLog(medicine1.Id, new DateTime(2023, 3, 13, 10, 50, 0)) { Id = 2, StatusId = 1, OwnerId = "validUserId" };
        context.MedicineLogs.Add(log2);

        context.SaveChanges();

        return mock is null ? throw new Exception("Could not create db mock") : mock;
    }

    public void Destroy(EndSicknessContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}
