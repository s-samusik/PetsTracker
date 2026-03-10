namespace PT.Infrastructure.Entities;

public sealed class UserEntity : BaseEntity
{
    public required string PhoneNumber { get; set; }
    public List<PetCardEntity> PetCardEntities { get; set; } = [];
}
