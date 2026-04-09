using PT.Domain.Enums;

namespace PT.Infrastructure.Entities;

public sealed class PrivacyPolicyEntity : BaseEntity
{
    public required string Value { get; set; }
    public required int Version { get; set; }
    public UserType UserType { get; set; } = UserType.Undefined;
}
