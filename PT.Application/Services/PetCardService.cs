using PT.Application.Dtos;
using PT.Application.Interfaces.Repositories;
using PT.Application.Interfaces.Services;
using PT.Domain.Models;
using PT.Domain.Enums;

namespace PT.Application.Services;

internal sealed class PetCardService
    (IUnitOfWork uow, IPetCardRepository cardRepository, ICodeService codeService, IUserService userService)
    : IPetCardService
{
    private readonly IUnitOfWork _uow = uow;
    private readonly IPetCardRepository _cardRepository = cardRepository;
    private readonly ICodeService _codeService = codeService;
    private readonly IUserService _userService = userService;

    public async Task<IReadOnlyList<PetCard>> GetAllByStateAsync(CardState state, CancellationToken ct = default)
        => await _cardRepository.GetAllByStateAsync(state, ct);

    public async Task<IReadOnlyList<PetCard>> GetAllByUserAsync(Guid userId, CancellationToken ct = default)
        => await _cardRepository.GetAllByUserAsync(userId, ct);

    public async Task<PetCard?> GetByCodeAsync(string code, CancellationToken ct = default)
        => await _cardRepository.GetByCodeAsync(code, ct);

    public async Task<PetCard> RegisterAsync(RegisterPetCardDto dto, CancellationToken ct = default)
    {
        await _uow.BeginTransactionAsync(ct);

        try
        {
            var code = await _codeService.GetByValueAsync(dto.Code, ct);

            var user = await _userService.ExistsAsync(dto.PhoneNumber, ct)
                ? await _userService.GetByPhoneAsync(dto.PhoneNumber, ct)
                : await _userService.RegisterAsync(dto.PhoneNumber, ct);

            var links = dto.SocialLinks
                .Select(x => SocialLink.Create(x.Type, x.Username))
                .ToList();

            var card = PetCard.Register(user!, code!, dto.PetName, links);

            code!.AssignToCard(card.Id);

            await _cardRepository.AddAsync(card, ct);

            await _uow.CommitAsync(ct);

            return card;
        }
        catch
        {
            await _uow.RollbackAsync(ct);
            throw;
        }
    }
}
