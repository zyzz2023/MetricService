using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Application.Common.Tools.Csv;

public sealed class MetricValueCsvRow
{
    public DateTime Date { get; set; } = default!;
    public double ExecutionTime { get; set; }
    public double Value { get; set; }
}
