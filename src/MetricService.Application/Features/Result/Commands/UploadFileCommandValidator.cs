using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core.CustomTypeProviders;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Application.Features.Result.Commands;

public class UploadFileCommandValidator : AbstractValidator<UploadFileCommand>
{
    public UploadFileCommandValidator()
    {
        RuleFor(c => c.file)
            .NotEmpty()
            .WithMessage("File cannot empty.")
            .Must(c => string.Equals(Path.GetExtension(c.FileName).ToLower(), ".csv", StringComparison.OrdinalIgnoreCase))
            .WithMessage("File must be a CSV file");
    }

}
