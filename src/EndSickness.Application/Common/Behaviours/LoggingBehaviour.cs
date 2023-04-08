namespace EndSickness.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;
    public LoggingBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }
    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        await Task.Run(() => _logger.LogInformation("EndSickness Request: {name} {request}", typeof(TRequest).Name, request), cancellationToken);
    }
}
