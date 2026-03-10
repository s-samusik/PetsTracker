using PT.Domain.Enums;
using PT.Domain.Models;

public sealed class Code : BaseEntity
{
    public string Value { get; private set; }
    public CodeState State { get; private set; }
    public DateTimeOffset? ActivatedAt { get; private set; }
    public DateTimeOffset? DeactivatedAt { get; private set; }

    public Guid? PetCardId { get; private set; }

    private Code(string value)
    {
        Value = value;
        State = CodeState.Generated;
    }

    public Code(
        Guid id,
        string value,
        CodeState state,
        DateTimeOffset createdAt,
        DateTimeOffset? updatedAt,
        DateTimeOffset? activatedAt,
        DateTimeOffset? deactivatedAt,
        Guid? petCardId)
    {
        Id = id;
        Value = value;
        State = state;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        ActivatedAt = activatedAt;
        DeactivatedAt = deactivatedAt;
        PetCardId = petCardId;
    }

    public static Code Generate(string value)
        => new(value);

    public void AssignToCard(Guid petCardId)
    {
        if (State != CodeState.Generated)
            throw new InvalidOperationException($"Cannot assign code: '{Value}' with state: '{State}' to card with Id: '{petCardId}'");

        PetCardId = petCardId;
        ActivatedAt = DateTimeOffset.UtcNow;
        State = CodeState.Activated;

        MarkUpdated();
    }
}