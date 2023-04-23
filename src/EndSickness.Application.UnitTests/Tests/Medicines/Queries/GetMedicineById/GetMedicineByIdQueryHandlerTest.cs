using EndSickness.Application.Common.Exceptions;
using EndSickness.Application.Medicines.Queries.GetMedicineById;
using EndSickness.Shared.Medicines.Queries.GetMedicineById;

namespace EndSickness.Application.UnitTests.Tests.Medicines.Queries.GetMedicineById;

[Collection("QueryCollection")]
public class GetMedicineByIdQueryHandlerTest : QueryTestBase
{
    private readonly GetMedicineByIdQueryHandler _handler;
    private readonly GetMedicineByIdQueryValidator _validate;
    private readonly GetMedicineByIdQueryValidator _validateUnauthorized;
    private readonly GetMedicineByIdQueryValidator _validateForbidden;

    public GetMedicineByIdQueryHandlerTest() : base()
    {
        _handler = new(_context, _mapper);
        _validate = new(_context, _resourceOwnershipValidUser);
        _validateUnauthorized = new(_context, _resourceOwnershipUnauthorizedUser);
        _validateForbidden = new(_context, _resourceOwnershipInvalidUser);
    }

    [Fact]
    public async Task GetMedicineByIdQueryTest_ShouldBeValid()
    {
        var query = new GetMedicineByIdQuery(1);
        await _validate.ValidateAsync(query);
        var result = await _handler.Handle(query, CancellationToken.None);
        result.Id.Should().Be(1);
    }

    [Fact]
    public async Task GetMedicineByIdQueryTest_ShouldAllProperiesBePopulated()
    {
        var query = new GetMedicineByIdQuery(1);
        await _validate.ValidateAsync(query);
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
            await _validate.ValidateAsync(query);
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
            await _validate.ValidateAsync(query);
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
            await _validateUnauthorized.ValidateAsync(query);
            var result = await _handler.Handle(query, CancellationToken.None);
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
            await _validateForbidden.ValidateAsync(query);
            var result = await _handler.Handle(query, CancellationToken.None);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<ForbiddenAccessException>();
        }
    }
}
