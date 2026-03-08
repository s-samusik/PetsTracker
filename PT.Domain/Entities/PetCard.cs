using PT.Domain.Enums;

namespace PT.Domain.Entities;

public sealed class PetCard : BaseEntity
{
    public string? PetName { get; set; }
    public CardState State { get; set; } = CardState.Undefined;
    public string? PhotoUrl { get; set; }
    public List<SocialLink> SocialLinks { get; set; } = [];

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public Guid CodeId { get; set; }
    public Code Code { get; set; } = null!;

    public static PetCard Register(Code code, string petName, User user, List<SocialLink> links)
    {
        var card = new PetCard
        {
            CodeId = code.Id,
            UserId = user.Id,
            PetName = petName,
            PhotoUrl = null,
            State = CardState.Registered,
            SocialLinks = links,
            CreatedAt = DateTime.UtcNow
        };

        return card;
    }
}
