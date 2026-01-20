using ErrorOr;
using MetricService.Domain.Common;
using MetricService.Domain.Common.Errors;
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

    public static ErrorOr<MetricValue> Create(string fileName, DateTime date, double executionTime, double value)
    {
        if (date < new DateTime(2000, 01, 01) || date > DateTime.UtcNow)
            return DomainError.InvalidDateTime;

        if(executionTime < 0)
            return DomainError.InvalidExecutionTime;

        if (value < 0)
            return DomainError.InvalidValue;

        return new MetricValue(fileName, date, executionTime, value);
    }

    internal void LinkToFileResult(FileResult fileResult)
    {
        ResultId = fileResult.Id;
        Result = fileResult;
    }
}
