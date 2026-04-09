using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PT.Infrastructure.Entities;

namespace PT.Infrastructure.Configurations;

public class SocialLinkConfiguration : IEntityTypeConfiguration<SocialLinkEntity>
{
    public void Configure(EntityTypeBuilder<SocialLinkEntity> builder)
    {
        builder.ToTable("social_links");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Type)
            .HasConversion<string>();

        builder.Property(x => x.Username)
            .HasMaxLength(64);

        builder.HasIndex(x => new { x.PetCardEntityId, x.Type })
            .IsUnique();
    }
}
