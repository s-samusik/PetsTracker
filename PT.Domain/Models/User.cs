using PT.Domain.Enums;

namespace PT.Domain.Models;

public class User
{
    public Guid Id { get; set; }
    public string? PhoneNumber { get; set; }
    public List<PetCard> PetCards { get; set; } = [];
}
