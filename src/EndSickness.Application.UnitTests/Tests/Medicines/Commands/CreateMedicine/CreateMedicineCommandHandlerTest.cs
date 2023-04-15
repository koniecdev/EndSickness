using EndSickness.Application.Medicines.Commands.CreateMedicine;
using EndSickness.Shared.Medicines.Commands.CreateMedicine;
using FluentValidation;

namespace EndSickness.Application.UnitTests.Tests.Medicines.Commands.CreateMedicine;

public class CreateMedicineCommandHandlerTest : CommandTestBase
{
    private readonly CreateMedicineCommandHandler _handler;
    private readonly CreateMedicineCommandHandler _unauthorizedUserHandler;
    private readonly CreateMedicineCommandHandler _freshUserHandler;
    private readonly CreateMedicineCommandValidator _validator;

    public CreateMedicineCommandHandlerTest()
    {
        _handler = new(_context, _mapper);
        _unauthorizedUserHandler = new(_context, _mapper);
        _freshUserHandler = new(_context, _mapper);
        _validator = new();
    }

    [Fact]
    public async Task MinimumDataRequestGiven_CreateMedicine_ValidUser_ShouldBeValid()
    {
        var command = new CreateMedicineCommand("Polopiryna", 3, 0, 0);
        var response = await ValidateRequestAsync(command, _handler);
        response.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task FullRequestGiven_CreateMedicine_ValidUser_ShouldBeValid()
    {
        var command = new CreateMedicineCommand("Polopiryna", 3, 4, 7);
        var response = await ValidateAndHandleRequest(command, _handler);
        response.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task FullRequestGiven_CreateMedicine_NotAuthorizedUser_ShouldBeValid()
    {
        var command = new CreateMedicineCommand("Polopiryna", 3, 4, 7);
        var response = await ValidateAndHandleRequest(command, _unauthorizedUserHandler);
        response.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task FullRequestGiven_CreateMedicine_NotValidUser_ShouldBeValid()
    {
        var command = new CreateMedicineCommand("Polopiryna", 3, 4, 7);
        var response = await ValidateAndHandleRequest(command, _freshUserHandler);
        response.Should().BeGreaterThan(0);
    }

    private async Task<int> ValidateRequestAsync(CreateMedicineCommand command, CreateMedicineCommandHandler handler)
    {
        var validationResult = _validator.Validate(command);
        if (validationResult.IsValid)
        {
            var response = await handler.Handle(command, CancellationToken.None);
            return response;
        }
        else
        {
            throw new ValidationException(validationResult.Errors);
        }
    }
    private async Task<int> ValidateAndHandleRequest(CreateMedicineCommand command, CreateMedicineCommandHandler handler)
    {
        var validationResult = _validator.Validate(command);
        if (validationResult.IsValid)
        {
            var response = await handler.Handle(command, CancellationToken.None);
            return response;
        }
        else
        {
            throw new ValidationException(validationResult.Errors);
        }
    }
}