using FluentValidation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Application.Features.Result.Queries
{
    public class GetResultsQueryValidator : AbstractValidator<GetResultsQuery>
    {
        public GetResultsQueryValidator()
        {
            RuleFor(c => c.FileName)
                .MaximumLength(255)
                .WithMessage("File name cannot exceed 255 characters.");

            RuleFor(c => c.FromDate)
                .Must(BeValidDate)
                .WithMessage("'FromDate' must be between 2000.01.01 and today");

            RuleFor(c => c.ToDate)
                .Must(BeValidDate)
                .WithMessage("'ToDate' must be between 2000.01.01 and today");

            RuleFor(c => c.FromAverageValue)
                .GreaterThanOrEqualTo(0)
                .When(c => c.FromAverageValue.HasValue)
                .WithMessage("'FromAverageValue' cannot be negative.");

            RuleFor(c => c.ToAverageValue)
                .GreaterThanOrEqualTo(0)
                .When(c => c.ToAverageValue.HasValue)
                .WithMessage("'ToAverageValue' cannot be negative");

            RuleFor(c => c.FromAverageValue)
                .GreaterThanOrEqualTo(0)
                .When(c => c.FromAverageValue.HasValue)
                .WithMessage("'FromAverageValue' cannot be negative.");

            RuleFor(c => c.FromAverageExecutionTime)
                .GreaterThanOrEqualTo(0)
                .When(c => c.FromAverageExecutionTime.HasValue)
                .WithMessage("'FromAverageExecutionTime' cannot be negative");

            RuleFor(c => c.ToAverageExecutionTime)
                .GreaterThanOrEqualTo(0)
                .When(c => c.ToAverageExecutionTime.HasValue)
                .WithMessage("'FromAverageExecutionTime' cannot be negative");
        }

        private bool BeValidDate(DateTime? date)
        {
            if(!date.HasValue) 
                return true;

            return date >= new DateTime(2000, 01, 01) && date <= DateTime.UtcNow;
        }
    }
}
