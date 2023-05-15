using EndSickness.Application.Common.Exceptions;
using EndSickness.Application.MedicineLogs.Commands.DeleteMedicineLogsByMedicineId;
using EndSickness.Shared.MedicineLogs.Commands.DeleteMedicineLogsByMedicineId;
using FluentValidation;

namespace EndSickness.Application.UnitTests.Tests.MedicineLogs.Commands.DeleteMedicineLogsByMedicineId;

public class DeleteMedicineLogsByMedicineIdCommandHandlerTest : CommandTestBase
{
    private readonly DeleteMedicineLogsByMedicineIdCommandValidator _validator;
    private readonly DeleteMedicineLogsByMedicineIdCommandHandler _handler;
    private readonly DeleteMedicineLogsByMedicineIdCommandHandler _handlerUnauthorized;
    private readonly DeleteMedicineLogsByMedicineIdCommandHandler _handlerForbidden;

    public DeleteMedicineLogsByMedicineIdCommandHandlerTest()
    {
        _validator = new();
        _handler = new(_context, _resourceOwnershipValidUser);
        _handlerUnauthorized = new(_context, _resourceOwnershipUnauthorizedUser);
        _handlerForbidden = new(_context, _resourceOwnershipForbiddenUser);
    }

    [Fact]
    public async Task DeleteMedicineLogsByMedicineId_ValidUser_ShouldDelete()
    {
        var medicineId = 4;
        var command = new DeleteMedicineLogsByMedicineIdCommand(medicineId);
        await ValidateAndHandleRequest(command, _handler);
        _context.MedicineLogs.Where(m => m.StatusId != 0 && m.MedicineId == medicineId).Count().Should().Be(0);
    }

    [Fact]
    public async Task DeleteMedicineLogsByMedicineId_UnauthorizedUser_ShouldNotDelete()
    {
        var command = new DeleteMedicineLogsByMedicineIdCommand(4);
        try
        {
            await ValidateAndHandleRequest(command, _handlerUnauthorized);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<UnauthorizedAccessException>();
        }
    }

    [Fact]
    public async Task DeleteMedicineLogsByMedicineId_ForbiddenUser_ShouldNotDelete()
    {
        var command = new DeleteMedicineLogsByMedicineIdCommand(4);
        try
        {
            await ValidateAndHandleRequest(command, _handlerForbidden);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<ForbiddenAccessException>();
        }
    }

    [Fact]
    public async Task DeleteMedicineLogsByMedicineId_ValidUser_ShouldBeNotFound()
    {
        var command = new DeleteMedicineLogsByMedicineIdCommand(124214);
        try
        {
            await ValidateAndHandleRequest(command, _handler);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<ResourceNotFoundException>();
        }
    }

    private async Task ValidateAndHandleRequest(DeleteMedicineLogsByMedicineIdCommand command, DeleteMedicineLogsByMedicineIdCommandHandler handler)
    {
        var validationResult = _validator.Validate(command);
        if (validationResult.IsValid)
        {
            await handler.Handle(command, CancellationToken.None);
        }
        else
        {
            throw new ValidationException(validationResult.Errors);
        }
    }
}