using PT.Domain.Entities;

namespace PT.Application.Dtos;

public sealed record RegisterPetCardDto(
    string Code,
    string PhoneNumber,
    string PetName,
    List<SocialLink> SocialLinks
);

