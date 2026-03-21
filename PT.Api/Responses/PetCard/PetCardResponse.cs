using PT.Domain.Enums;

namespace PT.Api.Responses.PetCard;

public sealed record PetCardResponse(
    Guid Id,
    string Code,
    string? PetName,
    string? PhotoUrl,
    string? Address,
    CardState State,
    List<SocialLinkResponse> SocialLinks
);

public sealed record SocialLinkResponse(
    SocialMediaType Type,
    string? Username
);