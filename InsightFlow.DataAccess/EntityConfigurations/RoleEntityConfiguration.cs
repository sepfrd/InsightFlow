using System.Data;
using InsightFlow.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsightFlow.DataAccess.EntityConfigurations;

public class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
{

    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder
            .Property(role => role.Name)
            .HasColumnType(SqlDbType.NVarChar.ToString())
            .HasMaxLength(50);

        builder
            .Property(role => role.Description)
            .HasColumnType(SqlDbType.NVarChar.ToString())
            .HasMaxLength(100);
    }
}