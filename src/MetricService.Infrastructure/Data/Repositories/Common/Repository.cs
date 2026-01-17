using MetricService.Domain.Common;
using MetricService.Domain.Interfaces.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Infrastructure.Data.Repositories.Common;

public class Repository<T> : IRepository<T> 
    where T : BaseEntity<Guid>
{
    protected readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public async Task<bool> IsExsist(T entity)
    {
        var result = await _context.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return result != null;
    }

    public void Update(T entity)
    {
        _context.Update(entity);
    }
}
