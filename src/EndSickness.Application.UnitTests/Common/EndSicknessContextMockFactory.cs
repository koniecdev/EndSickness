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

        var medicine = new Medicine("Nurofen", 4, 3, 7) { Id = 1, StatusId = 1, OwnerId = "validUserId" };
        context.Medicines.Add(medicine);

        var medicine1 = new Medicine("Voltaren", 12, 2, 7) { Id = 2, StatusId = 1, OwnerId = "validUserId" };
        context.Medicines.Add(medicine1);

        var medicine2 = new Medicine("APAP", 3, 2, 6) { Id = 3, StatusId = 0, OwnerId = "secondUserId" };
        context.Medicines.Add(medicine2);

        var medicine3 = new Medicine("Nospa", 3, 2, 6) { Id = 4, StatusId = 1, OwnerId = "secondUserId" };
        context.Medicines.Add(medicine3);

        var log1 = new MedicineLog(medicine1.Id, new DateTime(2023, 3, 12, 13, 50, 0)) { Id = 4, StatusId = 1, OwnerId = "validUserId" };
        context.MedicineLogs.Add(log1);
        var log2 = new MedicineLog(medicine1.Id, new DateTime(2023, 3, 13, 10, 50, 0)) { Id = 5, StatusId = 1, OwnerId = "validUserId" };
        context.MedicineLogs.Add(log2);
        var log3 = new MedicineLog(medicine1.Id, new DateTime(2023, 3, 13, 10, 50, 0)) { Id = 6, StatusId = 0, OwnerId = "validUserId" };
        context.MedicineLogs.Add(log3);
        var log4 = new MedicineLog(medicine1.Id, new DateTime(2023, 3, 13, 10, 50, 0)) { Id = 7, StatusId = 1, OwnerId = "secondUserId" };
        context.MedicineLogs.Add(log4);

        context.SaveChanges();

        return mock is null ? throw new Exception("Could not create db mock") : mock;
    }

    public void Destroy(EndSicknessContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}
