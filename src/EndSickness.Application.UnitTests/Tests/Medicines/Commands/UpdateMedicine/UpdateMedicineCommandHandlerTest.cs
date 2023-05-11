using EndSickness.Application.Common.Exceptions;
using EndSickness.Application.Medicines.Commands.UpdateMedicine;
using EndSickness.Shared.Medicines.Commands.UpdateMedicine;
using FluentValidation;

namespace EndSickness.Application.UnitTests.Tests.Medicines.Commands.UpdateMedicine;

public class UpdateMedicineCommandHandlerTest : CommandTestBase
{
    private readonly UpdateMedicineCommandValidator _validator;
    private readonly UpdateMedicineCommandHandler _handler;
    private readonly UpdateMedicineCommandHandler _handlerUnauthorized;
    private readonly UpdateMedicineCommandHandler _handlerForbidden;

    public UpdateMedicineCommandHandlerTest()
    {
        _validator = new();
        _handler = new(_context, _mapper, _resourceOwnershipValidUser);
        _handlerUnauthorized = new(_context, _mapper, _resourceOwnershipUnauthorizedUser);
        _handlerForbidden = new(_context, _mapper, _resourceOwnershipForbiddenUser);
    }

    [Fact]
    public async Task MinimumDataRequestGiven_UpdateMedicine_ShouldBeValid()
    {
        var id = 1;
        var newName = "Nurofen forte";
        var command = new UpdateMedicineCommand() { Id = id, Name = newName };
        await ValidateRequestAsync(command, _handler);
        var updatedFromDb = await _context.Medicines.SingleAsync(m => m.Id == id);
        (updatedFromDb.Name.Equals(newName) && updatedFromDb.MaxDailyAmount == 3).Should().Be(true);
    }

    [Fact]
    public async Task NewDateRequestGiven_UpdateMedicine_ShouldBeValid()
    {
        var id = 1;
        var newCd = 6;
        var command = new UpdateMedicineCommand() { Id = id, HourlyCooldown = newCd };
        await ValidateRequestAsync(command, _handler);
        var updatedFromDb = await _context.Medicines.SingleAsync(m => m.Id == id);
        (updatedFromDb.HourlyCooldown == newCd && updatedFromDb.MaxDaysOfTreatment == 7).Should().Be(true);
    }

    [Fact]
    public async Task NullDateRequestGiven_UpdateMedicine_ShouldBeValid()
    {
        var id = 1;
        var command = new UpdateMedicineCommand() { Id = id, HourlyCooldown = 0 };
        await ValidateRequestAsync(command, _handler);
        var updatedFromDb = await _context.Medicines.SingleAsync(m => m.Id == id);
        (updatedFromDb.HourlyCooldown == 0 && updatedFromDb.MaxDaysOfTreatment == 7).Should().Be(true);
    }

    [Fact]
    public async Task ResetDataRequestGiven_UpdateMedicine_ShouldBeValid()
    {
        var id = 1;
        var newName = "Nurofen forte";
        var command = new UpdateMedicineCommand() { Id = id, Name = newName, MaxDailyAmount = 0 };
        await ValidateRequestAsync(command, _handler);
        var updatedFromDb = await _context.Medicines.SingleAsync(m => m.Id == id);
        (updatedFromDb.Name.Equals(newName) && updatedFromDb.MaxDailyAmount == 0 && updatedFromDb.MaxDaysOfTreatment == 7).Should().Be(true);
    }

    [Fact]
    public async Task MinimumDataRequestGiven_UpdateMedicine_ShouldBeUnauthorized()
    {
        try
        {
            var id = 1;
            var newName = "Nurofen forte";
            var command = new UpdateMedicineCommand() { Id = id, Name = newName };
            await ValidateRequestAsync(command, _handlerUnauthorized);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch(Exception ex)
        {
            ex.Should().BeOfType<UnauthorizedAccessException>();
        }
    }

    [Fact]
    public async Task MinimumDataRequestGiven_UpdateMedicine_ShouldBeForbidden()
    {
        try
        {
            var id = 1;
            var newName = "Nurofen forte";
            var command = new UpdateMedicineCommand() { Id = id, Name = newName };
            await ValidateRequestAsync(command, _handlerForbidden);
            var updatedFromDb = await _context.Medicines.SingleAsync(m => m.Id == id);
            updatedFromDb.Name.Should().Be(newName);
        }
        catch(Exception ex)
        {
            ex.Should().BeOfType<ForbiddenAccessException>();
        }
    }

    private async Task ValidateRequestAsync(UpdateMedicineCommand command, UpdateMedicineCommandHandler handler)
    {
        var validationResult = await _validator.ValidateAsync(command);
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