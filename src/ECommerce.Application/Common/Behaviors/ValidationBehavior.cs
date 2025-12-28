using ECommerce.SharedKernel.Domain;
using FluentValidation;
using MediatR;

namespace ECommerce.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Any())
        {
            var validationErrors = failures
                .Select(f => new ValidationError(f.PropertyName, f.ErrorMessage))
                .ToList();

            return CreateValidationResult(validationErrors);
        }

        return await next();
    }

    private static TResponse CreateValidationResult(List<ValidationError> errors)
    {
        if (typeof(TResponse) == typeof(Result))
        {
            return (Result.ValidationFailure(errors) as TResponse)!;
        }

        var resultType = typeof(TResponse).GenericTypeArguments[0];
        var failureMethod = typeof(Result<>)
            .MakeGenericType(resultType)
            .GetMethod(nameof(Result<object>.ValidationFailure));

        return (failureMethod!.Invoke(null, new object[] { errors }) as TResponse)!;
    }
}