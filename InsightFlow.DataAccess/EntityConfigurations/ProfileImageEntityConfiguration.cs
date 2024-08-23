using System.Data;
using InsightFlow.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsightFlow.DataAccess.EntityConfigurations;

public class ProfileImageEntityConfiguration : IEntityTypeConfiguration<ProfileImage>
{

    public void Configure(EntityTypeBuilder<ProfileImage> builder)
    {
        builder
            .HasOne<Profile>(entity => entity.Profile)
            .WithOne(profile => profile.ProfileImage)
            .HasForeignKey<ProfileImage>(entity => entity.ProfileId);

        builder
            .Property(entity => entity.ImageFormat)
            .HasColumnType(SqlDbType.VarChar.ToString())
            .HasMaxLength(10);
    }
}