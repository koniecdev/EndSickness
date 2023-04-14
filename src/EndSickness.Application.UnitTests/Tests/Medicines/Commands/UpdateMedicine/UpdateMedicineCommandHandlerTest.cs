using EndSickness.Application.Common.Exceptions;
using EndSickness.Application.Medicines.Commands.UpdateMedicine;
using EndSickness.Shared.Medicines.Commands.UpdateMedicine;
using FluentValidation;

namespace EndSickness.Application.UnitTests.Tests.Medicines.Commands.UpdateMedicine;

public class UpdateMedicineCommandHandlerTest : CommandTestBase
{
    private readonly UpdateMedicineCommandHandler _handler;
    private readonly UpdateMedicineCommandHandler _unauthorizedUserHandler;
    private readonly UpdateMedicineCommandHandler _forbiddenUserHandler;
    private readonly UpdateMedicineCommandValidator _validator;

    public UpdateMedicineCommandHandlerTest()
    {
        _handler = new(_context, _mapper, _resourceOwnershipValidUser);
        _unauthorizedUserHandler = new(_context, _mapper, _resourceOwnershipUnauthorizedUser);
        _forbiddenUserHandler = new(_context, _mapper, _resourceOwnershipInvalidUser);
        _validator = new();
    }

    [Fact]
    public async Task MinimumDataRequestGiven_UpdateMedicine_ShouldBeValid()
    {
        var id = 1;
        var newName = "Nurofen forte";
        var command = new UpdateMedicineCommand(id) { Name = newName };
        await ValidateRequestAsync(command, _handler);
        var updatedFromDb = await _context.Medicines.SingleAsync(m => m.Id == id);
        (updatedFromDb.Name.Equals(newName) && updatedFromDb.MaxDailyAmount == 3).Should().Be(true);
    }

    [Fact]
    public async Task NewDateRequestGiven_UpdateMedicine_ShouldBeValid()
    {
        var id = 1;
        var newCd = TimeSpan.FromHours(6);
        var command = new UpdateMedicineCommand(id) { Cooldown = newCd };
        await ValidateRequestAsync(command, _handler);
        var updatedFromDb = await _context.Medicines.SingleAsync(m => m.Id == id);
        (updatedFromDb.Cooldown.Equals(newCd) && updatedFromDb.MaxDaysOfTreatment == 7).Should().Be(true);
    }

    [Fact]
    public async Task NullDateRequestGiven_UpdateMedicine_ShouldBeValid()
    {
        var id = 1;
        var command = new UpdateMedicineCommand(id) { Cooldown = TimeSpan.Zero };
        await ValidateRequestAsync(command, _handler);
        var updatedFromDb = await _context.Medicines.SingleAsync(m => m.Id == id);
        (updatedFromDb.Cooldown == TimeSpan.Zero && updatedFromDb.MaxDaysOfTreatment == 7).Should().Be(true);
    }

    [Fact]
    public async Task DataWithNullRequestGiven_UpdateMedicine_ShouldBeValid()
    {
        var id = 1;
        var newName = "Nurofen forte";
        var command = new UpdateMedicineCommand(id) { Name = newName, MaxDailyAmount = null };
        await ValidateRequestAsync(command, _handler);
        var updatedFromDb = await _context.Medicines.SingleAsync(m => m.Id == id);
        (updatedFromDb.Name.Equals(newName) && updatedFromDb.MaxDailyAmount == null && updatedFromDb.MaxDaysOfTreatment == 7).Should().Be(true);
    }

    [Fact]
    public async Task MinimumDataRequestGiven_UpdateMedicine_ShouldBeUnauthorized()
    {
        try
        {
            var id = 1;
            var newName = "Nurofen forte";
            var command = new UpdateMedicineCommand(id) { Name = newName };
            await ValidateRequestAsync(command, _unauthorizedUserHandler);
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
            var command = new UpdateMedicineCommand(id) { Name = newName };
            await ValidateRequestAsync(command, _forbiddenUserHandler);
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