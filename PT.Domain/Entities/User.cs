namespace PT.Domain.Entities;

public sealed class User : BaseEntity
{
    public required string PhoneNumber { get; set; }
    public List<PetCard> PetCards { get; set; } = [];
}
