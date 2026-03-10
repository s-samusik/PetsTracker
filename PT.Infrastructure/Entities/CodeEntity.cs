using PT.Domain.Enums;

namespace PT.Infrastructure.Entities;

public sealed class CodeEntity : BaseEntity
{
    public required string Value { get; set; }
    public CodeState State { get; set; } = CodeState.Undefined;
    public DateTimeOffset? ActivatedAt { get; set; }
    public DateTimeOffset? DeactivatedAt { get; set; }

    public Guid? PetCardEntityId { get; set; }
    public PetCardEntity? PetCardEntity { get; set; }
}
