using InsightFlow.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsightFlow.DataAccess.EntityConfigurations;

public class ProfileEntityConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder
            .HasOne<User>(profile => profile.User)
            .WithOne(user => user.Profile)
            .HasForeignKey<Profile>(profile => profile.UserId);
    }
}