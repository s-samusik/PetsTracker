namespace PT.Application.Dtos;

public sealed record RegisterPetCardDto(
    string Code,
    string PhoneNumber,
    string PetName,
    string Address,
    string Info,
    List<SocialLinkDto> SocialLinks
);

