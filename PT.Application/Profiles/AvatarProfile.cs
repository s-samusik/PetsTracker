using PT.Application.Interfaces.Profiles;
using PT.Domain.Enums;
using SixLabors.ImageSharp;

namespace PT.Application.Profiles;

public sealed class AvatarProfile : IImageProcessingProfile
{
    public Size TargetSize => new(256, 256);
    public bool CropToSquare => true;
    public bool RemoveExif => true;
    public MediaFormat OutputFormat => MediaFormat.WebP;
}

