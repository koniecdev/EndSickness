using EndSickness.Application.Common.Exceptions;
using EndSickness.Application.MedicineLogs.Commands.DeleteMedicineLog;
using EndSickness.Shared.MedicineLogs.Commands.DeleteMedicineLog;
using FluentValidation;

namespace EndSickness.Application.UnitTests.Tests.MedicineLogs.Commands.DeleteMedicineLog;

public class DeleteMedicineLogCommandHandlerTest : CommandTestBase
{
    private readonly DeleteMedicineLogCommandHandler _handler;
    private readonly DeleteMedicineLogCommandValidator _validator;
    private readonly DeleteMedicineLogCommandValidator _validatorUnauthorized;
    private readonly DeleteMedicineLogCommandValidator _validatorForbidden;

    public DeleteMedicineLogCommandHandlerTest()
    {
        _handler = new(_context);
        _validator = new(_context, _resourceOwnershipValidUser);
        _validatorUnauthorized = new(_context, _resourceOwnershipUnauthorizedUser);
        _validatorForbidden = new(_context, _resourceOwnershipForbiddenUser);
    }

    [Fact]
    public async Task DeleteMedicineLog_ValidUser_ShouldDelete()
    {
        var id = 4;
        var command = new DeleteMedicineLogCommand(id);
        await ValidateAndHandleRequest(command, _validator);
        _context.MedicineLogs.Where(m => m.StatusId != 0 && m.Id == id).Count().Should().Be(0);
    }

    [Fact]
    public async Task DeleteMedicineLog_UnauthorizedUser_ShouldNotDelete()
    {
        var command = new DeleteMedicineLogCommand(4);
        try
        {
            await ValidateAndHandleRequest(command, _validatorUnauthorized);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<UnauthorizedAccessException>();
        }
    }

    [Fact]
    public async Task DeleteMedicineLog_ForbiddenUser_ShouldNotDelete()
    {
        var command = new DeleteMedicineLogCommand(4);
        try
        {
            await ValidateAndHandleRequest(command, _validatorForbidden);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<ForbiddenAccessException>();
        }
    }

    [Fact]
    public async Task DeleteMedicineLog_ValidUser_ShouldBeNotFound()
    {
        var command = new DeleteMedicineLogCommand(124214);
        try
        {
            await ValidateAndHandleRequest(command, _validator);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<ResourceNotFoundException>();
        }
    }

    private async Task ValidateAndHandleRequest(DeleteMedicineLogCommand command, DeleteMedicineLogCommandValidator validator)
    {
        var validationResult = await validator.ValidateAsync(command);
        if (validationResult.IsValid)
        {
            await _handler.Handle(command, CancellationToken.None);
        }
        else
        {
            throw new ValidationException(validationResult.Errors);
        }
    }
}