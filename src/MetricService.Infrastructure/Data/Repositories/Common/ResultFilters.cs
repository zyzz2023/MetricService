using MetricService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Infrastructure.Data.Repositories.Common;

public static class ResultFilters
{
    public static IQueryable<FileResult> ApplyFilters(
    IQueryable<FileResult> query,
    string? fileName,
    DateTime? startDateFrom,
    DateTime? startDateTo,
    double? averageValueFrom,
    double? averageValueTo,
    double? averageExecutionTimeFrom,
    double? averageExecutionTimeTo)
    {
        if (!string.IsNullOrWhiteSpace(fileName))
        {
            query = query.Where(r => r.FileName.Contains(fileName));
        }

        if (startDateFrom.HasValue)
        {
            query = query.Where(r => r.StartDate >= startDateFrom.Value);
        }

        if (startDateTo.HasValue)
        {
            query = query.Where(r => r.StartDate <= startDateTo.Value);
        }

        if (averageValueFrom.HasValue)
        {
            query = query.Where(r => r.AverageValue >= averageValueFrom.Value);
        }

        if (averageValueTo.HasValue)
        {
            query = query.Where(r => r.AverageValue <= averageValueTo.Value);
        }

        if (averageExecutionTimeFrom.HasValue)
        {
            query = query.Where(r => r.AverageExecutionTime >= averageExecutionTimeFrom.Value);
        }

        if (averageExecutionTimeTo.HasValue)
        {
            query = query.Where(r => r.AverageExecutionTime <= averageExecutionTimeTo.Value);
        }

        return query;
    }
}
