using EndSickness.Application.Common.Exceptions;
using EndSickness.Application.MedicineLogs.Commands.CreateMedicineLog;
using EndSickness.Shared.MedicineLogs.Commands.CreateMedicineLog;
using FluentValidation;

namespace EndSickness.Application.UnitTests.Tests.MedicineLogs.Commands.CreateMedicineLog;

public class CreateMedicineLogCommandHandlerTest : CommandTestBase
{
    private readonly CreateMedicineLogCommandHandler _handler;
    private readonly CreateMedicineLogCommandHandler _unauthorizedUserHandler;
    private readonly CreateMedicineLogCommandHandler _freshUserHandler;
    private readonly CreateMedicineLogCommandValidator _validator;

    public CreateMedicineLogCommandHandlerTest()
    {
        _handler = new(_context, _mapper, _resourceOwnershipValidUser);
        _unauthorizedUserHandler = new(_context, _mapper, _resourceOwnershipUnauthorizedUser);
        _freshUserHandler = new(_context, _mapper, _resourceOwnershipInvalidUser);
        _validator = new(_time);
    }

    [Fact]
    public async Task MinimumDataRequestGiven_CreateMedicineLog_ValidUser_ShouldBeValid()
    {
        var command = new CreateMedicineLogCommand(1, _time.Now);
        var response = await ValidateAndHandleRequest(command, _handler);
        response.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task NotExistingDependencyRequestGiven_CreateMedicineLog_ValidUser_ShouldBeInValid()
    {
        try
        {
            var command = new CreateMedicineLogCommand(1111, _time.Now);
            var response = await ValidateAndHandleRequest(command, _handler);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch(Exception ex)
        {
            ex.Should().BeOfType<ResourceNotFoundException>();
        }
    }

    [Fact]
    public async Task TooOldDateRequestGiven_CreateMedicineLog_ValidUser_ShouldBeInvalid()
    {
        try
        {
            var command = new CreateMedicineLogCommand(1, _time.Now - TimeSpan.FromDays(1000));
            var response = await ValidateAndHandleRequest(command, _handler);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<ValidationException>();
        }
    }

    [Fact]
    public async Task FullRequestGiven_CreateMedicineLog_NotAuthorizedUser_ShouldBeUnauthorized()
    {
        try
        {
            var command = new CreateMedicineLogCommand(1, _time.Now);
            var response = await ValidateAndHandleRequest(command, _unauthorizedUserHandler);
        }
        catch(Exception ex)
        {
            ex.Should().BeOfType<UnauthorizedAccessException>();
        }
    }

    [Fact]
    public async Task MedicineNotOfThisUser_CreateMedicineLog_ValidUser_ShouldBeInvalid()
    {
        try
        {
            var command = new CreateMedicineLogCommand(1, _time.Now);
            var response = await ValidateAndHandleRequest(command, _freshUserHandler);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch(Exception ex)
        {
            ex.Should().BeOfType<ForbiddenAccessException>();
        }
    }

    private async Task<int> ValidateAndHandleRequest(CreateMedicineLogCommand command, CreateMedicineLogCommandHandler handler)
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