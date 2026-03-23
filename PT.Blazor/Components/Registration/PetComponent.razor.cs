using Microsoft.AspNetCore.Components.Forms;

namespace PT.Blazor.Components.Registration;

public partial class PetComponent
{
    private string petname = null!;
    private string city = null!;

    private IBrowserFile? uploadedAvatar;

    private string avatarPreview = string.Empty;

    protected override void OnInitialized()
    {
        ResetPicture();
    }

    private void ResetPicture()
    {
        avatarPreview = string.Empty;
        StateHasChanged();
    }

    private async Task SwapPicture()
    {
        if (uploadedAvatar is null)
        {
            return;
        }
        if (uploadedAvatar.Size > 5242880)
        {
            return;
        }
        StateHasChanged();

        using var stream = uploadedAvatar.OpenReadStream(5242880);
        using var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream);
        var fileBytes = memoryStream.ToArray();

        avatarPreview = $"data:{uploadedAvatar.ContentType};base64,{Convert.ToBase64String(fileBytes)}";
        uploadedAvatar = null;
    }
}