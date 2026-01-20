using CsvHelper;
using CsvHelper.Configuration;
using ErrorOr;
using MediatR;
using MetricService.Contracts.Requests;
using MetricService.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Application.Common.Tools.Csv;

public sealed class MetricValueCsvParser
{
    private const int minRows = 1;
    private const int maxRows = 10000;

    public ErrorOr<ICollection<MetricValue>> Parse(IFormFile file)
    {
        try
        {
            string fileName = file.FileName;
            using var stream = new StreamReader(file.OpenReadStream());

            using var csv = new CsvReader(stream, new CsvConfiguration(
                CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                HasHeaderRecord = true
            });

            csv.Context.RegisterClassMap<MetricValueCsvMap>();

            var rows = csv.GetRecords<MetricValueCsvRow>();

            var result = rows
                .Select(r => MetricValue.Create(
                    fileName,
                    r.Date,
                    r.ExecutionTime,
                    r.Value))
                .ToList();

            if(result.Any(x => x.IsError))
                return result.FirstOrDefault(x => x.IsError).FirstError;

            if (result.Count > maxRows || result.Count < minRows)
                return Error.Validation("IVALID_ROWS_COUNT", "Rows count must be greater than 0 and less than 10000.");

            return result
                .Select(x => x.Value)
                .ToList();
        }
        catch(Exception ex)
        {
            return Error.Validation("INVALID_FILE_DATA", $"Invalid file data: {ex.Message}");
        }
    }
}
