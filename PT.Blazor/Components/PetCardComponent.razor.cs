using Microsoft.AspNetCore.Components;
using MudBlazor;
using PT.Application.Interfaces.Services;
using PT.Domain.Enums;
using PT.Domain.Models;

namespace PT.Blazor.Components;

public partial class PetCardComponent
{
    [Parameter] public string Code { get; set; } = default!;

    [Inject] private IPetCardService PetCardService { get; set; } = default!;
    [Inject] private NavigationManager Nav {  get; set; } = default!;

    private PetCard? PetCard;

    private readonly Dictionary<SocialMediaType, (string Icon, string UrlTemplate)> socialMap =
        new()
        {
            { SocialMediaType.Instagram, (Icons.Custom.Brands.Instagram, "https://instagram.com/{0}") },
            { SocialMediaType.Telegram,  (Icons.Custom.Brands.Telegram,  "https://t.me/{0}") },
            { SocialMediaType.TikTok,    (Icons.Custom.Brands.TikTok,    "https://www.tiktok.com/@{0}") }
        };

    protected override async Task OnInitializedAsync()
    {
        PetCard = await PetCardService.GetByCodeAsync(Code);
    }

    private void OpenSocial(SocialLink link)
    {
        if (socialMap.TryGetValue(link.Type, out var data) && !string.IsNullOrWhiteSpace(link.Username))
        {
            var url = string.Format(data.UrlTemplate, link.Username);
            
            Nav.NavigateTo(url, forceLoad: true);
        }
    }
}