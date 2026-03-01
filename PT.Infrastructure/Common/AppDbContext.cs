using Microsoft.EntityFrameworkCore;
using PT.Domain.Entities;

namespace PT.Infrastructure.Common;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<PetCard> PetCards => Set<PetCard>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
