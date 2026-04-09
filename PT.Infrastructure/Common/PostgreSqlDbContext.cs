using Microsoft.EntityFrameworkCore;
using PT.Infrastructure.Entities;

namespace PT.Infrastructure.Common;

public class PostgreSqlDbContext(DbContextOptions<PostgreSqlDbContext> options) 
    : DbContext(options)
{
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<PetCardEntity> PetCards => Set<PetCardEntity>();
    public DbSet<CodeEntity> Codes => Set<CodeEntity>();
    public DbSet<SocialLinkEntity> SocialLinks => Set<SocialLinkEntity>();
    public DbSet<PrivacyPolicyEntity> PrivacyPolicies => Set<PrivacyPolicyEntity>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgreSqlDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
