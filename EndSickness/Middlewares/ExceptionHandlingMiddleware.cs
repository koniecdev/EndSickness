using EndSickness.Exceptions.Interfaces;
using EndSickness.ExceptionsHandling;
using EndSickness.ExceptionsHandling.ExceptionHandlingStrategy;
using Newtonsoft.Json;
using System.Reflection;

namespace EndSickness.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private IExceptionResponse _errorResponse;
    private readonly IExceptionHandlingStrategy _defaultStrategy;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IExceptionResponse errorResponse, IExceptionHandlingStrategy defaultStrategy)
    {
        _next = next;
        _logger = logger;
        _errorResponse = errorResponse;
        _defaultStrategy = defaultStrategy;
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
        ExceptionHandler? exceptionHandler;
        var exceptionStrategiesList = Assembly.GetExecutingAssembly().GetExportedTypes().Where(m => typeof(IExceptionHandlingStrategy).IsAssignableFrom(m) && !m.IsInterface && !m.IsAbstract).ToList();
        if (exceptionStrategiesList.Any(m => m.Name.StartsWith(exception.GetType().Name)))
        {
            var exceptionName = exception.GetType().Name;
            var thrownExceptionStrategy = exceptionStrategiesList.First(m => m.Name.Contains(exceptionName)) ?? typeof(DefaultExceptionHandlingStrategy);
            exceptionHandler =
                new(Activator.CreateInstance(thrownExceptionStrategy) as IExceptionHandlingStrategy ?? _defaultStrategy,
                exception);
        }
        else
        {
            exceptionHandler = new(new DefaultExceptionHandlingStrategy(_errorResponse), exception);
        }

        _errorResponse = exceptionHandler.Handle();

        _logger.LogError(exception, "There was an error in {Layer}: {ErrorMessage}", exception.Source, _errorResponse.Message);

        JsonSerializerSettings defaultSettings = new()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        var result = JsonConvert.SerializeObject(_errorResponse, defaultSettings);
        await response.WriteAsync(result);
    }
}
