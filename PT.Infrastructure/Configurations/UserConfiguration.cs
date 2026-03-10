using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PT.Infrastructure.Entities;

namespace PT.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(64)
            .IsRequired();

        builder.HasMany(x => x.PetCardEntities)
            .WithOne(x => x.UserEntity)
            .HasForeignKey(x => x.UserEntityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

