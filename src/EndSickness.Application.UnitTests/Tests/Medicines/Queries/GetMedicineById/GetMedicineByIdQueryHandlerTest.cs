using EndSickness.Application.Medicines.Queries.GetMedicineById;
using EndSickness.Shared.Medicines.Queries.GetMedicineById;

namespace EndSickness.Application.UnitTests.Tests.Medicines.Queries.GetMedicineById;

[Collection("QueryCollection")]
public class GetMedicineByIdQueryHandlerTest : QueryTestBase
{
    private readonly GetMedicineByIdQueryHandler _handler;
    private readonly GetMedicineByIdQueryHandler _notAuthorizedHandler;

    public GetMedicineByIdQueryHandlerTest() : base()
    {
        _handler = new(_context, _mapper, _currentUser);
        _notAuthorizedHandler = new(_context, _mapper, _unauthorizedCurrentUser);
    }

    [Fact]
    public async Task GetMedicineByIdQueryTest_ShouldBeValid()
    {
        var result = await _handler.Handle(new GetMedicineByIdQuery(1), CancellationToken.None);
        result.Id.Should().Be(1);
    }

    [Fact]
    public async Task GetMedicineByIdQueryTest_ShouldNotFindDeleted()
    {
        try
        {
            var result = await _handler.Handle(new GetMedicineByIdQuery(3), CancellationToken.None);
            throw new Exception("Test method did not threw an expected exception");
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<InvalidOperationException>();
        }
    }

    [Fact]
    public async Task GetMedicineByIdQueryTest_ShouldAllProperiesBePopulated()
    {
        var result = await _handler.Handle(new GetMedicineByIdQuery(1), CancellationToken.None);
        (result.Id == 1 && result.Name == "Nurofen" && result.Cooldown.Equals(TimeSpan.FromHours(4))
            && result.AppUserId == 1337 && result.MaxDailyAmount == 3
            && result.MaxDaysOfTreatment.Equals(TimeSpan.FromDays(7)))
            .Should().Be(true);
    }

    [Fact]
    public async Task GetMedicineByIdQueryTest_ShouldBeNotFound()
    {
        try
        {
            var result = await _handler.Handle(new GetMedicineByIdQuery(1337), CancellationToken.None);
            throw new Exception("Test method did not threw expected exception");
        }
        catch(Exception ex)
        {
            ex.Should().BeOfType<InvalidOperationException>();
        }
    }

    [Fact]
    public async Task GetMedicineByIdQueryTest_ShouldBeNotAuthorized()
    {
        try
        {
            var result = await _notAuthorizedHandler.Handle(new GetMedicineByIdQuery(1), CancellationToken.None);
            throw new Exception("Test method did not threw expected exception");
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<UnauthorizedAccessException>();
        }
    }
}
