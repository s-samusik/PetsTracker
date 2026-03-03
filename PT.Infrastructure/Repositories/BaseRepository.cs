using Microsoft.EntityFrameworkCore;
using PT.Application.Interfaces.Repositories;

namespace PT.Infrastructure.Repositories;

internal abstract class BaseRepository<T>(DbContext context) : IBaseRepository<T> where T : class
{
    protected readonly DbContext _context = context;
    protected readonly DbSet<T> _dbSet = context.Set<T>();


    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _dbSet.FindAsync([id], ct);
    }

    public async Task AddAsync(T entity, CancellationToken ct = default)
    {
        _ = await _dbSet.AddAsync(entity, ct);
    }

    public Task Update(T entity)
    {
        _dbSet.Update(entity);
        
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        _ = await _context.SaveChangesAsync(ct);
    }
}
