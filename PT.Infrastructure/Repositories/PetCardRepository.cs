using Microsoft.EntityFrameworkCore;
using PT.Application.Interfaces.Repositories;
using PT.Domain.Entities;
using PT.Domain.Enums;
using PT.Infrastructure.Common;

namespace PT.Infrastructure.Repositories;

internal sealed class PetCardRepository(PostgreSqlDbContext context) : BaseRepository<PetCard>(context), IPetCardRepository
{
    public async Task<IReadOnlyList<PetCard>> GetAllByStateAsync(CardState state, CancellationToken ct = default)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(x => x.State == state)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<PetCard>> GetAllByUserAsync(Guid userId, CancellationToken ct = default)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .ToListAsync(ct);
    }

    public async Task<PetCard?> GetByCodeAsync(string code, CancellationToken ct = default)
    {
        return await _dbSet
            .AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.Code)
            .Include(x => x.SocialLinks)
            .FirstOrDefaultAsync(x => x.Code.Value == code, ct);
    }
}
