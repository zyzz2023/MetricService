using ErrorOr;
using Mapster;
using MapsterMapper;
using MediatR;
using MetricService.Application.Features.Metric.Common;
using MetricService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Application.Features.Metric.Queries;

internal class GetLatestMetricsQueryHandler : IRequestHandler<GetLatestMetricsQuery, ErrorOr<ICollection<MetricValueDto>>>
{
    private readonly IMetricValueRepository _repository;
    private readonly IMapper _mapper;

    public GetLatestMetricsQueryHandler(IMetricValueRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ErrorOr<ICollection<MetricValueDto>>> Handle(GetLatestMetricsQuery request, CancellationToken cancellationToken)
    {
        var results = await _repository.GetLatestByFileNameAsync(request.FileName);

        var resultsDto = results.Select(x => x.Adapt<MetricValueDto>()).ToList();

        return resultsDto;
    }
}
