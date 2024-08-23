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
            .HasOne<Person>(user => user.Person)
            .WithOne(person => person.User)
            .HasForeignKey<User>(user => user.PersonId);

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