using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PT.Infrastructure.Common;

namespace PT.Infrastructure.Extensions;

public static class DatabaseExtensions
{
    public static void ApplyMigrations(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        
        var db = scope.ServiceProvider.GetRequiredService<PostgreSqlDbContext>();
        
        db.Database.Migrate();
    }
}
