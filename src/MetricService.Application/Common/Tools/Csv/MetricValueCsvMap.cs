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
    public sealed class MetricValueCsvMap : ClassMap<MetricValueCsvRow>
    {
        public MetricValueCsvMap()
        {
            Map(m => m.Date)
            .Name("Date")
            .TypeConverterOption.Format(
                "yyyy-MM-dd'T'HH-mm-ss.ffff'Z'")
            .TypeConverterOption.DateTimeStyles(
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);

            Map(m => m.ExecutionTime)
                .Name("ExecutionTime");

            Map(m => m.Value)
                .Name("Value");
        }
    }
}
