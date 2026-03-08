namespace PT.Domain.Entities;

public sealed class User : BaseEntity
{
    public required string PhoneNumber { get; set; }
    public List<PetCard> PetCards { get; set; } = [];

    public void AddPetCard(PetCard card)
    {
        if (PetCards.Any(x => x.Id == card.Id))
        {
            throw new InvalidOperationException($"Card with Id '{card.Id}' already assigned to user with Id '{Id}'");
        }

        PetCards.Add(card);
        UpdatedAt = DateTime.UtcNow;
    }
}
