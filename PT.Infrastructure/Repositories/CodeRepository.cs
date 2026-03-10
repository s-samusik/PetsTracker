using Microsoft.EntityFrameworkCore;
using PT.Application.Interfaces.Repositories;
using PT.Domain.Enums;
using PT.Infrastructure.Common;
using PT.Infrastructure.Mappings;

namespace PT.Infrastructure.Repositories;

internal sealed class CodeRepository(PostgreSqlDbContext context) : ICodeRepository
{
    private readonly PostgreSqlDbContext _context = context;

    public async Task<Code?> GetByValueAsync(string value, CancellationToken ct = default)
    {
        var entity = await _context.Codes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Value == value, ct);

        return entity?.ToDomain();
    }

    public async Task<Code?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _context.Codes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        return entity?.ToDomain();
    }

    public async Task<IReadOnlyList<Code>> GetAllByStateAsync(CodeState state, CancellationToken ct = default)
    {
        var entities = await _context.Codes
            .AsNoTracking()
            .Where(x => x.State == state)
            .ToListAsync(ct);

        return [.. entities.Select(x => x.ToDomain())];
    }

    public async Task AddAsync(Code model, CancellationToken ct = default)
    {
        var entity = model.ToEntity();
        await _context.Codes.AddAsync(entity, ct);
    }

    public void Update(Code model)
    {
        var entity = _context.Codes.First(x => x.Id == model.Id);

        entity.State = model.State;
        entity.ActivatedAt = model.ActivatedAt;
        entity.DeactivatedAt = model.DeactivatedAt;
        entity.PetCardEntityId = model.PetCardId;
        entity.UpdatedAt = model.UpdatedAt;
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await _context.SaveChangesAsync(ct);
}
