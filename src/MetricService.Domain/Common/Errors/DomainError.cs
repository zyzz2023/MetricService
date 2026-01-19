using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
namespace MetricService.Domain.Common.Errors;

public static class DomainError
{
    public static Error EmptyMetricValues => 
        Error.Validation("EMPTY_METRIC_VALUES", "Metric values cannot be empty");

}
