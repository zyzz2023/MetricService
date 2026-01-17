using MetricService.Domain.Entities;
using MetricService.Domain.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Domain.Interfaces;

public interface IFileResultRepository : IRepository<FileResult>
{
    Task AddWithOverwriteAsync(
        string fileName, 
        FileResult fileResult, 
        CancellationToken cancellationToken
        );

    Task<ICollection<FileResult>> GetFilteredAsync(
        string? fileName,
        DateTime? startDateFrom,
        DateTime? startDateTo,
        double? averageValueFrom,
        double? averageValueTo,
        double? averageExecutionTimeFrom,
        double? averageExecutionTimeTo,
        int page = 1,
        int pageSize = 50,
        CancellationToken cancellationToken = default
        );
}
