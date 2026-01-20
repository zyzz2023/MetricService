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
    string? FileName = null,
    DateTime? FromDate = null,
    DateTime? ToDate = null,
    double? FromAverageValue = null ,
    double? ToAverageValue = null,
    double? FromAverageExecutionTime = null,
    double? ToAverageExecutionTime = null) : IRequest<ErrorOr<ICollection<FileResultDto>>>;