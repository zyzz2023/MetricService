using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Application.Features.Result.Common;

public record FileResultDto()
{
    public string FileName { get; set; }
    public double DeltaTimeSeconds { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double AverageExecutionTime { get; set; }
    public double AverageValue { get; set; }
    public double MedianValue { get; set; }
    public double MaxValue { get; set; }
    public double MinValue { get; set; }
}
