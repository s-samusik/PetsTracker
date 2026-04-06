using Microsoft.AspNetCore.Components;
using PT.Application.Interfaces.Services;
using PT.Blazor.Constants;
using PT.Domain.Enums;
using PT.Domain.Models;

namespace PT.Blazor.Components;

public partial class PetCardComponent
{
    [Parameter] public string Code { get; set; } = default!;

    [Inject] private IPetCardService PetCardService { get; set; } = default!;
    [Inject] private IUserService UserService { get; set; } = default!;
    [Inject] private NavigationManager Nav { get; set; } = default!;

    private PetCard? PetCard;
    private User? User;

    private readonly Dictionary<SocialMediaType, (string Icon, string UrlTemplate)> socialMap =
        new()
        {
            { SocialMediaType.Instagram, (Icon.Instagram, "https://instagram.com/{0}") },
            { SocialMediaType.Telegram,  (Icon.Telegram, "https://t.me/{0}") },
            { SocialMediaType.TikTok,    (Icon.TikTok, "https://www.tiktok.com/{0}") },
            { SocialMediaType.Viber,     (Icon.Viber, "viber://add?number={0}")}
        };

    protected override async Task OnInitializedAsync()
    {
        PetCard = await PetCardService.GetByCodeAsync(Code);
        User = await UserService.GetByIdAsync(PetCard?.UserId ?? Guid.Empty);
    }

    private void CallOwner()
    {
        Nav.NavigateTo($"tel:{User?.PhoneNumber ?? string.Empty}");
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