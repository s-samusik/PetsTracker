using Microsoft.AspNetCore.Components;
using MudBlazor;
using PT.Blazor.Constants;
using PT.Blazor.Models;

namespace PT.Blazor.Components.Registration;

public partial class OwnerComponent
{
    [Parameter] public PetCardModel PetCardModel { get; set; } = default!;

    private readonly string[] Socials = ["Instagram", "Telegram", "Viber", "TikTok"];

    protected override void OnInitialized()
    {
        foreach (var s in Socials)
        {
            PetCardModel.SocialLinks.TryAdd(s, string.Empty);
        }
    }

    private static string GetSocialIcon(string social)
    {
        return social switch
        {
            "Instagram" => Icon.Instagram,
            "Telegram" => Icon.Telegram,
            "TikTok" => Icon.TikTok,
            "Viber" => Icon.Viber,
            _ => Icons.Material.Filled.Person
        };
    }
}