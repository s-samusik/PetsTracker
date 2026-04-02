using Microsoft.AspNetCore.Components;
using MudBlazor;
using PT.Blazor.Constants;
using PT.Blazor.Models;

namespace PT.Blazor.Components.Registration;

public partial class OwnerComponent
{
    [Parameter] public PetCardModel PetCardModel { get; set; } = default!;

    private readonly string[] Socials = { "Instagram", "Telegram", "Viber", "TikTok" };

    protected override void OnInitialized()
    {
        foreach (var s in Socials)
        {
            if (!PetCardModel.SocialLinks.ContainsKey(s))
            {
                PetCardModel.SocialLinks[s] = s == "Viber"
                    ? DigitsOnly(PetCardModel.PhoneNumber)
                    : string.Empty;
            }
        }
    }

    private static string DigitsOnly(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        return new string([.. input.Where(char.IsDigit)]);
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