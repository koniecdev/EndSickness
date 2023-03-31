using Microsoft.AspNetCore.Builder;

namespace EndSickness.Infrastructure.Middlewares;

public static class ErrorHandlerMiddlewareExtension
{
    public static IApplicationBuilder GlobalErrorHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
