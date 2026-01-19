using MediatR;
using ErrorOr;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Application.Features.Result.Commands;

public record UploadFileCommand(string FilePath) : IRequest<ErrorOr<UploadFileCommandResult>>;

