using EndSickness.Application.Common.Exceptions;
using EndSickness.Application.MedicineLogs.Queries.GetMedicineLogById;
using EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogById;

namespace EndSickness.Application.UnitTests.Tests.MedicineLogs.Queries.GetMedicineLogById;

[Collection("QueryCollection")]
public class GetMedicineLogByIdQueryHandlerTest : QueryTestBase
{
    private readonly GetMedicineLogByIdQueryHandler _handler;
    private readonly GetMedicineLogByIdQueryValidator _validator;
    private readonly GetMedicineLogByIdQueryValidator _validatorUnauthorized;
    private readonly GetMedicineLogByIdQueryValidator _validatorForbidden;

    public GetMedicineLogByIdQueryHandlerTest() : base()
    {
        _handler = new(_context, _mapper);
        _validator = new(_context, _resourceOwnershipValidUser);
        _validatorUnauthorized = new(_context, _resourceOwnershipUnauthorizedUser);
        _validatorForbidden = new(_context, _resourceOwnershipInvalidUser);
    }

    [Fact]
    public async Task GetMedicineLogByIdQueryTest_ShouldBeValid()
    {
        var query = new GetMedicineLogByIdQuery(4);
        await _validator.ValidateAsync(query);
        var result = await _handler.Handle(query, CancellationToken.None);
        result.Id.Should().Be(4);
    }

    [Fact]
    public async Task GetMedicineLogByIdQueryTest_ShouldAllProperiesBePopulated()
    {
        var query = new GetMedicineLogByIdQuery(4);
        await _validator.ValidateAsync(query);
        var newData = new DateTime(2023, 3, 12, 13, 50, 0);
        var result = await _handler.Handle(query, CancellationToken.None);
        (result.Id == 4 && result.LastlyTaken.Equals(newData) && result.Medicine.Name.Equals("Voltaren"))
            .Should().Be(true);
    }

    [Fact]
    public async Task GetMedicineLogByIdQueryTest_QueryForDeletedResource()
    {
        try
        {
            var query = new GetMedicineLogByIdQuery(6);
            await _validator.ValidateAsync(query);
            var result = await _handler.Handle(query, CancellationToken.None);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<ResourceNotFoundException>();
        }
    }

    [Fact]
    public async Task GetMedicineLogByIdQueryTest_ShouldBeNotFound()
    {
        try
        {
            var query = new GetMedicineLogByIdQuery(1337);
            await _validator.ValidateAsync(query);
            var result = await _handler.Handle(new GetMedicineLogByIdQuery(1337), CancellationToken.None);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<ResourceNotFoundException>();
        }
    }

    [Fact]
    public async Task GetMedicineLogByIdQueryTest_ShouldBeNotAuthorized()
    {
        try
        {
            var query = new GetMedicineLogByIdQuery(4);
            await _validatorUnauthorized.ValidateAsync(query);
            var result = await _handler.Handle(query, CancellationToken.None);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<UnauthorizedAccessException>();
        }
    }

    [Fact]
    public async Task GetMedicineLogByIdQueryTest_ShouldBeForbidden()
    {
        try
        {
            var query = new GetMedicineLogByIdQuery(4);
            await _validatorForbidden.ValidateAsync(query);
            var result = await _handler.Handle(new GetMedicineLogByIdQuery(4), CancellationToken.None);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<ForbiddenAccessException>();
        }
    }
}
