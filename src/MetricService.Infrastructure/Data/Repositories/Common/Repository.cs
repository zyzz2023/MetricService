using MetricService.Domain.Common;
using MetricService.Domain.Interfaces.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Infrastructure.Data.Repositories.Common;

// Generic Repository
public class Repository<TEntity> : IRepository<TEntity> 
    where TEntity : BaseEntity<Guid>
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetFilteredAsync(
        Expression<Func<TEntity, bool>>? predicate = null, 
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, 
        int? skip = null, 
        int? take = null, 
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = _dbSet;

        if(predicate != null)
            query = query.Where(predicate);

        if(orderBy != null)
            query = orderBy(query);

        if(skip.HasValue)
            query = query.Skip(skip.Value);

        if(take.HasValue)
            query = query.Take(take.Value);

        return await query
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }
    
    public async Task<bool> IsExsist(TEntity entity)
    {
        return await _context.Set<TEntity>().AnyAsync();
    }

    public void Update(TEntity entity)
    {
        _context.Update(entity);
    }
}
