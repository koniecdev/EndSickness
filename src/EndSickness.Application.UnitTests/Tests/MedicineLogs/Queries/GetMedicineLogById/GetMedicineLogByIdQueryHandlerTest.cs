using EndSickness.Application.Common.Exceptions;
using EndSickness.Application.MedicineLogs.Queries.GetMedicineLogById;
using EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogById;

namespace EndSickness.Application.UnitTests.Tests.MedicineLogs.Queries.GetMedicineLogById;

[Collection("QueryCollection")]
public class GetMedicineLogByIdQueryHandlerTest : QueryTestBase
{
    private readonly GetMedicineLogByIdQueryHandler _handler;
    private readonly GetMedicineLogByIdQueryHandler _notAuthorizedUserHandler;
    private readonly GetMedicineLogByIdQueryHandler _forbiddenUserHandler;

    public GetMedicineLogByIdQueryHandlerTest() : base()
    {
        _handler = new(_context, _mapper, _resourceOwnershipValidUser);
        _notAuthorizedUserHandler = new(_context, _mapper, _resourceOwnershipUnauthorizedUser);
        _forbiddenUserHandler = new(_context, _mapper, _resourceOwnershipInvalidUser);
    }

    [Fact]
    public async Task GetMedicineLogByIdQueryTest_ShouldBeValid()
    {
        var result = await _handler.Handle(new GetMedicineLogByIdQuery(4), CancellationToken.None);
        result.Id.Should().Be(4);
    }

    [Fact]
    public async Task GetMedicineLogByIdQueryTest_ShouldAllProperiesBePopulated()
    {
        var newData = new DateTime(2023, 3, 12, 13, 50, 0);
        var result = await _handler.Handle(new GetMedicineLogByIdQuery(4), CancellationToken.None);
        (result.Id == 4 && result.LastlyTaken.Equals(newData) && result.Medicine.Name.Equals("Voltaren"))
            .Should().Be(true);
    }

    [Fact]
    public async Task GetMedicineLogByIdQueryTest_QueryForDeletedResource()
    {
        try
        {
            var result = await _handler.Handle(new GetMedicineLogByIdQuery(6), CancellationToken.None);
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
            var result = await _notAuthorizedUserHandler.Handle(new GetMedicineLogByIdQuery(4), CancellationToken.None);
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
            var result = await _forbiddenUserHandler.Handle(new GetMedicineLogByIdQuery(4), CancellationToken.None);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<ForbiddenAccessException>();
        }
    }
}
