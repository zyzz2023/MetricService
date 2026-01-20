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

    public static Error InvalidDateTime =>
        Error.Validation("INVALID_DATE_TIME", "The date cannot be later than the current date or earlier than 01.01.2000.");

    public static Error InvalidExecutionTime => 
        Error.Validation("IVALID_EXECUTION_TIME", "Execution time must be greater than 0");

    public static Error InvalidValue =>
        Error.Validation("INVALID_VALUE", "Value must be greater than 0");
}
