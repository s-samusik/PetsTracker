using PT.Domain.Enums;

namespace PT.Domain.Models;

public class SocialLink
{
    public SocialMediaType Type { get; set; }
    public string? Value { get; set; }
}
