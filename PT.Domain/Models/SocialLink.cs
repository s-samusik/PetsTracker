using PT.Domain.Enums;

namespace PT.Domain.Models;

public sealed class SocialLink : BaseEntity
{
    public SocialMediaType Type { get; private set; }
    public string? Username { get; private set; }

    private SocialLink(SocialMediaType type, string? username)
    {
        Type = type;
        Username = username;
    }

    public SocialLink(Guid id, SocialMediaType type, string? username)
    {
        Id = id;
        Type = type;
        Username = username;
    }

    public static SocialLink Create(SocialMediaType type, string? username)
        => new(type, username);
}

