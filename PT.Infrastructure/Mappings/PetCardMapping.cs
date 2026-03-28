using PT.Domain.Models;
using PT.Infrastructure.Entities;

namespace PT.Infrastructure.Mappings;

internal static class PetCardMapping
{
    public static PetCard ToDomain(this PetCardEntity entity)
    {
        var links = entity.SocialLinkEntities
            .Select(x => x.ToDomain())
            .ToList();

        return new PetCard(
            id: entity.Id,
            userId: entity.UserEntityId,
            codeId: entity.CodeEntityId,
            petName: entity.PetName,
            photoUrl: entity.PhotoUrl,
            address: entity.Address,
            info: entity.Info,
            state: entity.State,
            createdAt: entity.CreatedAt,
            updatedAt: entity.UpdatedAt,
            links: links
        );
    }

    public static PetCardEntity ToEntity(this PetCard model)
        => new()
        {
            Id = model.Id,
            PetName = model.PetName,
            PhotoUrl = model.PhotoUrl,
            Address = model.Address,
            Info = model.Info,
            State = model.State,
            UserEntityId = model.UserId,
            CodeEntityId = model.CodeId,
            SocialLinkEntities = [.. model.SocialLinks.Select(x => x.ToEntity(model.Id))],
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt
        };
}
