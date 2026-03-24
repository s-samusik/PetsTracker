using Microsoft.AspNetCore.Components;
using PT.Blazor.Models;

namespace PT.Blazor.Components.Registration;

public partial class PetComponent
{
    [Parameter] public PetCardModel PetCardModel { get; set; } = default!;

    private async Task SwapPicture()
    {
        if (PetCardModel.Avatar is null)
        {
            return;
        }
        if (PetCardModel.Avatar.Size > 5242880)
        {
            return;
        }
        StateHasChanged();

        using var stream = PetCardModel.Avatar.OpenReadStream(5242880);
        using var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream);
        var fileBytes = memoryStream.ToArray();

        PetCardModel.AvatarPreview = $"data:{PetCardModel.Avatar.ContentType};base64,{Convert.ToBase64String(fileBytes)}";
    }
}