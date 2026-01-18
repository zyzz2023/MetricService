using CsvHelper;
using CsvHelper.Configuration;
using MetricService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Application.Common.Tools.Csv
{
    public sealed class MetricValueCsvParser
    {
        public ICollection<MetricValue> Parse(
            Stream csvStream,
            string fileName)
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

            return rows
                .Select(r => MetricValue.Create(
                    fileName,
                    r.Date,
                    r.ExecutionTime,
                    r.Value))
                .ToList();
        }
    }
}
