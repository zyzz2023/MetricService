using MetricService.Domain.Entities;
using MetricService.Domain.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Domain.Interfaces
{
    public interface IMetricValueRepository : IRepository<MetricValue>
    {
        Task<ICollection<MetricValue>> GetFilteredAsync(
            string fileName,
            int count = 10,
            CancellationToken cancellationToken = default
            );
    }
}
