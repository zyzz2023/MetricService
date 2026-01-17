using MetricService.Domain.Common;
using MetricService.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Domain.Entities;

public class MetricValue : BaseEntity<Guid>
{
    public string FileName { get; private set; }

    public DateTime Date { get; private set; }
    public double ExecutionTime { get; private set; }
    public double Value { get; private set; }

    public Guid ResultId { get; private set; }
    public FileResult Result { get; private set; }

    protected MetricValue() { } // EF COre

    private MetricValue(string fileName, DateTime date, double executionTime, double value)
    {
        Id = Guid.NewGuid();
        FileName = fileName;
        Date = date;
        ExecutionTime = executionTime;
        Value = value;
    }

    public static MetricValue Create(string fileName, DateTime date, double executionTime, double value)
    {
        Validate(fileName, date, executionTime, value);

        return new MetricValue(fileName, date, executionTime, value);
    }

    private static void Validate(string fileName, DateTime date, double executionTime, double value)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            throw new InvalidFileNameException("File name can't be null or empty.");

        if (date > DateTime.Now || date < new DateTime(2000, 01, 01))
            throw new InvalidDateException("The date cannot be later than the current date or earlier than 01.01.2000");

        if (executionTime < 0)
            throw new InvalidExecutionTimeException("The execution time cannot be less than 0");

        if (value < 0)
            throw new InvalidMetricValueException("The value cannot be less than 0");
    }

    internal void LinkToFileResult(FileResult fileResult)
    {
        if (fileResult == null)
            throw new InvalidFileResultException("File result cannot be null.");

        ResultId = fileResult.Id;
        Result = fileResult;
    }
}
