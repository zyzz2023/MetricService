using Microsoft.AspNetCore.Mvc;
using MetricService.Contracts.Requests;
using MediatR;
using MetricService.Application.Features.UploadFile.Commands;
using System.Threading.Tasks;

namespace MetricService.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MetricsController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly ILogger<MetricsController> _logger;

        public MetricsController(ILogger<MetricsController> logger, ISender mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<string> Post(UploadFileRequest request)
        {
            var command = new UploadFileCommand(request.filePath);
            var result = await _mediator.Send(command);

            return result.fileName;
        }
    }
}
