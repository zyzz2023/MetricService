using ErrorOr;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest> validator)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        if (_validator is null)
            return await next(cancellationToken);

        var result = await _validator.ValidateAsync(request, cancellationToken);

        if (result.IsValid)
            return await next(cancellationToken);

        var errors = result.Errors
            .Select(error => Error.Validation(error.PropertyName, error.ErrorMessage))
            .ToList();

        return (dynamic)errors;
    }
}
