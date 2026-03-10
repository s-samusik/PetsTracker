using PT.Application.Dtos;

namespace PT.Api.Requests.PetCard;

public sealed record PetCardRegisterRequest
    (string Code, string PhoneNumber, string PetName, List<SocialLinkDto> SocialLinks);
