using Microsoft.AspNetCore.Components.Forms;

namespace PT.Blazor.Models;

public sealed class PetCardModel
{
    public required string Code {  get; set; }
    public string? PhoneNumber { get; set; }
    public string? PetName { get; set; }
    public string? Address { get; set; }
    public string? AvatarPreview { get; set; }
    public IBrowserFile? Avatar {  get; set; }

    public Dictionary<string, string> SocialLinks = [];
    public List<string> SelectedSocials { get; set; } = [];
}

