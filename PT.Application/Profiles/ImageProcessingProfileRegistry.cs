using PT.Application.Interfaces.Profiles;
using PT.Domain.Enums;

namespace PT.Application.Profiles;

public sealed class ImageProcessingProfileRegistry : IImageProcessingProfileRegistry
{
    private readonly Dictionary<MediaType, IImageProcessingProfile> _profiles;

    public ImageProcessingProfileRegistry()
    {
        _profiles = new Dictionary<MediaType, IImageProcessingProfile>
        {
            { MediaType.Avatar, new AvatarProfile() }
        };
    }

    public IImageProcessingProfile Get(MediaType type)
    {
        return _profiles.TryGetValue(type, out var profile) 
            ? profile 
            : throw new Exception($"Profile not found for '{type}'");
    }
}
