using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PT.Infrastructure.Entities;

namespace PT.Infrastructure.Configurations;

public class PetCardConfiguration : IEntityTypeConfiguration<PetCardEntity>
{
    public void Configure(EntityTypeBuilder<PetCardEntity> builder)
    {
        builder.ToTable("petcards");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PetName).HasMaxLength(64);
        builder.Property(x => x.PhotoUrl);
        builder.Property(x => x.Address);

        builder.Property(x => x.State)
            .HasConversion<string>();

        builder.HasOne(x => x.CodeEntity)
            .WithOne(x => x.PetCardEntity)
            .HasForeignKey<PetCardEntity>(x => x.CodeEntityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.SocialLinkEntities)
            .WithOne(x => x.PetCardEntity)
            .HasForeignKey(x => x.PetCardEntityId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
