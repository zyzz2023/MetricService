using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MetricService.Application.Common.Tools.Csv;
using MetricService.Domain.Entities;
using MetricService.Domain.Interfaces;
using MetricService.Domain.Interfaces.Common;

namespace MetricService.Application.Features.UploadFile.Commands;

public class UploadFileHandler : IRequestHandler<UploadFileCommand, UploadFileResult>
{
    private readonly IFileResultRepository _repository;
    private readonly MetricValueCsvParser _csvParser;

    public UploadFileHandler(IFileResultRepository repository)
    {
        _repository = repository;
        _csvParser = new MetricValueCsvParser();
    }
    public async Task<UploadFileResult> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        try
        {
            using var stream = File.OpenRead(request.FilePath);

            string fileName = Path.GetFileName(request.FilePath);

            var metrics = _csvParser.Parse(
                stream,
                fileName);

            var fileResult = FileResult.Create(fileName, metrics);

            var result = new UploadFileResult(fileName);

            await _repository.AddWithOverwriteAsync(fileName, fileResult, cancellationToken);

            return result;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
