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
    }
}