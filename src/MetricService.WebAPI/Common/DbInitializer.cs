using Extensions.Hosting.AsyncInitialization;
using MetricService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MetricService.WebAPI.Common;

public class DbInitializer : IAsyncInitializer
{
    private readonly ApplicationDbContext _context;

    public DbInitializer(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        await _context.Database.MigrateAsync(cancellationToken);
    }
}
