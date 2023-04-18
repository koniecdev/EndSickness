using EndSickness.Application.Medicines.Queries.GetDosageById;
using EndSickness.Shared.Medicines.Queries.GetDosageById;

namespace EndSickness.Application.UnitTests.Tests.Medicines.Queries.GetDosageById;

[Collection("QueryCollection")]
public class GetDosageByIdQueryHandlerTest : QueryTestBase
{
    private readonly GetDosageByIdQueryHandler _handler;

    public GetDosageByIdQueryHandlerTest() : base()
    {
        _handler = new(_context, _resourceOwnershipValidUser);
    }

    [Fact]
    public async Task ValidDataset_ValidUser_ShouldBeValid()
    {
        var request = new GetDosageByIdQuery(1);
        var fromDb = await _handler.Handle(request, CancellationToken.None);
        fromDb.LastDose.Should().Be(new TimeOnly(12, 0, 0));
        fromDb.NextDose.Should().Be(new TimeOnly(16, 0, 0));
        fromDb.TakeUntil.Should().Be(new DateOnly(2023, 3, 19));
        fromDb.MedicineName.Should().Be("Nurofen");
    }

    [Fact]
    public async Task ValidDataset_ValidUser_ShouldCalculateNextDayNextDose()
    {
        var request = new GetDosageByIdQuery(4);
        var fromDb = await _handler.Handle(request, CancellationToken.None);
        fromDb.LastDose.Should().Be(new TimeOnly(20, 49, 0));
        fromDb.NextDose.Should().Be(new TimeOnly(12, 0, 0));
        fromDb.TakeUntil.Should().Be(new DateOnly(2023, 1, 18));
        fromDb.MedicineName.Should().Be("Nospa");
    }

    [Fact]
    public async Task ValidDataset_ValidUser_ShouldCalculateNextDayNextDoseWithLastDoseOverlap()
    {
        var request = new GetDosageByIdQuery(5);
        var fromDb = await _handler.Handle(request, CancellationToken.None);
        fromDb.LastDose.Should().Be(new TimeOnly(11, 0, 0));
        fromDb.NextDose.Should().Be(new TimeOnly(14, 0, 0));
    }
}
