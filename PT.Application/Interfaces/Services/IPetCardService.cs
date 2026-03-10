using PT.Application.Dtos;
using PT.Domain.Models;
using PT.Domain.Enums;

namespace PT.Application.Interfaces.Services;

public interface IPetCardService
{
    Task<PetCard?> GetByCodeAsync(string code, CancellationToken ct = default);
    Task <PetCard> RegisterAsync(RegisterPetCardDto dto,  CancellationToken ct = default);
    Task<IReadOnlyList<PetCard>> GetAllByUserAsync(Guid userId, CancellationToken ct = default);
    Task<IReadOnlyList<PetCard>> GetAllByStateAsync(CardState state, CancellationToken ct = default);
}
