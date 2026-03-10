using PT.Domain.Enums;
using PT.Domain.Models;

namespace PT.Application.Interfaces.Repositories;

public interface IPetCardRepository : IBaseRepository<PetCard>
{
    Task<PetCard?> GetByCodeAsync(string code, CancellationToken ct = default);
    Task<IReadOnlyList<PetCard>> GetAllByStateAsync(CardState state, CancellationToken ct = default);
    Task<IReadOnlyList<PetCard>> GetAllByUserAsync(Guid userId, CancellationToken ct = default);
}