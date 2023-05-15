using EndSickness.Application.Common.Exceptions;
using EndSickness.Application.MedicineLogs.Commands.CreateMedicineLog;
using EndSickness.Shared.MedicineLogs.Commands.CreateMedicineLog;
using FluentValidation;

namespace EndSickness.Application.UnitTests.Tests.MedicineLogs.Commands.CreateMedicineLog;

public class CreateMedicineLogCommandHandlerTest : CommandTestBase
{
    private readonly CreateMedicineLogCommandValidator _validator;
    private readonly CreateMedicineLogCommandHandler _handler;
    private readonly CreateMedicineLogCommandHandler _handlerUnauthorized;
    private readonly CreateMedicineLogCommandHandler _handlerForbidden;

    public CreateMedicineLogCommandHandlerTest()
    {
        _validator = new(_time);
        _handler = new(_context, _mapper, _resourceOwnershipValidUser, _overdosingService);
        _handlerUnauthorized = new(_context, _mapper, _resourceOwnershipUnauthorizedUser, _overdosingService);
        _handlerForbidden = new(_context, _mapper, _resourceOwnershipForbiddenUser, _overdosingService);
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
            var response = await ValidateAndHandleRequest(command, _handlerUnauthorized);
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
            var response = await ValidateAndHandleRequest(command, _handlerForbidden);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch(Exception ex)
        {
            ex.Should().BeOfType<ForbiddenAccessException>();
        }
    }

    [Fact]
    public async Task SingleOverdoseRequest_CreateMedicineLog_ValidUser_ShouldBeInvalid()
    {
        try
        {
            var command = new CreateMedicineLogCommand(5, new DateTime(2023, 1, 2, 12, 30, 0));
            var response = await ValidateAndHandleRequest(command, _handler);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<OverdoseException>();
        }
    }

    [Fact]
    public async Task HourlyOverdoseRequest_CreateMedicineLog_ValidUser_ShouldBeInvalid()
    {
        try
        {
            var command = new CreateMedicineLogCommand(1, new DateTime(2023, 1, 1, 14, 0, 0));
            var response = await ValidateAndHandleRequest(command, _handler);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<OverdoseException>();
        }
    }

    [Fact]
    public async Task DailyOverdoseRequest_CreateMedicineLog_ValidUser_ShouldBeValid()
    {
        try
        {
            var command = new CreateMedicineLogCommand(1, new DateTime(2023, 1, 2, 7, 0, 0));
            var response = await ValidateAndHandleRequest(command, _handler);
            throw new Exception(SD.UnexpectedErrorInTestMethod);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<OverdoseException>();
        }
    }

    [Fact]
    public async Task ValidRequest_CreateMedicineLog_ValidUser_ShouldBeValid()
    {
        var command = new CreateMedicineLogCommand(1, new DateTime(2023, 1, 2, 15, 0, 0));
        var response = await ValidateAndHandleRequest(command, _handler);
        response.Should().BeGreaterThan(0);
    }

    private async Task<int> ValidateAndHandleRequest(CreateMedicineLogCommand command, CreateMedicineLogCommandHandler handler)
    {
        var validationResult = await _validator.ValidateAsync(command);
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