using Microsoft.EntityFrameworkCore;
using PT.Domain.Entities;

namespace PT.Infrastructure.Common;

public class PostgreSqlDbContext(DbContextOptions<PostgreSqlDbContext> options) 
    : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<PetCard> PetCards => Set<PetCard>();
    public DbSet<Code> Codes => Set<Code>();
    public DbSet<SocialLink> SocialLinks => Set<SocialLink>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgreSqlDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
