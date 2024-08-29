using System.Data;
using InsightFlow.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsightFlow.DataAccess.EntityConfigurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(user => user.Username).IsUnique();

        builder
            .Property(user => user.FirstName)
            .HasColumnType(SqlDbType.NVarChar.ToString())
            .HasMaxLength(100);

        builder
            .Property(user => user.LastName)
            .HasColumnType(SqlDbType.NVarChar.ToString())
            .HasMaxLength(100);

        builder
            .Property(user => user.Username)
            .HasColumnType(SqlDbType.NVarChar.ToString())
            .HasMaxLength(32);

        builder
            .Property(user => user.Password)
            .HasColumnType(SqlDbType.NVarChar.ToString())
            .HasMaxLength(500);

        builder
            .Property(user => user.Email)
            .HasColumnType(SqlDbType.NVarChar.ToString())
            .HasMaxLength(320);
    }
}