using PT.Domain.Enums;

namespace PT.Infrastructure.Entities;

public sealed class PetCardEntity : BaseEntity
{
    public string? PetName { get; set; }
    public CardState State { get; set; } = CardState.Undefined;
    public string? PhotoUrl { get; set; }
    public List<SocialLinkEntity> SocialLinkEntities { get; set; } = [];

    public Guid UserEntityId { get; set; }
    public UserEntity UserEntity { get; set; } = null!;
    public Guid CodeEntityId { get; set; }
    public CodeEntity CodeEntity { get; set; } = null!;
}
