using PT.Domain.Enums;

namespace PT.Domain.Entities;

public sealed class PetCard : BaseEntity
{
    public string? PetName { get; set; }
    public CardState State { get; set; }
    public string? PhotoUrl { get; set; }
    public List<SocialLink> SocialLinks { get; set; } = [];
}
