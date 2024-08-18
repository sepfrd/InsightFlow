using InsightFlow.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsightFlow.DataAccess.EntityConfigurations;

public class AnswerEntityConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder
            .HasOne<User>(answer => answer.User)
            .WithMany(user => user.Answers)
            .HasForeignKey(answer => answer.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<Question>(answer => answer.Question)
            .WithMany(question => question.Answers)
            .HasForeignKey(answer => answer.QuestionId);
    }
}