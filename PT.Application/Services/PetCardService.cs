using PT.Application.Dtos;
using PT.Application.Interfaces.Repositories;
using PT.Application.Interfaces.Services;
using PT.Domain.Models;
using PT.Domain.Enums;

namespace PT.Application.Services;

public sealed class PetCardService(
    IUnitOfWork uow,
    IPetCardRepository cardRepository,
    ICodeService codeService,
    ImageProcessingService imageProcessor,
    IFileStorageService fileStorage,
    IUserService userService)
    : IPetCardService
{
    private readonly IUnitOfWork _uow = uow;
    private readonly IPetCardRepository _cardRepository = cardRepository;
    private readonly ICodeService _codeService = codeService;
    private readonly IUserService _userService = userService;
    private readonly ImageProcessingService _imageProcessor = imageProcessor;
    private readonly IFileStorageService _fileStorage = fileStorage;

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
            await _codeService.UpdateAsync(code, ct);

            await _uow.CommitAsync(ct);

            return card;
        }
        catch
        {
            await _uow.RollbackAsync(ct);
            throw;
        }
    }

    public async Task<string> UploadAvatarAsync(Guid cardId, Stream file, string contentType, CancellationToken ct = default)
    {
        var card = await _cardRepository.GetByIdAsync(cardId, ct)
            ?? throw new InvalidOperationException($"Card '{cardId}' not found");

        var processed = await _imageProcessor.ProcessAsync(file, MediaType.Avatar);

        var fileName = $"avatars/{cardId}.webp";

        var url = await _fileStorage.UploadAsync(processed, fileName, "image/webp", ct);

        card.UpdatePhoto(url);

        _cardRepository.Update(card);
        await _uow.SaveChangesAsync(ct);

        return url;
    }
}
