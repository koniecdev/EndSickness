using EndSickness.Application.Common.Exceptions;
using EndSickness.Application.MedicineLogs.Commands.DeleteMedicineLog;
using EndSickness.Shared.MedicineLogs.Commands.DeleteMedicineLog;
using FluentValidation;

namespace EndSickness.Application.UnitTests.Tests.MedicineLogs.Commands.DeleteMedicineLog;

public class DeleteMedicineLogCommandHandlerTest : CommandTestBase
{
    private readonly DeleteMedicineLogCommandHandler _handler;
    private readonly DeleteMedicineLogCommandHandler _unauthorizedUserHandler;
    private readonly DeleteMedicineLogCommandHandler _freshUserHandler;
    private readonly DeleteMedicineLogCommandValidator _validator;

    public DeleteMedicineLogCommandHandlerTest()
    {
        _handler = new(_context, _resourceOwnershipValidUser);
        _unauthorizedUserHandler = new(_context, _resourceOwnershipUnauthorizedUser);
        _freshUserHandler = new(_context, _resourceOwnershipInvalidUser);
        _validator = new();
    }

    [Fact]
    public async Task DeleteMedicineLog_ValidUser_ShouldDelete()
    {
        var id = 4;
        var command = new DeleteMedicineLogCommand(id);
        await ValidateAndHandleRequest(command, _handler);
        _context.MedicineLogs.Where(m => m.StatusId != 0 && m.Id == id).Count().Should().Be(0);
    }

    [Fact]
    public async Task DeleteMedicineLog_UnauthorizedUser_ShouldNotDelete()
    {
        var command = new DeleteMedicineLogCommand(4);
        try
        {
            await ValidateAndHandleRequest(command, _unauthorizedUserHandler);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<UnauthorizedAccessException>();
        }
    }

    [Fact]
    public async Task DeleteMedicineLog_ValidUser_ShouldBeNotFound()
    {
        var command = new DeleteMedicineLogCommand(124214);
        try
        {
            await ValidateAndHandleRequest(command, _freshUserHandler);
            throw new Exception("Test method returned unexpected exception");
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<ResourceNotFoundException>();
        }
    }

    private async Task ValidateAndHandleRequest(DeleteMedicineLogCommand command, DeleteMedicineLogCommandHandler handler)
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