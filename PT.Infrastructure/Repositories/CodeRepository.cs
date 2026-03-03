using Microsoft.EntityFrameworkCore;
using PT.Application.Interfaces.Repositories;
using PT.Domain.Entities;
using PT.Infrastructure.Common;

namespace PT.Infrastructure.Repositories;

internal sealed class CodeRepository(PostgreSqlDbContext context) : BaseRepository<Code>(context), ICodeRepository
{
    public async Task<Code?> GetByValueAsync(string code, CancellationToken ct = default)
    {
        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Value == code, ct);
    }
}
