using FluentValidation;
using FluentValidation.Results;
using MediatR;
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
        RuleFor(c => c.FilePath)
            .Must(str => !string.IsNullOrWhiteSpace(str))
            .WithMessage("File name cannot be null or empty.")
            .Must(BeValidFilePath)
            .WithMessage("Invalid file path")
            .Must(BeCsvFile)
            .WithMessage("File must be a CSV file");
    }

    private bool BeValidFilePath(string filePath)
    {
        try
        {
            var fullPath = Path.GetFullPath(filePath);
            return !string.IsNullOrWhiteSpace(fullPath);
        }
        catch
        {
            return false;
        }
    }

    private bool BeCsvFile(string filePath)
    {
        var extension = Path.GetExtension(filePath);
        return string.Equals(extension, ".csv", StringComparison.OrdinalIgnoreCase);
    }
}
