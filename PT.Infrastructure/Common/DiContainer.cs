using Microsoft.Extensions.DependencyInjection;

namespace PT.Infrastructure.Common
{
    public static class DiContainer
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            //services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
