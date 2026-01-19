namespace MetricService.Application.Features.Metric.Common;

public class MetricValueDto
{
    public string FileName { get; set; }

    public DateTime Date { get; set; }
    public double ExecutionTime { get; set; }
    public double Value { get; set; }
}