using InsightFlow.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsightFlow.DataAccess.EntityConfigurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(x => x.Username).IsUnique();

        builder
            .HasOne<Person>(user => user.Person)
            .WithOne(person => person.User)
            .HasForeignKey<User>(user => user.PersonId);
    }
}