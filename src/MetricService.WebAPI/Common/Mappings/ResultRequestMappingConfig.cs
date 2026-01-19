using Mapster;
using MetricService.Application.Features.Metric.Queries;
using MetricService.Application.Features.Result.Queries;
using MetricService.Contracts.Requests;

public class ResultRequestsMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GetFilteredResultsRequest, GetResultsQuery>();

        config.NewConfig<GetLatestValuesRequest, GetLatestMetricsQuery>();
    }
}