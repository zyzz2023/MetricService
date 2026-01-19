using ErrorOr;
using MediatR;
using LinqKit;
using MapsterMapper;
using MetricService.Application.Features.Result.Common;
using MetricService.Domain.Interfaces;
using MetricService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Mapster;

namespace MetricService.Application.Features.Result.Queries;

public class GetResultsQueryHandler : IRequestHandler<GetResultsQuery, ErrorOr<ICollection<FileResultDto>>>
{
    private readonly IFileResultRepository _repository;
    private readonly IMapper _mapper;

    public GetResultsQueryHandler(IFileResultRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<ICollection<FileResultDto>>> Handle(GetResultsQuery request, CancellationToken cancellationToken)
    {
        var predicate = BuildPredicate(request);

        var results = await _repository.GetFilteredAsync(
            predicate: predicate, cancellationToken: cancellationToken);

        var resultsDto = results.Select(x => x.Adapt<FileResultDto>()).ToList();

        return resultsDto;
    }
    private Expression<Func<FileResult, bool>> BuildPredicate(GetResultsQuery request)
    {
        Expression<Func<FileResult, bool>> predicate = x => true;

        if (!string.IsNullOrWhiteSpace(request.FileName))
            predicate = predicate.And(x => x.FileName.Contains(request.FileName));

        if (request.FromDate.HasValue)
            predicate = predicate.And(x => x.StartDate >= request.FromDate.Value);

        if (request.ToDate.HasValue)
            predicate = predicate.And(x => x.EndDate <= request.ToDate.Value);

        if (request.FromAverageValue.HasValue)
            predicate = predicate.And(x => x.AverageValue >= request.FromAverageValue.Value);

        if (request.ToAverageValue.HasValue)
            predicate = predicate.And(x => x.AverageValue <= request.ToAverageValue.Value);

        if (request.FromAverageExecutionTime.HasValue)
            predicate = predicate.And(x => x.AverageExecutionTime >= request.FromAverageExecutionTime.Value);

        if (request.ToAverageExecutionTime.HasValue)
            predicate = predicate.And(x => x.AverageExecutionTime <= request.ToAverageExecutionTime.Value);

        return predicate;
    }
}
