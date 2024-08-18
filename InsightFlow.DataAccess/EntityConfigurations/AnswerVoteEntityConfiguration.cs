using InsightFlow.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsightFlow.DataAccess.EntityConfigurations;

public class AnswerVoteEntityConfiguration : IEntityTypeConfiguration<AnswerVote>
{
    public void Configure(EntityTypeBuilder<AnswerVote> builder)
    {
        builder
            .HasOne<Answer>(vote => vote.Answer)
            .WithMany(answer => answer.Votes)
            .HasForeignKey(vote => vote.AnswerId);
    }
}