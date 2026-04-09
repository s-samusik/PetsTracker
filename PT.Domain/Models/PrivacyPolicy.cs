using PT.Domain.Enums;

namespace PT.Domain.Models;

public sealed class PrivacyPolicy
{
    public Guid Id { get; private set; }
    public int Version { get; private set; }
    public string Value { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    public PrivacyPolicy(Guid id, int version, string value, DateTimeOffset createdAt)
    {
        Id = id;
        Version = version;
        Value = value;
        CreatedAt = createdAt;
    }
}
