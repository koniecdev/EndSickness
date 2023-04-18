using EndSickness.Application.Common.Exceptions;
using EndSickness.Application.MedicineLogs.Commands.CreateMedicineLog;
using EndSickness.Shared.MedicineLogs.Commands.CreateMedicineLog;
using FluentValidation;

namespace EndSickness.Application.UnitTests.Tests.MedicineLogs.Commands.CreateMedicineLog;

public class CreateMedicineLogCommandHandlerTest : CommandTestBase
{
    private readonly CreateMedicineLogCommandHandler _handler;
    private readonly CreateMedicineLogCommandValidator _validator;
    private readonly CreateMedicineLogCommandValidator _validatorUnauthorized;
    private readonly CreateMedicineLogCommandValidator _validatorForbidden;

    public CreateMedicineLogCommandHandlerTest()
    {
        _handler = new(_context, _mapper);
        _validator = new(_time, _context, _resourceOwnershipValidUser);
        _validatorUnauthorized = new(_time, _context, _resourceOwnershipUnauthorizedUser);
        _validatorForbidden = new(_time, _context, _resourceOwnershipForbiddenUser);
    }

    [Fact]
    public async Task MinimumDataRequestGiven_CreateMedicineLog_ValidUser_ShouldBeValid()
    {
        var command = new CreateMedicineLogCommand(1, _time.Now);
        var response = await ValidateAndHandleRequest(command, _validator);
        response.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task NotExistingDependencyRequestGiven_CreateMedicineLog_ValidUser_ShouldBeInValid()
    {
        try
        {
            var command = new CreateMedicineLogCommand(1111, _time.Now);
            var response = await ValidateAndHandleRequest(command, _validator);
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
            var response = await ValidateAndHandleRequest(command, _validator);
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
            var response = await ValidateAndHandleRequest(command, _validatorUnauthorized);
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
            var response = await ValidateAndHandleRequest(command, _validatorForbidden);
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
            var command = new CreateMedicineLogCommand(5, new DateTime(2023, 1, 2, 13, 0, 0));
            var response = await ValidateAndHandleRequest(command, _validator);
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
            var response = await ValidateAndHandleRequest(command, _validator);
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
            var response = await ValidateAndHandleRequest(command, _validator);
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
        var response = await ValidateAndHandleRequest(command, _validator);
        response.Should().BeGreaterThan(0);
    }

    private async Task<int> ValidateAndHandleRequest(CreateMedicineLogCommand command, CreateMedicineLogCommandValidator validator)
    {
        var validationResult = await validator.ValidateAsync(command);
        if (validationResult.IsValid)
        {
            var response = await _handler.Handle(command, CancellationToken.None);
            return response;
        }
        else
        {
            throw new ValidationException(validationResult.Errors);
        }
    }
}