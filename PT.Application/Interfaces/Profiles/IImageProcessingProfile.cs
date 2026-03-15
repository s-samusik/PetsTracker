using PT.Domain.Enums;
using SixLabors.ImageSharp;

namespace PT.Application.Interfaces.Profiles;

public interface IImageProcessingProfile
{
    Size TargetSize { get; }
    bool CropToSquare { get; }
    bool RemoveExif { get; }
    MediaFormat OutputFormat { get; }
}
