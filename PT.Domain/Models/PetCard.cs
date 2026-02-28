using PT.Domain.Enums;

namespace PT.Domain.Models;

public class PetCard
{
    public Guid Id { get; set; }
    public string? PetName { get; set; }
    public CardState State { get; set; }
    public string? PhotoUrl { get; set; }
    public List<SocialLink> SocialLinks { get; set; } = new();
}
