using PT.Domain.Enums;

namespace PT.Domain.Models;

public sealed class PrivacyPolicy
{
    public Guid Id { get; private set; }
    public int Version { get; private set; }
    public string Value { get; private set; }
    public UserType UserType { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    public PrivacyPolicy(int version, string value, UserType userType)
    {
        Id = Guid.NewGuid();
        Version = version;
        Value = value;
        UserType = userType;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public PrivacyPolicy(Guid id, int version, string value, UserType userType, DateTimeOffset createdAt)
    {
        Id = id;
        Version = version;
        Value = value;
        UserType = userType;
        CreatedAt = createdAt;
    }

    public static PrivacyPolicy Create(int version, string value, UserType userType)
        => new(version, value, userType);
}
