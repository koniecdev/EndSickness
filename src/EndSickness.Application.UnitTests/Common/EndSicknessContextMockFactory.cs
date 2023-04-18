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

        var nurofen = new Medicine("Nurofen", 4, 3, 7) { Id = 1, StatusId = 1, OwnerId = "validUserId" };
        context.Medicines.Add(nurofen);

        var medicine2 = new Medicine("Voltaren", 12, 2, 7) { Id = 2, StatusId = 1, OwnerId = "validUserId" };
        context.Medicines.Add(medicine2);

        var medicine3 = new Medicine("APAP", 3, 3, 6) { Id = 3, StatusId = 0, OwnerId = "secondUserId" };
        context.Medicines.Add(medicine3);

        var medicine4 = new Medicine("Nospa", 4, 3, 6) { Id = 4, StatusId = 1, OwnerId = "validUserId" };
        context.Medicines.Add(medicine4);

        var medicine5 = new Medicine("Ibuprom", 3, 3, 6) { Id = 5, StatusId = 1, OwnerId = "validUserId" };
        context.Medicines.Add(medicine5);

        var log1 = new MedicineLog(medicine2.Id, new DateTime(2023, 3, 12, 13, 50, 0)) { Id = 4, StatusId = 1, OwnerId = "validUserId" };
        context.MedicineLogs.Add(log1);
        var log2 = new MedicineLog(medicine2.Id, new DateTime(2023, 3, 13, 10, 50, 0)) { Id = 5, StatusId = 1, OwnerId = "validUserId" };
        context.MedicineLogs.Add(log2);
        var log3 = new MedicineLog(medicine2.Id, new DateTime(2023, 3, 13, 10, 50, 0)) { Id = 6, StatusId = 0, OwnerId = "validUserId" };
        context.MedicineLogs.Add(log3);
        var log4 = new MedicineLog(medicine2.Id, new DateTime(2023, 3, 13, 10, 50, 0)) { Id = 7, StatusId = 1, OwnerId = "secondUserId" };
        context.MedicineLogs.Add(log4);

        var nurofenLog1 = new MedicineLog(nurofen.Id, new DateTime(2023, 1, 1, 12, 0, 0)) { Id = 100, StatusId = 1, OwnerId = "validUserId" };
        context.MedicineLogs.Add(nurofenLog1);
        var nurofenLog2 = new MedicineLog(nurofen.Id, new DateTime(2023, 1, 1, 16, 0, 0)) { Id = 101, StatusId = 1, OwnerId = "validUserId" };
        context.MedicineLogs.Add(nurofenLog2);
        var nurofenLog3 = new MedicineLog(nurofen.Id, new DateTime(2023, 1, 1, 20, 0, 0)) { Id = 102, StatusId = 1, OwnerId = "validUserId" };
        context.MedicineLogs.Add(nurofenLog3);

        var log7 = new MedicineLog(medicine4.Id, new DateTime(2023, 1, 12, 12, 0, 0)) { Id = 10, StatusId = 1, OwnerId = "validUserId" };
        context.MedicineLogs.Add(log7);
        var log8 = new MedicineLog(medicine4.Id, new DateTime(2023, 1, 12, 16, 20, 0)) { Id = 11, StatusId = 1, OwnerId = "validUserId" };
        context.MedicineLogs.Add(log8);
        var log9 = new MedicineLog(medicine4.Id, new DateTime(2023, 1, 12, 20, 49, 0)) { Id = 12, StatusId = 1, OwnerId = "validUserId" };
        context.MedicineLogs.Add(log9);

        var log10 = new MedicineLog(medicine5.Id, new DateTime(2023, 1, 1, 12, 0, 0)) { Id = 13, StatusId = 1, OwnerId = "validUserId" };
        context.MedicineLogs.Add(log10);
        var log11 = new MedicineLog(medicine5.Id, new DateTime(2023, 1, 1, 18, 20, 0)) { Id = 14, StatusId = 1, OwnerId = "validUserId" };
        context.MedicineLogs.Add(log11);
        var log12 = new MedicineLog(medicine5.Id, new DateTime(2023, 1, 2, 23, 0, 0)) { Id = 15, StatusId = 1, OwnerId = "validUserId" };
        context.MedicineLogs.Add(log12);

        context.SaveChanges();

        return mock is null ? throw new Exception("Could not create db mock") : mock;
    }

    public void Destroy(EndSicknessContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}
