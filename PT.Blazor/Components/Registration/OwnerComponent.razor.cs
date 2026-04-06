using Microsoft.AspNetCore.Components;
using MudBlazor;
using PT.Application.Services;
using PT.Blazor.Constants;
using PT.Blazor.Models;
using PT.Domain.Enums;

namespace PT.Blazor.Components.Registration;

public partial class OwnerComponent
{
    [Parameter] public PetCardModel PetCardModel { get; set; } = default!;
    [Parameter] public EventCallback<bool> OnOwnerValidChanged { get; set; } = default!;

    [Inject] public PhoneNumberService PhoneNumberService { get; set; } = default!;

    private static readonly SocialMediaType[] Socials =
    [
        SocialMediaType.Instagram,
        SocialMediaType.Telegram,
        SocialMediaType.TikTok,
        SocialMediaType.Viber
    ];

    private static readonly Dictionary<SocialMediaType, string> SocialIcons = new()
    {
        [SocialMediaType.Instagram] = Icon.Instagram,
        [SocialMediaType.Telegram] = Icon.Telegram,
        [SocialMediaType.TikTok] = Icon.TikTok,
        [SocialMediaType.Viber] = Icon.Viber,
    };

    private static readonly Dictionary<SocialMediaType, string> SocialHelpers = new()
    {
        [SocialMediaType.Instagram] = "имя пользователя без @, пример: user_123",
        [SocialMediaType.Telegram] = "имя пользователя без @, пример: user_123",
        [SocialMediaType.TikTok] = "имя пользователя с @, пример: @user_123",
        [SocialMediaType.Viber] = "номер телефона, только цифры: 375XXXXXXXXX",
    };

    private bool isNumberValid = default;

    protected override void OnInitialized()
    {
        foreach (var s in Socials)
        {
            var key = s.ToString();
            
            if (!PetCardModel.SocialLinks.ContainsKey(key))
            {
                PetCardModel.SocialLinks[key] = string.Empty;
            }
        }
    }

    private static string GetSocialIcon(SocialMediaType type)
        => SocialIcons.TryGetValue(type, out var icon) ? icon : Icons.Material.Filled.Person;

    private static string GetHelperText(SocialMediaType type)
        => SocialHelpers.TryGetValue(type, out var text) ? text : string.Empty;

    private void HandleTextChanged(string text)
    {
        var number = "375" + text.Replace(" ", string.Empty);

        isNumberValid = PhoneNumberService.IsValid(number);

        OnOwnerValidChanged.InvokeAsync(isNumberValid);
    }
}