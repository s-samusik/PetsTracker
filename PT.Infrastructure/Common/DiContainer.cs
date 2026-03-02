using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PT.Infrastructure.Common;

public static class DiContainer
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var appConnectionString = configuration.GetConnectionString("PostgreDB");

        services.AddDbContext<PostgreSqlDbContext>(opt =>
        {
            opt.UseNpgsql(appConnectionString, sql =>
            {
                sql.MigrationsAssembly(typeof(PostgreSqlDbContext).Assembly.FullName);
            });
        });

        //services.TryAddScoped<IRepository, Repository>();
    }
}
