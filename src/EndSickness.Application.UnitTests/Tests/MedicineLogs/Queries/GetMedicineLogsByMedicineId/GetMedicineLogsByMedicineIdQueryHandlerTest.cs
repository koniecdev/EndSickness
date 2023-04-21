using EndSickness.Application.Common.Exceptions;
using EndSickness.Application.MedicineLogs.Queries.GetMedicineLogsByMedicineId;
using EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogsByMedicineId;

namespace EndSickness.Application.UnitTests.Tests.MedicineLogs.Queries.GetMedicineLogsByMedicineId;

[Collection("QueryCollection")]
public class GetMedicineLogsByMedicineIdQueryHandlerTest : QueryTestBase
{
    private readonly GetMedicineLogsByMedicineIdQueryHandler _handler;
    private readonly GetMedicineLogsByMedicineIdQueryValidator _validator;
    private readonly GetMedicineLogsByMedicineIdQueryValidator _validatorUnauthorized;
    private readonly GetMedicineLogsByMedicineIdQueryValidator _validatorForbidden;

    public GetMedicineLogsByMedicineIdQueryHandlerTest() : base()
    {
        _handler = new(_context, _mapper);
        _validator = new(_context, _resourceOwnershipValidUser);
        _validatorUnauthorized = new(_context, _resourceOwnershipUnauthorizedUser);
        _validatorForbidden = new(_context, _resourceOwnershipInvalidUser);
    }

    [Fact]
    public async Task ValidDataset_ValidUser_ShouldReturnEverythingCorrectly()
    {
        var request = new GetMedicineLogsByMedicineIdQuery(1);
        await _validator.ValidateAsync(request);
        var response = await _handler.Handle(request, CancellationToken.None);
        response.MedicineId.Should().Be(1);
        response.MedicineLogs.Count.Should().Be(3);
        response.MedicineLogs.First().LastlyTaken.Should().Be(new DateTime(2023, 1, 1, 12, 0, 0));
        response.MedicineLogs.ElementAt(1).LastlyTaken.Should().Be(new DateTime(2023, 1, 1, 16, 0, 0));
        response.MedicineLogs.ElementAt(2).LastlyTaken.Should().Be(new DateTime(2023, 1, 1, 20, 0, 0));
    }

    [Fact]
    public async Task ValidDataset_ValidUser_ShouldBeUnauthorized()
    {
        try
        {
            var request = new GetMedicineLogsByMedicineIdQuery(1);
            await _validatorUnauthorized.ValidateAsync(request);
            var response = await _handler.Handle(request, CancellationToken.None);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<UnauthorizedAccessException>();
        }
    }
    [Fact]
    public async Task ValidDataset_ValidUser_ShouldBeForbidden()
    {
        try
        {
            var request = new GetMedicineLogsByMedicineIdQuery(1);
            await _validatorForbidden.ValidateAsync(request);
            var response = await _handler.Handle(request, CancellationToken.None);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<ForbiddenAccessException>();
        }
    }
}
