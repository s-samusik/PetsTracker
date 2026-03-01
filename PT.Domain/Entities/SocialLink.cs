using PT.Domain.Enums;

namespace PT.Domain.Entities;

public sealed class SocialLink
{
    public SocialMediaType Type { get; set; }
    public string? Value { get; set; }
}
