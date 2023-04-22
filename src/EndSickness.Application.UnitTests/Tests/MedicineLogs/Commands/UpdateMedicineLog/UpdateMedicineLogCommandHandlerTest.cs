using EndSickness.Application.Common.Exceptions;
using EndSickness.Application.MedicineLogs.Commands.UpdateMedicineLog;
using EndSickness.Shared.MedicineLogs.Commands.UpdateMedicineLog;
using FluentValidation;

namespace EndSickness.Application.UnitTests.Tests.MedicineLogs.Commands.UpdateMedicineLog;

public class UpdateMedicineLogCommandHandlerTest : CommandTestBase
{
    private readonly UpdateMedicineLogCommandHandler _handler;
    private readonly UpdateMedicineLogCommandValidator _validator;
    private readonly UpdateMedicineLogCommandValidator _validatorUnauthorized;
    private readonly UpdateMedicineLogCommandValidator _validatorForbidden;

    public UpdateMedicineLogCommandHandlerTest()
    {
        _handler = new(_context, _mapper);
        _validator = new(_time, _context, _resourceOwnershipValidUser);
        _validatorUnauthorized = new(_time, _context, _resourceOwnershipUnauthorizedUser);
        _validatorForbidden = new(_time, _context, _resourceOwnershipForbiddenUser);
    }

    [Fact]
    public async Task MinimumDataRequestGiven_UpdateMedicineLog_ValidUser_ShouldBeValid()
    {
        var id = 4;
        var newData = new DateTime(2023, 6, 6, 6, 6, 6);
        var command = new UpdateMedicineLogCommand() { Id = id, LastlyTaken = newData };
        await ValidateRequestAsync(command, _validator);
        var updatedFromDb = await _context.MedicineLogs.SingleAsync(m => m.Id == id);
        updatedFromDb.LastlyTaken.Equals(newData).Should().Be(true);
    }


    [Fact]
    public async Task TooEarlyDateRequestGiven_UpdateMedicineLog_ValidUser_ShouldBeInvalid()
    {
        try
        {
            var id = 4;
            var newData = new DateTime(2022, 6, 6, 6, 6, 6);
            var command = new UpdateMedicineLogCommand() { Id = id, LastlyTaken = newData };
            await ValidateRequestAsync(command, _validator);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<ValidationException>();
        }
    }

    [Fact]
    public async Task FutureDateRequestGiven_UpdateMedicineLog_ValidUser_ShouldBeInvalid()
    {
        try
        {
            var id = 4;
            var newData = new DateTime(2024, 6, 6, 6, 6, 6);
            var command = new UpdateMedicineLogCommand() { Id = id, LastlyTaken = newData };
            await ValidateRequestAsync(command, _validator);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<ValidationException>();
        }
    }

    [Fact]
    public async Task MinimumDataRequestGiven_UpdateMedicine_ShouldBeUnauthorized()
    {
        try
        {
            var id = 4;
            var newData = new DateTime(2023, 6, 6, 6, 6, 6);
            var command = new UpdateMedicineLogCommand() { Id = id, LastlyTaken = newData };
            await ValidateRequestAsync(command, _validatorUnauthorized);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<UnauthorizedAccessException>();
        }
    }

    [Fact]
    public async Task MinimumDataRequestGiven_UpdateMedicine_ShouldBeForbidden()
    {
        var id = 4;
        try
        {
            var newData = new DateTime(2023, 6, 6, 6, 6, 6);
            var command = new UpdateMedicineLogCommand() { Id = id, LastlyTaken = newData };
            await ValidateRequestAsync(command, _validatorForbidden);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<ForbiddenAccessException>();
        }
    }


    private async Task ValidateRequestAsync(UpdateMedicineLogCommand command, UpdateMedicineLogCommandValidator validator)
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