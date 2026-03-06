using Microsoft.EntityFrameworkCore;
using PT.Application.Interfaces.Repositories;
using PT.Domain.Entities;
using PT.Infrastructure.Common;

namespace PT.Infrastructure.Repositories;

internal sealed class PetCardRepository(PostgreSqlDbContext context) : BaseRepository<PetCard>(context), IPetCardRepository
{
    public async Task<PetCard?> GetByCodeAsync(string code, CancellationToken ct = default)
    {
        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Code.Value == code, ct);
    }
}
