using EndSickness.Application.Common.Exceptions;
using EndSickness.Infrastructure.ExceptionsHandling.ExceptionHandlingStrategy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Reflection;

namespace EndSickness.Infrastructure.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IErrorResponse _errorResponse;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IErrorResponse errorResponse)
    {
        _next = next;
        _logger = logger;
        _errorResponse = errorResponse;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    public async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var response = context.Response; 

        ExceptionHandler? errorHandler = exception switch
        {
            //ValidationException => new(new ValidationExceptionHandlerStrategy(), exception),
            ArgumentNullException => new(new ArgumentNullExceptionHandlerStrategy(), exception),
            UserNotResolvedException => new(new UserNotResolvedExceptionHandlerStrategy(), exception),
            InvalidOperationException => new(new InvalidOperationExceptionHandlerStrategy(), exception),
            UnauthorizedAccessException => new(new UnauthorizedAccessExceptionHandlerStrategy(), exception),
            ForbiddenAccessException => new(new ForbiddenAccessExceptionHandlerStrategy(), exception),
            EmptyResultException => new(new EmptyResultExceptionHandlerStrategy(), exception),
            _ => new(new DefaultExceptionHandlerStrategy(), exception)
        };

        var exceptionStrategies = Assembly.GetExecutingAssembly().GetExportedTypes().Where(m => typeof(IExceptionHandlerStrategy).IsAssignableFrom(m) && !m.IsInterface && !m.IsAbstract).ToList();
        if (exceptionStrategies.Any(m => m.GetType().Name.StartsWith(exception.GetType().Name)))
        {
            errorHandler = Activator.CreateInstance(exceptionStrategies.First(m => m.GetType().Name.StartsWith(exception.GetType().Name))) as ExceptionHandler;
            if(errorHandler is null)
            {
                throw new Exception("shit");
            }
        }
        else
        {
            errorHandler = new(new DefaultExceptionHandlerStrategy(), exception);
        }

        //foreach(var exceptionHandlerStrategy in Assembly.GetExecutingAssembly().GetExportedTypes().Where(m => m.Name.EndsWith("ExceptionHandlerStrategy")))
        

        (response.StatusCode, _errorResponse.Message) = errorHandler.Handle();

        _logger.LogError(exception, "There was an error in {Layer}: {ErrorMessage}", exception.Source, _errorResponse.Message);
        
        JsonSerializerSettings defaultSettings = new()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        var result = JsonConvert.SerializeObject(_errorResponse, defaultSettings);
        await context.Response.WriteAsync(result);
    }
}
