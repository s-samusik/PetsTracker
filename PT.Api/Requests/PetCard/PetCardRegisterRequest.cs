using PT.Application.Dtos;

namespace PT.Api.Requests.PetCard;

public sealed record PetCardRegisterRequest
    (string Code, string PhoneNumber, string PetName, string Address, string Info, List<SocialLinkDto> SocialLinks);
