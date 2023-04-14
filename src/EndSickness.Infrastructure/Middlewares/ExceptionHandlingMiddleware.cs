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
            await ProcessExceptionAsync(httpContext.Response, ex);
        }
    }

    public async Task ProcessExceptionAsync(HttpResponse response, Exception exception)
    {
        response.ContentType = "application/json";
        ExceptionHandler? errorHandler;
        var exceptionStrategies = Assembly.GetExecutingAssembly().GetExportedTypes().Where(m => typeof(IExceptionHandlerStrategy).IsAssignableFrom(m) && !m.IsInterface && !m.IsAbstract).ToList();
        if (exceptionStrategies.Any(m => m.Name.StartsWith(exception.GetType().Name)))
        {
            var exceptionName = exception.GetType().Name;
            var selectedHandler = exceptionStrategies.First(m => m.Name.Contains(exceptionName)) ?? typeof(DefaultExceptionHandlerStrategy);
            errorHandler = 
                new(Activator.CreateInstance(selectedHandler) as IExceptionHandlerStrategy ?? new DefaultExceptionHandlerStrategy(),
                exception);
        }
        else
        {
            errorHandler = new(new DefaultExceptionHandlerStrategy(), exception);
        }

        (response.StatusCode, _errorResponse.Message) = errorHandler.Handle();

        _logger.LogError(exception, "There was an error in {Layer}: {ErrorMessage}", exception.Source, _errorResponse.Message);
        
        JsonSerializerSettings defaultSettings = new()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        var result = JsonConvert.SerializeObject(_errorResponse, defaultSettings);
        await response.WriteAsync(result);
    }
}
