using PT.Domain.Enums;

namespace PT.Application.Interfaces.Profiles;

public interface IImageProcessingProfileRegistry
{
    IImageProcessingProfile Get(MediaType type);
}
