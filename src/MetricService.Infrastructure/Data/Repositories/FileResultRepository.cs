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
}
