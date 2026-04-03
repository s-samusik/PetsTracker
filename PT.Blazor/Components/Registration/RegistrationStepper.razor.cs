using Microsoft.AspNetCore.Components;
using PT.Application.Dtos;
using PT.Application.Interfaces.Services;
using PT.Blazor.Models;
using PT.Domain.Enums;

namespace PT.Blazor.Components.Registration;

public partial class RegistrationStepper : ComponentBase
{
    [Parameter] public PetCardModel PetCardModel { get; set; } = default!;
    [Parameter] public EventCallback OnFinish { get; set; } = default!;

    [Inject] private NavigationManager Nav { get; set; } = default!;
    [Inject] private IPetCardService PetCardService { get; set; } = default!;

    private bool ownerValid = true;
    private bool petValid = true;
    private int index;
    private bool completed;
    private bool loading;

    private async Task OnRegisterClick()
    {
        var socialLinks = PetCardModel.SocialLinks
            .Select(kvp =>
                Enum.TryParse<SocialMediaType>(kvp.Key, true, out var type)
                    ? new SocialLinkDto(type, kvp.Value)
                    : null)
            .Where(x => x is not null && !string.IsNullOrWhiteSpace(x.Username))
            .ToList()!;

        var dto = new RegisterPetCardDto(
            Code: PetCardModel.Code,
            PhoneNumber: PetCardModel.PhoneNumber,
            PetName: PetCardModel.PetName,
            Address: PetCardModel.Address,
            Info: PetCardModel.Info,
            SocialLinks: socialLinks
        );

        Guid petCardId;

        loading = true;

        try
        {
            var card = await PetCardService.RegisterAsync(dto);
            petCardId = card.Id;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка регистрации карточки: {ex}");
            throw;  
        }

        try
        {
            if (PetCardModel.AvatarBytes is not null &&
                PetCardModel.AvatarContentType is not null)
            {
                await using var ms = new MemoryStream(PetCardModel.AvatarBytes);

                await PetCardService.UploadAvatarAsync(
                    petCardId,
                    ms,
                    PetCardModel.AvatarContentType
                );
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Аватарка НЕ загружена: {ex}");
        }

        loading = false;
        completed = true;
        await OnFinish.InvokeAsync();
    }

    private void GoToStart()
    {
        Nav.NavigateTo($"/", forceLoad: true);
    }

    private bool CanGoNext =>
        index switch
        {
            0 => ownerValid,
            1 => petValid,
            2 => true,
            _ => false
        };
}