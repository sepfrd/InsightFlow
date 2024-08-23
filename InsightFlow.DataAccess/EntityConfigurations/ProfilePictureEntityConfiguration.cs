using InsightFlow.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsightFlow.DataAccess.EntityConfigurations;

public class ProfilePictureEntityConfiguration : IEntityTypeConfiguration<ProfilePicture>
{

    public void Configure(EntityTypeBuilder<ProfilePicture> builder)
    {
        builder
            .HasOne<Profile>(entity => entity.Profile)
            .WithOne(profile => profile.ProfilePicture)
            .HasForeignKey<ProfilePicture>(entity => entity.ProfileId);
    }
}