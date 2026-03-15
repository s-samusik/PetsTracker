using PT.Application.Interfaces.Profiles;
using PT.Domain.Enums;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace PT.Application.Services;

public sealed class ImageProcessingService(IImageProcessingProfileRegistry registry)
{
    private readonly IImageProcessingProfileRegistry _registry = registry;

    public async Task<Stream> ProcessAsync(Stream input, MediaType type)
    {
        var profile = _registry.Get(type);

        input.Position = 0;

        using var image = await Image.LoadAsync(input);

        if (profile.RemoveExif)
            image.Metadata.ExifProfile = null;

        if (profile.CropToSquare)
        {
            var size = Math.Min(image.Width, image.Height);
            image.Mutate(x => x.Crop(new Rectangle(
                (image.Width - size) / 2,
                (image.Height - size) / 2,
                size,
                size)));
        }

        image.Mutate(x => x.Resize(new ResizeOptions
        {
            Mode = ResizeMode.Max,
            Size = profile.TargetSize
        }));

        var output = new MemoryStream();

        switch (profile.OutputFormat)
        {
            case MediaFormat.WebP:
                await image.SaveAsWebpAsync(output);
                break;

            case MediaFormat.Jpeg:
                await image.SaveAsJpegAsync(output);
                break;

            case MediaFormat.Png:
                await image.SaveAsPngAsync(output);
                break;

            default:
                await image.SaveAsWebpAsync(output);
                break;
        }

        output.Position = 0;

        return output;
    }
}
