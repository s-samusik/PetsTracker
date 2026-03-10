using PT.Domain.Enums;

namespace PT.Infrastructure.Entities;

public sealed class SocialLinkEntity : BaseEntity
{
    public SocialMediaType Type { get; set; }
    public string? Username { get; set; }

    public Guid PetCardEntityId { get; set; }
    public PetCardEntity PetCardEntity { get; set; } = null!;
}
