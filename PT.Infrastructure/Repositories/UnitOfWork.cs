using Microsoft.EntityFrameworkCore.Storage;
using PT.Application.Interfaces.Repositories;
using PT.Infrastructure.Common;

namespace PT.Infrastructure.Repositories;

public sealed class UnitOfWork(PostgreSqlDbContext context) : IUnitOfWork
{
    private readonly PostgreSqlDbContext _context = context;
    private IDbContextTransaction? _transaction;

    public async Task BeginTransactionAsync(CancellationToken ct = default)
    {
        _transaction = await _context.Database.BeginTransactionAsync(ct);
    }

    public async Task CommitAsync(CancellationToken ct = default)
    {
        await _context.SaveChangesAsync(ct);
        
        if (_transaction != null)
        {
            await _transaction.CommitAsync(ct);
        }
    }

    public async Task RollbackAsync(CancellationToken ct = default)
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync(ct);
        }
    }

    public Task SaveChangesAsync(CancellationToken ct = default)
        => _context.SaveChangesAsync(ct);
}
