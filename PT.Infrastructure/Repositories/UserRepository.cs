using Microsoft.EntityFrameworkCore;
using PT.Application.Interfaces.Repositories;
using PT.Domain.Entities;
using PT.Infrastructure.Common;

namespace PT.Infrastructure.Repositories;

internal sealed class UserRepository(PostgreSqlDbContext context) : BaseRepository<User>(context), IUserRepository
{
    public async Task<bool> ExistsAsync(string phoneNumber, CancellationToken ct = default)
    {
        return await _dbSet
            .AsNoTracking()
            .AnyAsync(x => x.PhoneNumber == phoneNumber, ct);
    }

    public async Task<User?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken ct = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber, ct);
    }
}
