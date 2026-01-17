using MetricService.Domain.Common;
using MetricService.Domain.Exceptions;
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

    private readonly List<MetricValue> _metricValues = new List<MetricValue>();
    public IReadOnlyCollection<MetricValue> MetricValues => _metricValues.AsReadOnly();


    protected FileResult() { } // EF Core

    private FileResult(string fileName)
    {
        Id = Guid.NewGuid();
        FileName = fileName;
    }

    public static FileResult Create(
        string fileName,
        ICollection<MetricValue> metricValues
        )
    {
        var result = new FileResult(fileName);

        result.AddMetricValueRange(metricValues);

        result.CalculateFileResult();

        return result;
    }


    private void AddMetricValueRange(ICollection<MetricValue> metricValues)
    {
        if (metricValues == null || !metricValues.Any())
            throw new InvalidFileResultException("Metric values cannot be null or empty.");

        // Проверка на валидность файла должна быть и на уровне Application,
        // эта проверка является гарантом, что Result с неправильным списком
        // не будет создан
        if (metricValues.Count < 1 || metricValues.Count > 10000)
            throw new InvalidFileResultException("Metric values must be greater than 1 and less than 10000.");

        foreach (var metric in metricValues)
        {
            AddMetricValue(metric);
        }
    }

    /// <summary>
    /// Factory метод для подсчета показателей файла
    /// При пустом _metricValues выдает исключение
    /// </summary>
    /// <exception cref="InvalidFileResultException"></exception>
    private void CalculateFileResult()
    {
        if (_metricValues == null || !_metricValues.Any())
            throw new InvalidFileResultException("For calculate result - metric values cannot be null or empty.");

        // StartTime, EndTime, DeltaTimeSeconds
        CalculateDate();

        // AverageExecutionTime, AverageValue
        CalculateAverages();

        // MedianValue, MaxValue, MinValue
        CalculateStatistics();

    }

    private void AddMetricValue(MetricValue metricValue)
    {
        if (metricValue == null)
            throw new InvalidFileResultException("Metric value cannot be null.");

        if (_metricValues.Any(m => m.Id == metricValue.Id))
            return;

        _metricValues.Add(metricValue);
    }

    private void CalculateDate()
    {
        StartDate = _metricValues.Min(m => m.Date);
        EndDate = _metricValues.Max(m => m.Date);
        
        DeltaTimeSeconds = (EndDate - StartDate).TotalSeconds;
    }

    private void CalculateAverages()
    {
        double amountTime = 0;
        double amountValue = 0;
        
        int count = _metricValues.Count;

        foreach(var metric in _metricValues)
        {
            amountTime += metric.ExecutionTime;
            amountValue += metric.Value;
        }

        AverageExecutionTime = amountTime/count;
        AverageValue = amountValue/count;
    }

    private void CalculateStatistics()
    {
        var values = _metricValues.Select(m => m.Value).ToList();

        MedianValue = CalculateMedian(values);

        MaxValue = values.Max();
        MinValue = values.Min();
    }

    private double CalculateMedian(List<double> values)
    {
        // Создается копия, так как метод подсчитыает только медиану
        // и не подразумевает собой сортировку списка
        var sortValues = new List<double>(values);
        sortValues.Sort();

        int count = values.Count;
        int middleIndex = count / 2;

        if(count % 2 == 0)
        {
            return (sortValues[middleIndex - 1] + sortValues[middleIndex]) / 2.0;
        }
        else
        {
            return sortValues[middleIndex];
        }
    }
}
