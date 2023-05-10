using EndSickness.Application.Common.Exceptions;
using EndSickness.Application.MedicineLogs.Commands.DeleteMedicineLogsByMedicineId;
using EndSickness.Shared.MedicineLogs.Commands.DeleteMedicineLogsByMedicineId;
using FluentValidation;

namespace EndSickness.Application.UnitTests.Tests.MedicineLogs.Commands.DeleteMedicineLogsByMedicineId;

public class DeleteMedicineLogsByMedicineIdCommandHandlerTest : CommandTestBase
{
    private readonly DeleteMedicineLogsByMedicineIdCommandHandler _handler;
    private readonly DeleteMedicineLogsByMedicineIdCommandValidator _validator;
    private readonly DeleteMedicineLogsByMedicineIdCommandValidator _validatorUnauthorized;
    private readonly DeleteMedicineLogsByMedicineIdCommandValidator _validatorForbidden;

    public DeleteMedicineLogsByMedicineIdCommandHandlerTest()
    {
        _handler = new(_context);
        _validator = new(_context, _resourceOwnershipValidUser);
        _validatorUnauthorized = new(_context, _resourceOwnershipUnauthorizedUser);
        _validatorForbidden = new(_context, _resourceOwnershipForbiddenUser);
    }

    [Fact]
    public async Task DeleteMedicineLogsByMedicineId_ValidUser_ShouldDelete()
    {
        var medicineId = 4;
        var command = new DeleteMedicineLogsByMedicineIdCommand(medicineId);
        await ValidateAndHandleRequest(command, _validator);
        _context.MedicineLogs.Where(m => m.StatusId != 0 && m.MedicineId == medicineId).Count().Should().Be(0);
    }

    [Fact]
    public async Task DeleteMedicineLogsByMedicineId_UnauthorizedUser_ShouldNotDelete()
    {
        var command = new DeleteMedicineLogsByMedicineIdCommand(4);
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
    public async Task DeleteMedicineLogsByMedicineId_ForbiddenUser_ShouldNotDelete()
    {
        var command = new DeleteMedicineLogsByMedicineIdCommand(4);
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
    public async Task DeleteMedicineLogsByMedicineId_ValidUser_ShouldBeNotFound()
    {
        var command = new DeleteMedicineLogsByMedicineIdCommand(124214);
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

    private async Task ValidateAndHandleRequest(DeleteMedicineLogsByMedicineIdCommand command, DeleteMedicineLogsByMedicineIdCommandValidator validator)
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