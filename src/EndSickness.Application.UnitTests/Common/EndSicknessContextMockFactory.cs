using EndSickness.Domain.Entities;

namespace EndSickness.Application.UnitTests.Common;

public class EndSicknessContextMockFactory : IDbContextMockFactory<EndSicknessContext>
{
    public Mock<EndSicknessContext> Create()
    {
        var options = new DbContextOptionsBuilder<EndSicknessContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        var mock = new Mock<EndSicknessContext>(options) { CallBase = true };

        var context = mock.Object;

        context.Database.EnsureCreated();

        //Remember - Seed of database are also applied - keep tracking of int IDs

        var appUser1 = new AppUser("slayId", "secondUser@koniec.dev", "secondUser") { Id = 1337, StatusId = 1 };
        context.AppUsers.Add(appUser1);

        var medicine = new Medicine("Nurofen", TimeSpan.FromHours(4), appUser1.Id, 3, TimeSpan.FromDays(7)) { Id = 1, StatusId = 1 };
        context.Medicines.Add(medicine);

        var medicine1 = new Medicine("Voltaren", TimeSpan.FromHours(12), appUser1.Id, 2) { Id = 2, StatusId = 1 };
        context.Medicines.Add(medicine1);

        var log1 = new MedicineLog(appUser1.Id, medicine1.Id, new DateTime(2023, 3, 12, 13, 50, 0)) { Id = 1, StatusId = 1 };
        context.MedicineLogs.Add(log1);
        var log2 = new MedicineLog(appUser1.Id, medicine1.Id, new DateTime(2023, 3, 13, 10, 50, 0)) { Id = 2, StatusId = 1 };
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
