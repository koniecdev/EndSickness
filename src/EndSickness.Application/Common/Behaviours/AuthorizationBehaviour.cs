namespace EndSickness.Application.Common.Behaviours;

public class AuthorizationBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;
    private readonly ICurrentUserService _currentUserService;

    public AuthorizationBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }
    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        await Task.Run(() =>
        {
            if (!_currentUserService.IsAuthorized)
            {
                _logger.LogError("Unauthorized request has been registered");
                throw new UnauthorizedAccessException();
            }
        }, cancellationToken);
    }
}
