using System.Diagnostics;

namespace EndSickness.Application.Common.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;
    private readonly Stopwatch _stopwatch;
    public PerformanceBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
        _stopwatch = new();
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _stopwatch.Start();
        var response = await next();
        _stopwatch.Stop();

        RequestTimeHandler(request);

        return response;
    }

    private void RequestTimeHandler(TRequest request)
    {
        var elapsed = _stopwatch.ElapsedMilliseconds;
        if(elapsed > 500)
        {
            _logger.LogWarning("EndSickness long running request {requestName} : {elapsedMs}ms {request}", typeof(TRequest).Name, elapsed, request);
        }
    }
}
