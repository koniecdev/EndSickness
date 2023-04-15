using EndSickness.Application.Common.Exceptions;
using EndSickness.Application.Medicines.Queries.GetMedicineById;
using EndSickness.Shared.Medicines.Queries.GetMedicineById;

namespace EndSickness.Application.UnitTests.Tests.Medicines.Queries.GetMedicineById;

[Collection("QueryCollection")]
public class GetMedicineByIdQueryHandlerTest : QueryTestBase
{
    private readonly GetMedicineByIdQueryHandler _handler;
    private readonly GetMedicineByIdQueryHandler _notAuthorizedUserHandler;
    private readonly GetMedicineByIdQueryHandler _forbiddenUserHandler;

    public GetMedicineByIdQueryHandlerTest() : base()
    {
        _handler = new(_context, _mapper, _resourceOwnershipValidUser);
        _notAuthorizedUserHandler = new(_context, _mapper, _resourceOwnershipUnauthorizedUser);
        _forbiddenUserHandler = new(_context, _mapper, _resourceOwnershipInvalidUser);
    }

    [Fact]
    public async Task GetMedicineByIdQueryTest_ShouldBeValid()
    {
        var result = await _handler.Handle(new GetMedicineByIdQuery(1), CancellationToken.None);
        result.Id.Should().Be(1);
    }

    [Fact]
    public async Task GetMedicineByIdQueryTest_ShouldAllProperiesBePopulated()
    {
        var result = await _handler.Handle(new GetMedicineByIdQuery(1), CancellationToken.None);
        (result.Id == 1 && result.Name == "Nurofen" && result.HourlyCooldown == 4
            && result.MaxDailyAmount == 3
            && result.MaxDaysOfTreatment == 7)
            .Should().Be(true);
    }

    [Fact]
    public async Task GetMedicineByIdQueryTest_QueryForDeletedResource()
    {
        try
        {
            var result = await _handler.Handle(new GetMedicineByIdQuery(3), CancellationToken.None);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<ResourceNotFoundException>();
        }
    }

    [Fact]
    public async Task GetMedicineByIdQueryTest_ShouldBeNotFound()
    {
        try
        {
            var result = await _handler.Handle(new GetMedicineByIdQuery(1337), CancellationToken.None);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch(Exception ex)
        {
            ex.Should().BeOfType<ResourceNotFoundException>();
        }
    }

    [Fact]
    public async Task GetMedicineByIdQueryTest_ShouldBeNotAuthorized()
    {
        try
        {
            var result = await _notAuthorizedUserHandler.Handle(new GetMedicineByIdQuery(1), CancellationToken.None);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<UnauthorizedAccessException>();
        }
    }

    [Fact]
    public async Task GetMedicineByIdQueryTest_ShouldBeForbidden()
    {
        try
        {
            var result = await _forbiddenUserHandler.Handle(new GetMedicineByIdQuery(1), CancellationToken.None);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<ForbiddenAccessException>();
        }
    }
}
