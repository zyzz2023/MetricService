using MetricService.Domain.Entities;
using MetricService.Domain.Interfaces;
using MetricService.Infrastructure.Data.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Infrastructure.Data.Repositories
{
    public class MetricValueRepository : Repository<MetricValue>, IMetricValueRepository
    {
        public MetricValueRepository(ApplicationDbContext context) : base(context) { }

        public async Task<ICollection<MetricValue>> GetLatestByFileNameAsync(
            string fileName, 
            int count = 10, 
            CancellationToken cancellationToken = default)
        {
            var values = await _context.Values
                .Where(x => x.FileName == fileName)
                .OrderByDescending(v => v.Date)
                .Take(count)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return values;
        }
    }
}
