using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PT.Infrastructure.Entities;

namespace PT.Infrastructure.Configurations;

public class CodeConfiguration : IEntityTypeConfiguration<CodeEntity>
{
    public void Configure(EntityTypeBuilder<CodeEntity> builder)
    {
        builder.ToTable("codes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Value)
            .HasMaxLength(64)
            .IsRequired();

        builder.HasIndex(x => x.Value)
            .IsUnique();

        builder.Property(x => x.State)
            .HasConversion<string>();
    }
}
