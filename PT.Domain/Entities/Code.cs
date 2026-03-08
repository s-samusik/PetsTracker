using PT.Domain.Enums;

namespace PT.Domain.Entities;

public sealed class Code : BaseEntity
{
    public required string Value { get; set; }
    public CodeState State { get; set; } = CodeState.Undefined;
    public DateTimeOffset? ActivatedAt { get; set; }
    public DateTimeOffset? DeactivatedAt { get; set; }

    public Guid? PetCardId { get; set; }
    public PetCard? PetCard { get; set; }

    public void AssignToCard(Guid petCardId)
    {
        if (State != CodeState.Generated)
        {
            throw new InvalidOperationException($"Cannot register card, invalid code: {Value}, state: {State}");
        }

        PetCardId = petCardId;
        UpdatedAt = DateTime.UtcNow;
        State = CodeState.Activated;
    }
}
