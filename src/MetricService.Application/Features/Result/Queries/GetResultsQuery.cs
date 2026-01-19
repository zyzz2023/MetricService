using ErrorOr;
using MediatR;
using MetricService.Application.Features.Result.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Application.Features.Result.Queries;

public class GetResultsQuery : IRequest<ErrorOr<ICollection<FileResultDto>>>
{
    public string? FileName { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public double? FromAverageValue { get; set; }
    public double? ToAverageValue { get; set; }
    public double? FromAverageExecutionTime { get; set; }
    public double? ToAverageExecutionTime { get; set; }
    public int? Take { get; set; }
}
