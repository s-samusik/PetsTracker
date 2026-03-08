using PT.Domain.Entities;
using PT.Domain.Enums;

namespace PT.Application.Interfaces.Repositories;

public interface IPetCardRepository : IBaseRepository<PetCard>
{
    Task<PetCard?> GetByCodeAsync(string code, CancellationToken ct = default);
    Task<IReadOnlyList<PetCard>> GetAllByStateAsync(CardState state, CancellationToken ct = default);
    Task<IReadOnlyList<PetCard>> GetAllByUserAsync(Guid userId, CancellationToken ct = default);
}
