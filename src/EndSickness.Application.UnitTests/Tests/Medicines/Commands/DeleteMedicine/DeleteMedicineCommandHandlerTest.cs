using EndSickness.Application.Common.Exceptions;
using EndSickness.Application.Medicines.Commands.DeleteMedicine;
using EndSickness.Shared.Medicines.Commands.DeleteMedicine;
using FluentValidation;

namespace EndSickness.Application.UnitTests.Tests.Medicines.Commands.DeleteMedicine;

public class DeleteMedicineCommandHandlerTest : CommandTestBase
{
    private readonly DeleteMedicineCommandHandler _handler;
    private readonly DeleteMedicineCommandValidator _validator;
    private readonly DeleteMedicineCommandValidator _validatorUnauthorized;
    private readonly DeleteMedicineCommandValidator _validatorForbidden;

    public DeleteMedicineCommandHandlerTest()
    {
        _handler = new(_context);
        _validator = new(_context, _resourceOwnershipValidUser);
        _validatorUnauthorized = new(_context, _resourceOwnershipUnauthorizedUser);
        _validatorForbidden = new(_context, _resourceOwnershipForbiddenUser);
    }

    [Fact]
    public async Task DeleteMedicine_ShouldDelete()
    {
        var id = 1;
        var command = new DeleteMedicineCommand(id);
        await ValidateAndHandleRequest(command, _validator);
        _context.Medicines.Where(m => m.StatusId != 0 && m.Id == id).Count().Should().Be(0);
    }

    [Fact]
    public async Task DeleteMedicine_UnauthorizedUser_ShouldNotDelete()
    {
        var command = new DeleteMedicineCommand(1);
        try
        {
            await ValidateAndHandleRequest(command, _validatorUnauthorized);
            throw new Exception("Test method returned unexpected exception");
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<UnauthorizedAccessException>();
        }
    }

    [Fact]
    public async Task DeleteMedicine_NotAuthenticatedUser_ShouldNotDelete()
    {
        var command = new DeleteMedicineCommand(1);
        try
        {
            await ValidateAndHandleRequest(command, _validatorForbidden);
            throw new Exception("Test method returned unexpected exception");
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<ForbiddenAccessException>();
        }
    }

    [Fact]
    public async Task DeleteMedicine_ValidUser_ShouldBeNotFound()
    {
        var command = new DeleteMedicineCommand(124214);
        try
        {
            await ValidateAndHandleRequest(command, _validator);
            throw new Exception("Test method returned unexpected exception");
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<ResourceNotFoundException>();
        }
    }

    private async Task ValidateAndHandleRequest(DeleteMedicineCommand command, DeleteMedicineCommandValidator validator)
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