using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PT.Infrastructure.Entities;

namespace PT.Infrastructure.Configurations;

public class PrivacyPolicyConfiguration : IEntityTypeConfiguration<PrivacyPolicyEntity>
{
    public void Configure(EntityTypeBuilder<PrivacyPolicyEntity> builder)
    {
        builder.ToTable("privacy_policy");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.UserType, x.Version })
            .IsUnique(true);

        builder.Property(x => x.UserType)
            .HasConversion<string>();

        builder.Property(x => x.Value);
        builder.Property(x => x.Version);
    }
}
