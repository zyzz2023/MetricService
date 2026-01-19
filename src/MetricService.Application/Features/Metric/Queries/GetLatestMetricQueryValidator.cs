using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Application.Features.Metric.Queries;

public class GetLatestMetricQueryValidator : AbstractValidator<GetLatestMetricsQuery>
{
    public GetLatestMetricQueryValidator()
    {
        RuleFor(c => c.FileName)
            .Must(str => !string.IsNullOrWhiteSpace(str))
            .WithMessage("File name cannot be null or empty")
            .MaximumLength(255)
            .WithMessage("File name cannot exceed 255 characters.");
    }
}
