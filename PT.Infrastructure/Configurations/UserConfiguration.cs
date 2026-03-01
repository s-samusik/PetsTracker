using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PT.Domain.Entities;

namespace PT.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PhoneNumber)
               .HasMaxLength(32);

        builder.HasMany(x => x.PetCards)
               .WithOne()
               .HasForeignKey("UserId")
               .OnDelete(DeleteBehavior.Cascade);
    }
}

