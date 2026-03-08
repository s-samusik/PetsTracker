using PT.Domain.Entities;

namespace PT.Api.Requests.PetCard;

public sealed record PetCardRegisterRequest
    (string Code, string PhoneNumber, string PetName, List<SocialLink> SocialLinks);
