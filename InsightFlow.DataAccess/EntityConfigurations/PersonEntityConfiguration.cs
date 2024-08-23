using System.Data;
using InsightFlow.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsightFlow.DataAccess.EntityConfigurations;

public class PersonEntityConfiguration : IEntityTypeConfiguration<Person>
{

    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder
            .Property(person => person.FirstName)
            .HasColumnType(SqlDbType.NVarChar.ToString())
            .HasMaxLength(100);

        builder
            .Property(person => person.LastName)
            .HasColumnType(SqlDbType.NVarChar.ToString())
            .HasMaxLength(100);
    }
}