using PT.Domain.Entities;

namespace PT.Application.Interfaces.Repositories;

public interface IPetCardRepository : IBaseRepository<PetCard>
{
    Task<PetCard?> GetByCodeAsync(string code, CancellationToken ct = default);
}
