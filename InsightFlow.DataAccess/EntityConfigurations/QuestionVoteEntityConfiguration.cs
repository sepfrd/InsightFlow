using InsightFlow.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsightFlow.DataAccess.EntityConfigurations;

public class QuestionVoteEntityConfiguration : IEntityTypeConfiguration<QuestionVote>
{
    public void Configure(EntityTypeBuilder<QuestionVote> builder)
    {
        builder
            .HasOne<Question>(vote => vote.Question)
            .WithMany(question => question.Votes)
            .HasForeignKey(vote => vote.QuestionId);
    }
}