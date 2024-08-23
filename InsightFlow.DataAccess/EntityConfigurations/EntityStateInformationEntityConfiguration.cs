using System.Data;
using InsightFlow.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsightFlow.DataAccess.EntityConfigurations;

public class EntityStateInformationEntityConfiguration : IEntityTypeConfiguration<EntityStateInformation>
{

    public void Configure(EntityTypeBuilder<EntityStateInformation> builder)
    {
        builder
            .Property(entityStateInformation => entityStateInformation.Name)
            .HasColumnType(SqlDbType.NVarChar.ToString())
            .HasMaxLength(50);

        builder
            .Property(entityStateInformation => entityStateInformation.Description)
            .HasColumnType(SqlDbType.NVarChar.ToString())
            .HasMaxLength(100);
    }
}