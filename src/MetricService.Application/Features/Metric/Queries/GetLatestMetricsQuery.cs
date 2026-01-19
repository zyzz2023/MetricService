using ErrorOr;
using MediatR;
using MetricService.Application.Features.Metric.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Application.Features.Metric.Queries;

public record GetLatestMetricsQuery(string FileName) : IRequest<ErrorOr<ICollection<MetricValueDto>>>;
