using Microsoft.AspNetCore.Components;
using PT.Blazor.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;

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

        if (PetCardModel.Avatar.Size > 5 * 1024 * 1024)
        {
            return;
        }

        using var stream = PetCardModel.Avatar.OpenReadStream(5 * 1024 * 1024);
        using var memoryStream = new MemoryStream();
        
        await stream.CopyToAsync(memoryStream);
        var fileBytes = memoryStream.ToArray();

        using var image = Image.Load(fileBytes);

        image.Mutate(x => x.AutoOrient());

        using var outStream = new MemoryStream();

        if (PetCardModel.Avatar.ContentType.Contains("png", StringComparison.OrdinalIgnoreCase))
        {
            image.Save(outStream, new PngEncoder());
            PetCardModel.AvatarContentType = "image/png";
        }
        else
        {
            image.Save(outStream, new JpegEncoder
            {
                Quality = 90
            });

            PetCardModel.AvatarContentType = "image/jpeg";
        }

        var fixedBytes = outStream.ToArray();

        PetCardModel.AvatarBytes = fixedBytes;
        PetCardModel.AvatarPreview = $"data:{PetCardModel.AvatarContentType};base64,{Convert.ToBase64String(fixedBytes)}";

        StateHasChanged();
    }

    private void RemoveAvatar()
    {
        PetCardModel.AvatarPreview = null;
        PetCardModel.Avatar = null;
    }
}