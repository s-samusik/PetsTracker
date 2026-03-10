using PT.Domain.Models;
using PT.Infrastructure.Entities;

namespace PT.Infrastructure.Mappings;

internal static class SocialLinkMapping
{
    public static SocialLink ToDomain(this SocialLinkEntity entity)
    => new(
        id: entity.Id,
        type: entity.Type,
        username: entity.Username
    );

    public static SocialLinkEntity ToEntity(this SocialLink model, Guid petCardId)
        => new()
        {
            Id = model.Id,
            Type = model.Type,
            Username = model.Username,
            PetCardEntityId = petCardId
        };
}
