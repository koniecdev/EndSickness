using FluentValidation.Results;

namespace EndSickness.Application.Common.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            List<ValidationFailure>? failures = new();

            try 
            {
                failures = _validators.Select(m => m.Validate(context))
                .SelectMany(m => m.Errors).Where(m => m != null).ToList();
            }
            catch (Exception)
            {
                var failuresTasks = _validators.Select(async m => await m.ValidateAsync(context));
                failures = (await Task.WhenAll(failuresTasks))
                    .SelectMany(m => m.Errors).Where(m => m != null).ToList();
            }

            if(failures.Count > 0)
            {
                throw new ValidationException(failures);
            }
        }
        return await next();
    }
}
