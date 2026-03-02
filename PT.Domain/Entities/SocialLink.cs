using PT.Domain.Enums;

namespace PT.Domain.Entities;

public sealed class SocialLink : BaseEntity
{
    public SocialMediaType Type { get; set; }
    public string? Username { get; set; }
    
    public Guid PetCardId { get; set; }
    public PetCard PetCard { get; set; } = null!;
}
