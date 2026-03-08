using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PT.Domain.Entities;

namespace PT.Infrastructure.Configurations;

public class PetCardConfiguration : IEntityTypeConfiguration<PetCard>
{
    public void Configure(EntityTypeBuilder<PetCard> builder)
    {
        builder.ToTable("petcards");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PetName)
            .HasMaxLength(64);

        builder.Property(x => x.PhotoUrl);

        builder.Property(x => x.State)
            .HasConversion<string>();

        builder.HasOne(x => x.Code)
            .WithOne(x => x.PetCard)
            .HasForeignKey<PetCard>(x => x.CodeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.SocialLinks)
            .WithOne(x => x.PetCard)
            .HasForeignKey(x => x.PetCardId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
