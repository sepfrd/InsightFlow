using System.Data;
using InsightFlow.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsightFlow.DataAccess.EntityConfigurations;

public class QuestionEntityConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder
            .HasOne<User>(question => question.User)
            .WithMany(user => user.Questions)
            .HasForeignKey(question => question.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .Property(question => question.Title)
            .HasColumnType(SqlDbType.NVarChar.ToString())
            .HasMaxLength(200);

        builder
            .Property(question => question.Body)
            .HasColumnType(SqlDbType.NVarChar.ToString())
            .HasMaxLength(2000);
    }
}