using PT.Domain.Models;
using PT.Infrastructure.Entities;

namespace PT.Infrastructure.Mappings;

internal static class UserMapping
{
    public static User ToDomain(this UserEntity entity)
        => new(
            id: entity.Id,
            phone: entity.PhoneNumber,
            createdAt: entity.CreatedAt,
            updatedAt: entity.UpdatedAt
        );

    public static UserEntity ToEntity(this User model)
        => new()
        {
            Id = model.Id,
            PhoneNumber = model.PhoneNumber,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt
        };
}
