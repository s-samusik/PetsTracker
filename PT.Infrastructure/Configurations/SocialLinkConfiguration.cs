using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PT.Domain.Entities;

namespace PT.Infrastructure.Configurations;

public class SocialLinkConfiguration : IEntityTypeConfiguration<SocialLink>
{
    public void Configure(EntityTypeBuilder<SocialLink> builder)
    {
        builder.ToTable("sociallinks");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Type)
            .HasConversion<int>();

        builder.Property(x => x.Username)
            .HasMaxLength(64);

        builder.HasIndex(x => new { x.PetCardId, x.Type })
            .IsUnique();
    }
}

