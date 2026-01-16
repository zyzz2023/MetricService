using MetricService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Domain.Entities;

public class FileResult : BaseEntity<Guid>
{
    public string FileName { get; private set; }

    public double DeltaTimeSeconds { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public double AverageExecutionTime { get; private set; }
    public double AverageValue { get; private set; }
    public double MedianValue { get; private set; }
    public double MaxValue { get; private set; }
    public double MinValue { get; private set; }

    public ICollection<MetricValue> MetricValues { get; set; } = new List<MetricValue>();

    protected FileResult() { } // EF Core

    private FileResult(
        string fileName,
        DateTime startDate,
        DateTime endDate,
        double averageExecutionTime,
        double averageValue,
        double medianValue,
        double maxValue,
        double minValue
        )
    {
        FileName = fileName;
        StartDate = startDate;
        EndDate = endDate;
        AverageExecutionTime = averageExecutionTime;
        AverageValue = averageValue;
        MedianValue = medianValue;
        MaxValue = maxValue;
        MinValue = minValue;
    }

    public static FileResult Create(
        string fileName,
        DateTime startDate,
        DateTime endDate,
        double averageExecutionTime,
        double averageValue,
        double medianValue,
        double maxValue,
        double minValue
        )
    {
        return new FileResult(
            fileName,
            startDate,
            endDate,
            averageExecutionTime,
            averageValue,
            medianValue,
            maxValue,
            minValue
            );
    }
}
