using MetricService.Domain.Entities;
using MetricService.Domain.Interfaces;
using MetricService.Infrastructure.Data.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Infrastructure.Data.Repositories;

public class FileResultRepository : Repository<FileResult>, IFileResultRepository
{
    private readonly ApplicationDbContext _context;

    public FileResultRepository(ApplicationDbContext context) : base(context) { }

    public async Task AddWithOverwriteAsync(
        string fileName, 
        FileResult fileResult, 
        CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var existsingResult = await _context.Results
            .FirstOrDefaultAsync(x => x.FileName == fileName, cancellationToken);

            if (existsingResult != null)
            {
                _context.Results.Remove(existsingResult);
                await _context.SaveChangesAsync(cancellationToken);
            }

            await _context.AddAsync(fileResult, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<ICollection<FileResult>> GetFilteredAsync(
        string? fileName, 
        DateTime? startDateFrom, 
        DateTime? startDateTo, 
        double? averageValueFrom, 
        double? averageValueTo, 
        double? averageExecutionTimeFrom,
        double? averageExecutionTimeTo, 
        int page = 1, 
        int pageSize = 50, 
        CancellationToken cancellationToken = default)
    {
        var query = _context.Results.AsQueryable();

        query = ResultFilters.ApplyFilters(query, fileName, 
            startDateFrom, startDateTo,
            averageValueFrom, averageValueTo, 
            averageExecutionTimeFrom, averageExecutionTimeTo);

        var totalCount = query.CountAsync(cancellationToken);

        var result = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return result;
    }
}
