using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PT.Application.Interfaces.Services;
using PT.Application.Services;
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
    }
}
