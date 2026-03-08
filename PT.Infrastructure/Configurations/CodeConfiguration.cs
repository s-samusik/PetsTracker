using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PT.Domain.Entities;

namespace PT.Infrastructure.Configurations;

public class CodeConfiguration : IEntityTypeConfiguration<Code>
{
    public void Configure(EntityTypeBuilder<Code> builder)
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

