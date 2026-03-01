namespace PT.Domain.Entities;

public sealed class User : BaseEntity
{
    public string? PhoneNumber { get; set; }
    public List<PetCard> PetCards { get; set; } = [];
}
