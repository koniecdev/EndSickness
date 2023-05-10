using EndSickness.Application.Medicines.Queries.GetDosages;
using EndSickness.Application.Services.CalculateDosage;
using EndSickness.Shared.Medicines.Queries.GetDosages;

namespace EndSickness.Application.UnitTests.Tests.Medicines.Queries.GetDosages;

[Collection("QueryCollection")]
public class GetDosagesQueryHandlerTest : QueryTestBase
{
    private readonly GetDosagesQueryHandler _handler;

    public GetDosagesQueryHandlerTest() : base()
    {
        _handler = new(_context, _currentUser, new CalculateNeariestDosageService());
    }

    [Fact]
    public async Task ValidDataset_ValidUser_ShouldReturnEverythingCorrectly()
    {
        var request = new GetDosagesQuery();
        var result = await _handler.Handle(request, CancellationToken.None);
        result.Dosages.Count.Should().Be(4);
        (result.Dosages.Any(m => m.MedicineName == "Nurofen") && result.Dosages.Any(m => m.MedicineName == "Voltaren")
            && result.Dosages.Any(m => m.MedicineName == "Nospa") && result.Dosages.Any(m => m.MedicineName == "Ibuprom")).Should().BeTrue();
        var ibupromDosages = result.Dosages.First(m => m.MedicineId == 5);
        ibupromDosages.MedicineName.Should().Be("Ibuprom");
        ibupromDosages.NextDose.Should().Be(new DateTime(2023, 1, 2, 13, 0, 0));
        ibupromDosages.LastDose.Should().Be(new DateTime(2023, 1, 2, 10, 0, 0));
    }

}
