using Microsoft.AspNetCore.Mvc;
using MetricService.Contracts.Requests;
using MediatR;
using ErrorOr;
using MetricService.Application.Features.Result.Commands;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Differencing;
using MetricService.WebAPI.Controllers.Common;
using MapsterMapper;
using MetricService.Application.Features.Result.Common;
using MetricService.Application.Features.Result.Queries;
using MetricService.Application.Features.Metric.Queries;

namespace MetricService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetricsController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<MetricsController> _logger;

        public MetricsController(
            ILogger<MetricsController> logger,
            ISender mediator,
            IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UploadFileRequest request, CancellationToken ct)
        {
            var command = new UploadFileCommand(request.filePath);

            var result = await _mediator.Send(command, ct);

            return result.Match(
                result => Created(HttpContext.Request.Path, result),
                errors => Problem(errors));
        }

        [HttpGet("results")]
        public async Task<IActionResult> GetResults([FromQuery] GetFilteredResultsRequest request, CancellationToken ct)
        {
            var query = _mapper.Map<GetResultsQuery>(request);

            var result = await _mediator.Send(query, ct);

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        [HttpGet("metrics")]
        public async Task<IActionResult> GetLatestMetrics([FromQuery] GetLatestValuesRequest request, CancellationToken ct)
        {
            var query = _mapper.Map<GetLatestMetricsQuery>(request);

            var result = await _mediator.Send(query, ct);

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }
    }
}
