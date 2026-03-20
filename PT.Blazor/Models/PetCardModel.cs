namespace PT.Blazor.Models;

public sealed class PetCardModel
{
    public ManualEntryModel ManualEntry { get; set; } = new();
    public OwnerModel Owner { get; set; } = new();
    public PetModel Pet { get; set; } = new();
}

public sealed class ManualEntryModel
{
    public string Code { get; set; } = default!;
}

public sealed class OwnerModel
{
    public string? Name { get; set; }
    public string Phone { get; set; } = default!;
    public Dictionary<string, string> SocialNicks { get; set; } = [];
}

public sealed class PetModel
{
    public string Nickname { get; set; } = default!;
    public string? PhotoUrl { get; set; }
    public string? Address { get; set; }
}
