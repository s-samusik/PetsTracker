using PT.Application.Interfaces.Repositories;
using PT.Application.Interfaces.Services;
using PT.Domain.Entities;
using PT.Domain.Enums;

namespace PT.Application.Services;

internal sealed class PetCardService(IPetCardRepository cardRepository, ICodeRepository codeRepository) : IPetCardService
{
    private readonly IPetCardRepository _cardRepository = cardRepository;
    private readonly ICodeRepository _codeRepository = codeRepository;

    public async Task<IReadOnlyList<PetCard>> GetAllByStateAsync(CardState state, CancellationToken ct = default)
        => await _cardRepository.GetAllByStateAsync(state, ct);

    public async Task<IReadOnlyList<PetCard>> GetAllByUserAsync(Guid userId, CancellationToken ct = default)
        => await _cardRepository.GetAllByUserAsync(userId, ct);

    public async Task<PetCard?> GetByCodeAsync(string code, CancellationToken ct = default)
        => await _cardRepository.GetByCodeAsync(code, ct);

    public async Task RegisterAsync(string code, CancellationToken ct = default)
    {
        var codeItem = await _codeRepository.GetByValueAsync(code, ct) ?? throw new InvalidOperationException($"Code '{code}' not found");
        
        if (codeItem.State != CodeState.Generated)
        {
            throw new InvalidOperationException($"Cannot register card, invalid code: {code}, state: {codeItem?.State}");
        }
    }
}
