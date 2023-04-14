using EndSickness.Application.Common.Exceptions;
using EndSickness.Application.Medicines.Commands.DeleteMedicine;
using EndSickness.Shared.Medicines.Commands.DeleteMedicine;
using FluentValidation;

namespace EndSickness.Application.UnitTests.Tests.Medicines.Commands.DeleteMedicine;

public class DeleteMedicineCommandHandlerTest : CommandTestBase
{
    private readonly DeleteMedicineCommandHandler _handler;
    private readonly DeleteMedicineCommandHandler _unauthorizedUserHandler;
    private readonly DeleteMedicineCommandHandler _freshUserHandler;
    private readonly DeleteMedicineCommandValidator _validator;

    public DeleteMedicineCommandHandlerTest()
    {
        _handler = new(_context, _resourceOwnershipValidUser);
        _unauthorizedUserHandler = new(_context, _resourceOwnershipUnauthorizedUser);
        _freshUserHandler = new(_context, _resourceOwnershipInvalidUser);
        _validator = new();
    }

    [Fact]
    public async Task DeleteMedicine_ShouldDelete()
    {
        var id = 1;
        var command = new DeleteMedicineCommand(id);
        await ValidateAndHandleRequest(command, _handler);
        _context.Medicines.Where(m => m.StatusId != 0 && m.Id == id).Count().Should().Be(0);
    }

    [Fact]
    public async Task DeleteMedicine_UnauthorizedUser_ShouldNotDelete()
    {
        var command = new DeleteMedicineCommand(1);
        try
        {
            await ValidateAndHandleRequest(command, _unauthorizedUserHandler);
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
            await ValidateAndHandleRequest(command, _freshUserHandler);
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
            await ValidateAndHandleRequest(command, _freshUserHandler);
            throw new Exception("Test method returned unexpected exception");
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<ResourceNotFoundException>();
        }
    }

    private async Task ValidateAndHandleRequest(DeleteMedicineCommand command, DeleteMedicineCommandHandler handler)
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