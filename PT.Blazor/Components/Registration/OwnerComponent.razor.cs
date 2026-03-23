using MudBlazor;

namespace PT.Blazor.Components.Registration;

public partial class OwnerComponent
{
    private string phoneNumber = null!;
    private bool isDisabled;

    private List<string> SelectedSocials { get; set; } = [];
    private Dictionary<string, string> SocialNicks { get; set; } = [];

    private void OnSelectedSocialsChanged(IEnumerable<string> values)
    {
        SelectedSocials = [.. values];

        foreach (var v in SelectedSocials)
        {
            if (!SocialNicks.ContainsKey(v))
                SocialNicks[v] = string.Empty;
        }
    }

    private static string GetSocialIcon(string social)
    {
        return social switch
        {
            "Instagram" => Icons.Custom.Brands.Instagram,
            "Telegram" => Icons.Custom.Brands.Telegram,
            "TikTok" => Icons.Custom.Brands.TikTok,
            _ => Icons.Material.Filled.Person
        };
    }
}