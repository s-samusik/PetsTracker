using Microsoft.EntityFrameworkCore;
using PT.Infrastructure.Common;

namespace PT.Infrastructure.Repositories;

internal abstract class BaseRepository<TEntity>(PostgreSqlDbContext context)
    where TEntity : class
{
    protected readonly PostgreSqlDbContext _context = context;
    protected readonly DbSet<TEntity> _set = context.Set<TEntity>();

    protected async Task<TEntity?> FindEntityAsync(Guid id, CancellationToken ct)
        => await _set.FindAsync([id], ct);

    protected async Task AddEntityAsync(TEntity entity, CancellationToken ct)
        => await _set.AddAsync(entity, ct);
}