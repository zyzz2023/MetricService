using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using MetricService.Application.Common.Tools.Csv;
using MetricService.Domain.Entities;
using MetricService.Domain.Interfaces;
using MetricService.Domain.Interfaces.Common;

namespace MetricService.Application.Features.Result.Commands;

public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, ErrorOr<UploadFileCommandResult>>
{
    private readonly IFileResultRepository _repository;
    private readonly MetricValueCsvParser _csvParser;

    public UploadFileCommandHandler(IFileResultRepository repository)
    {
        _repository = repository;
        _csvParser = new MetricValueCsvParser();
    }
    public async Task<ErrorOr<UploadFileCommandResult>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        string fileName = Path.GetFileName(request.FilePath);

        using var stream = File.OpenRead(request.FilePath);

        var parseResult = _csvParser.Parse(stream, fileName);
        if (parseResult.IsError)
            return parseResult.FirstError;

        var fileResult = FileResult.Create(fileName, parseResult.Value);
        var calculateResult = fileResult.CalculateFileResult();

        if(calculateResult.IsError)
            return calculateResult.FirstError;

        await _repository.AddWithOverwriteAsync(fileName, fileResult, cancellationToken);

        return new UploadFileCommandResult(fileName);
    }
}
