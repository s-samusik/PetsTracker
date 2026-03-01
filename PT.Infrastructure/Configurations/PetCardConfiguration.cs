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

        builder.Property(x => x.PetName).HasMaxLength(128);
        builder.Property(x => x.PhotoUrl);
        builder.Property(x => x.State).HasConversion<int>();

        builder.OwnsMany(x => x.SocialLinks, sl =>
        {
            sl.ToTable("petcard_sociallinks");

            sl.WithOwner().HasForeignKey("PetCardId");

            sl.Property(x => x.Type).HasConversion<int>();
            sl.Property(x => x.Value).HasMaxLength(256);
        });
    }
}
