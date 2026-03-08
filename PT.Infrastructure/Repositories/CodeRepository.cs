using Microsoft.EntityFrameworkCore;
using PT.Application.Interfaces.Repositories;
using PT.Domain.Entities;
using PT.Domain.Enums;
using PT.Infrastructure.Common;

namespace PT.Infrastructure.Repositories;

internal sealed class CodeRepository(PostgreSqlDbContext context) : BaseRepository<Code>(context), ICodeRepository
{
    public async Task<IReadOnlyList<Code>> GetAllByStateAsync(CodeState state, CancellationToken ct = default)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(x => x.State == state)
            .ToListAsync(ct);
    }

    public async Task<Code?> GetByValueAsync(string code, CancellationToken ct = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(x => x.Value == code, ct);
    }
}
