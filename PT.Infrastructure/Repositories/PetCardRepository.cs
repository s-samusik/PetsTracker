using Microsoft.EntityFrameworkCore;
using PT.Application.Interfaces.Repositories;
using PT.Domain.Enums;
using PT.Domain.Models;
using PT.Infrastructure.Common;
using PT.Infrastructure.Mappings;

namespace PT.Infrastructure.Repositories;

internal sealed class PetCardRepository(PostgreSqlDbContext context) : IPetCardRepository
{
    private readonly PostgreSqlDbContext _context = context;

    public async Task<PetCard?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _context.PetCards
            .Include(x => x.SocialLinkEntities)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        return entity?.ToDomain();
    }

    public async Task<PetCard?> GetByCodeAsync(string code, CancellationToken ct = default)
    {
        var entity = await _context.PetCards
            .Include(x => x.SocialLinkEntities)
            .Include(x => x.CodeEntity)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.CodeEntity.Value == code, ct);

        return entity?.ToDomain();
    }

    public async Task<IReadOnlyList<PetCard>> GetAllByStateAsync(CardState state, CancellationToken ct = default)
    {
        var entities = await _context.PetCards
            .Include(x => x.SocialLinkEntities)
            .Where(x => x.State == state)
            .AsNoTracking()
            .ToListAsync(ct);

        return [.. entities.Select(x => x.ToDomain())];
    }

    public async Task<IReadOnlyList<PetCard>> GetAllByUserAsync(Guid userId, CancellationToken ct = default)
    {
        var entities = await _context.PetCards
            .Include(x => x.SocialLinkEntities)
            .Where(x => x.UserEntityId == userId)
            .AsNoTracking()
            .ToListAsync(ct);

        return [.. entities.Select(x => x.ToDomain())];
    }

    public async Task AddAsync(PetCard model, CancellationToken ct = default)
    {
        var entity = model.ToEntity();
        await _context.PetCards.AddAsync(entity, ct);
    }

    public void Update(PetCard model)
    {
        var entity = _context.PetCards
            .Include(x => x.SocialLinkEntities)
            .First(x => x.Id == model.Id);

        entity.PetName = model.PetName;
        entity.PhotoUrl = model.PhotoUrl;
        entity.State = model.State;
        entity.Address = model.Address;

        entity.UserEntityId = model.UserId;
        entity.CodeEntityId = model.CodeId;

        entity.SocialLinkEntities.Clear();
        
        entity.SocialLinkEntities.AddRange(
            model.SocialLinks.Select(link => link.ToEntity(model.Id)));
    }
}