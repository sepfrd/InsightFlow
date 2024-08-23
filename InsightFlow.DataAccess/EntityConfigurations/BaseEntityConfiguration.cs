using InsightFlow.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsightFlow.DataAccess.EntityConfigurations;

public class BaseEntityConfiguration : IEntityTypeConfiguration<BaseEntity>
{

    public void Configure(EntityTypeBuilder<BaseEntity> builder)
    {
        builder.HasKey(entity => entity.Id);

        builder
            .HasIndex(entity => entity.Guid)
            .IsUnique();
    }
}