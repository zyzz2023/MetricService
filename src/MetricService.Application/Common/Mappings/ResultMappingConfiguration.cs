using Mapster;
using MetricService.Application.Features.Metric.Common;
using MetricService.Application.Features.Result.Common;
using MetricService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Application.Common.Mappings;

public class ResultMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<FileResultDto, FileResult>();

        config.NewConfig<MetricValueDto, MetricValue>();
    }
}
