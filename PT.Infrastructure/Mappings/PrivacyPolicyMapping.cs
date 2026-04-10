using PT.Domain.Models;
using PT.Infrastructure.Entities;

namespace PT.Infrastructure.Mappings;

public static class PrivacyPolicyMapping
{
    public static PrivacyPolicy ToDomain(this PrivacyPolicyEntity entity)
        => new(
            id: entity.Id,
            value: entity.Value,
            version: entity.Version,
            userType: entity.UserType,
            createdAt: entity.CreatedAt
        );

    public static PrivacyPolicyEntity ToEntity(this PrivacyPolicy model)
        => new()
        {
            Id = model.Id,
            Value = model.Value,
            Version = model.Version,
            UserType = model.UserType,
            CreatedAt = model.CreatedAt,
        };
}
