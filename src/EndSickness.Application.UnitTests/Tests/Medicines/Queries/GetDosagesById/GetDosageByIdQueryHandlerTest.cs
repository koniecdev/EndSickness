using EndSickness.Application.Medicines.Queries.GetDosageById;
using EndSickness.Application.Services.CalculateDosage;
using EndSickness.Shared.Medicines.Queries.GetDosageById;

namespace EndSickness.Application.UnitTests.Tests.Medicines.Queries.GetDosageById;

[Collection("QueryCollection")]
public class GetDosageByIdQueryHandlerTest : QueryTestBase
{
    private readonly GetDosageByIdQueryHandler _handler;
    private readonly GetDosageByIdQueryValidator _validator;

    public GetDosageByIdQueryHandlerTest() : base()
    {
        _handler = new(_context, new CalculateNeariestDosageService());
        _validator = new(_context, _resourceOwnershipValidUser);
    }

    [Fact]
    public async Task ValidDataset_ValidUser_NextDoseShouldBeNext24HFromFirstDose()
    {
        var request = new GetDosageByIdQuery(4);
        await _validator.ValidateAsync(request);
        var fromDb = await _handler.Handle(request, CancellationToken.None);
        fromDb.LastDose.Should().Be(new TimeOnly(20, 49, 0));
        fromDb.NextDose.Should().Be(new TimeOnly(12, 0, 0));
        fromDb.MedicineName.Should().Be("Nospa");
    }

    [Fact]
    public async Task ValidDataset_ValidUser_NextDoseShouldBeOverNext24HFromFirstDose()
    {
        var request = new GetDosageByIdQuery(5);
        await _validator.ValidateAsync(request);
        var fromDb = await _handler.Handle(request, CancellationToken.None);
        fromDb.LastDose.Should().Be(new TimeOnly(10, 0, 0));
        fromDb.NextDose.Should().Be(new TimeOnly(13, 0, 0));
        fromDb.MedicineName.Should().Be("Ibuprom");
    }
}
