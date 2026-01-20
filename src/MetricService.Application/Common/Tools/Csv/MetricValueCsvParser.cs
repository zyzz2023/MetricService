using CsvHelper;
using CsvHelper.Configuration;
using ErrorOr;
using MetricService.Contracts.Requests;
using MetricService.Domain.Entities;
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
    private const int maxRows = 100;

    public ErrorOr<ICollection<MetricValue>> Parse(
        Stream csvStream, 
        string fileName)
    {
        try
        {
            using var reader = new StreamReader(csvStream);

            using var csv = new CsvReader(reader, new CsvConfiguration(
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

            if (result.Count > 10000 || result.Count < 1)
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
