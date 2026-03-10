using PT.Domain.Enums;

namespace PT.Application.Dtos;

public sealed record SocialLinkDto(
    SocialMediaType Type,
    string? Username
);
