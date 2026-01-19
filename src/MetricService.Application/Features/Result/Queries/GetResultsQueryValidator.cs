using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MetricService.Application.Features.Result.Queries
{
    public class GetResultsQueryValidator : AbstractValidator<GetResultsQuery>
    {
        public GetResultsQueryValidator()
        {
            RuleFor(c => c.FileName)
                .MaximumLength(255)
                .WithMessage("File name cannot exceed 255 characters.");

            RuleFor(x => x)
                .Must(HaveValidDateRange)
                .WithMessage("'fromDate' cannot be later than 'toDate'.")
                .Must(HaveValidDateLimits)
                .WithMessage("Dates cannot be in the future and must be after 2000-01-01.");

            RuleFor(x => x)
                .Must(HaveValidAverageValueRange)
                .WithMessage("'fromAverageValue' cannot be greater than 'toAverageValue'.")
                .Must(HaveValidAverageExecutionTimeRange)
                .WithMessage("'fromAverageExecutionTime' cannot be greater than 'toAverageExecutionTime'.");

            RuleFor(x => x.FromAverageValue)
                .GreaterThan(0)
                .When(x => x.FromAverageValue.HasValue)
                .WithMessage("'fromAverageValue' cannot be negative.");

            RuleFor(x => x.ToAverageValue)
                .GreaterThan(0)
                .When(x => x.ToAverageValue.HasValue)
                .WithMessage("'toAverageValue' cannot be negative");

            RuleFor(x => x.FromAverageValue)
                .GreaterThan(0)
                .When(x => x.FromAverageValue.HasValue)
                .WithMessage("'fromAverageValue' cannot be negative.");

            RuleFor(x => x.FromAverageExecutionTime)
                .GreaterThan(0)
                .When(x => x.FromAverageExecutionTime.HasValue)
                .WithMessage("'fromAverageExecutionTime' cannot be negative");

            RuleFor(x => x.ToAverageExecutionTime)
                .GreaterThan(0)
                .When(x => x.ToAverageExecutionTime.HasValue)
                .WithMessage("'fromAverageExecutionTime' cannot be negative");
        }

        private bool HaveValidDateRange(GetResultsQuery query)
        {
            if (!query.FromDate.HasValue || !query.ToDate.HasValue) // Только "от" или "до"
                return true; 

            return query.FromDate <= query.ToDate;
        }

        private bool HaveValidDateLimits(GetResultsQuery query)
        {
            var now = DateTime.UtcNow;
            var minDate = new DateTime(2000, 1, 1);

            if (query.FromDate.HasValue)
            {
                if (query.FromDate > now) return false;
                if (query.FromDate < minDate) return false;
            }

            if (query.ToDate.HasValue)
            {
                if (query.ToDate > now) return false;
                if (query.ToDate < minDate) return false;
            }

            return true;
        }

        private bool HaveValidAverageValueRange(GetResultsQuery query)
        {
            if (!query.FromAverageValue.HasValue || !query.ToAverageValue.HasValue)
                return true;

            return query.FromAverageValue <= query.ToAverageValue;
        }

        private bool HaveValidAverageExecutionTimeRange(GetResultsQuery query)
        {
            if (!query.FromAverageExecutionTime.HasValue || !query.ToAverageExecutionTime.HasValue)
                return true;

            return query.FromAverageExecutionTime <= query.ToAverageExecutionTime;
        }
    }
}
