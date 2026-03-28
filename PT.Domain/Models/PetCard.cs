using PT.Domain.Enums;

namespace PT.Domain.Models;

public sealed class PetCard : BaseEntity
{
    public string? PetName { get; private set; }
    public CardState State { get; private set; }
    public string? PhotoUrl { get; private set; }
    public string? Address {get; private set; }
    public string? Info { get; private set; }

    public Guid UserId { get; private set; }
    public Guid CodeId { get; private set; }

    private readonly List<SocialLink> _socialLinks = [];
    public IReadOnlyList<SocialLink> SocialLinks => _socialLinks;

    private PetCard(Guid userId, Guid codeId, string? petName, string? address, string? info, IEnumerable<SocialLink> links)
    {
        UserId = userId;
        CodeId = codeId;
        PetName = petName;
        Address = address;
        Info = info;
        State = CardState.Registered;
        _socialLinks.AddRange(links);
    }

    public PetCard(
        Guid id,
        Guid userId,
        Guid codeId,
        string? petName,
        string? photoUrl,
        string? address,
        string? info,
        CardState state,
        DateTimeOffset createdAt,
        DateTimeOffset? updatedAt,
        IEnumerable<SocialLink> links)
    {
        Id = id;
        UserId = userId;
        CodeId = codeId;
        PetName = petName;
        PhotoUrl = photoUrl;
        Address = address;
        Info = info;
        State = state;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        _socialLinks.AddRange(links);
    }


    public static PetCard Register(User user, Code code, string? petName, string? address, string? info, IEnumerable<SocialLink> links)
    {
        return code.State == CodeState.Generated
            ? new PetCard(user.Id, code.Id, petName, address, info, links)
            : throw new InvalidOperationException($"Code: '{code.Value}' is not valid, state: '{code.State}'");
    }

    public void UpdatePhoto(string url)
    {
        PhotoUrl = url;

        MarkUpdated();
    }
}

