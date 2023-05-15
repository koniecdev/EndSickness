using EndSickness.Application.Common.Exceptions;
using EndSickness.Application.Medicines.Queries.GetMedicineById;
using EndSickness.Shared.Medicines.Queries.GetMedicineById;

namespace EndSickness.Application.UnitTests.Tests.Medicines.Queries.GetMedicineById;

[Collection("QueryCollection")]
public class GetMedicineByIdQueryHandlerTest : QueryTestBase
{
    private readonly GetMedicineByIdQueryValidator _validate;
    private readonly GetMedicineByIdQueryHandler _handler;
    private readonly GetMedicineByIdQueryHandler _handlerUnauthorized;
    private readonly GetMedicineByIdQueryHandler _handlerForbidden;

    public GetMedicineByIdQueryHandlerTest() : base()
    {
        _validate = new();
        _handler = new(_context, _mapper, _resourceOwnershipValidUser);
        _handlerUnauthorized = new(_context, _mapper, _resourceOwnershipUnauthorizedUser);
        _handlerForbidden = new(_context, _mapper, _resourceOwnershipInvalidUser);
    }

    [Fact]
    public async Task GetMedicineByIdQueryTest_ShouldBeValid()
    {
        var query = new GetMedicineByIdQuery(1);
        _validate.Validate(query);
        var result = await _handler.Handle(query, CancellationToken.None);
        result.Id.Should().Be(1);
    }

    [Fact]
    public async Task GetMedicineByIdQueryTest_ShouldAllProperiesBePopulated()
    {
        var query = new GetMedicineByIdQuery(1);
        _validate.Validate(query);
        var result = await _handler.Handle(query, CancellationToken.None);
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
            var query = new GetMedicineByIdQuery(3);
            _validate.Validate(query);
            var result = await _handler.Handle(query, CancellationToken.None);
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
            var query = new GetMedicineByIdQuery(3);
            _validate.Validate(query);
            var result = await _handler.Handle(query, CancellationToken.None);
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
            var query = new GetMedicineByIdQuery(1);
            _validate.Validate(query);
            var result = await _handlerUnauthorized.Handle(query, CancellationToken.None);
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
            var query = new GetMedicineByIdQuery(1);
            _validate.Validate(query);
            var result = await _handlerForbidden.Handle(query, CancellationToken.None);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<ForbiddenAccessException>();
        }
    }
}
