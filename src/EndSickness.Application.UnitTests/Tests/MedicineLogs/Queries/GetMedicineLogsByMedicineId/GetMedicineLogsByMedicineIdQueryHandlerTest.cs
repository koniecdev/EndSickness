using EndSickness.Application.Common.Exceptions;
using EndSickness.Application.MedicineLogs.Queries.GetMedicineLogsByMedicineId;
using EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogsByMedicineId;

namespace EndSickness.Application.UnitTests.Tests.MedicineLogs.Queries.GetMedicineLogsByMedicineId;

[Collection("QueryCollection")]
public class GetMedicineLogsByMedicineIdQueryHandlerTest : QueryTestBase
{
    private readonly GetMedicineLogsByMedicineIdQueryHandler _handler;
    private readonly GetMedicineLogsByMedicineIdQueryValidator _validator;
    private readonly GetMedicineLogsByMedicineIdQueryHandler _handlerUnauthorized;
    private readonly GetMedicineLogsByMedicineIdQueryHandler _handlerForbidden;

    public GetMedicineLogsByMedicineIdQueryHandlerTest() : base()
    {
        _validator = new();
        _handler = new(_context, _mapper, _resourceOwnershipValidUser);
        _handlerUnauthorized = new(_context, _mapper, _resourceOwnershipUnauthorizedUser);
        _handlerForbidden = new(_context, _mapper, _resourceOwnershipInvalidUser);
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
            _validator.Validate(request);
            var response = await _handlerUnauthorized.Handle(request, CancellationToken.None);
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
            _validator.Validate(request);
            var response = await _handlerForbidden.Handle(request, CancellationToken.None);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<ForbiddenAccessException>();
        }
    }
}
