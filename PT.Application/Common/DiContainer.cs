using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using PT.Application.Interfaces.Profiles;
using PT.Application.Interfaces.Services;
using PT.Application.Interfaces.Validators;
using PT.Application.Profiles;
using PT.Application.Services;
using PT.Application.Validators.PhoneNumberValidators;
using PT.Domain.CodeFormats;

namespace PT.Application.Common;

public static class DiContainer
{
    public static void AddFormats(this IServiceCollection services)
    {
        services.TryAddScoped<ICodeFormat, AA11AACodeFormat>();
    }

    public static void AddApplication(this IServiceCollection services)
    {
        services.TryAddScoped<ICodeService, CodeService>();
        services.TryAddScoped<IUserService, UserService>();
        services.TryAddScoped<IPetCardService, PetCardService>();
    }

    public static void AddValidarors(this IServiceCollection services)
    {
        services.TryAddSingleton<PhoneNumberValidatorFactory>();
        services.TryAddSingleton<PhoneNumberService>();
        services.TryAddSingleton<IPhoneNumberValidator, BelarusPhoneNumberValidator>();
    }

    public static void AddImageProcessing(this IServiceCollection services)
    {
        services.TryAddSingleton<IImageProcessingProfileRegistry, ImageProcessingProfileRegistry>();
        services.TryAddSingleton<ImageProcessingService>();
    }

    public static IServiceCollection AddAwsStorage(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<AwsS3Options>(config.GetSection(AwsS3Options.Name));

        services.AddSingleton<IAmazonS3>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<AwsS3Options>>().Value;

            var config = new AmazonS3Config
            {
                ServiceURL = options.Endpoint,
                ForcePathStyle = true
            };

            return new AmazonS3Client(
                options.AccessKey,
                options.SecretKey,
                config);
        });

        services.AddScoped<IFileStorageService, S3HosterStorageService>();

        return services;
    }
}
