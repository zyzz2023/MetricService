using ErrorOr;
using MediatR;
using MetricService.Application.Features.Result.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Application.Features.Result.Queries;

public record GetResultsQuery(
    string? FileName,
    DateTime? FromDate,
    DateTime? ToDate,
    double? FromAverageValue,
    double? ToAverageValue,
    double? FromAverageExecutionTime,
    double? ToAverageExecutionTime) : IRequest<ErrorOr<ICollection<FileResultDto>>>;