using PT.Infrastructure.Entities;

namespace PT.Infrastructure.Mappings;

internal static class CodeMapping
{
    public static Code ToDomain(this CodeEntity entity)
        => new(
            id: entity.Id,
            value: entity.Value,
            state: entity.State,
            createdAt: entity.CreatedAt,
            updatedAt: entity.UpdatedAt,
            activatedAt: entity.ActivatedAt,
            deactivatedAt: entity.DeactivatedAt,
            petCardId: entity.PetCardEntityId
        );

    public static CodeEntity ToEntity(this Code model)
        => new()
        {
            Id = model.Id,
            Value = model.Value,
            State = model.State,
            ActivatedAt = model.ActivatedAt,
            DeactivatedAt = model.DeactivatedAt,
            PetCardEntityId = model.PetCardId,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt
        };
}

