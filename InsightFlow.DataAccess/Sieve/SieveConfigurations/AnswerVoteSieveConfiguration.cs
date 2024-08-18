using InsightFlow.Model.Entities;
using Sieve.Services;

namespace InsightFlow.DataAccess.Sieve.SieveConfigurations;

public class AnswerVoteSieveConfiguration : BaseSieveConfiguration<AnswerVote>
{
    public override void Configure(SievePropertyMapper mapper)
    {
        base.Configure(mapper);

        mapper
            .Property<AnswerVote>(entity => entity.Kind)
            .CanSort()
            .CanFilter();

        mapper
            .Property<AnswerVote>(entity => entity.AnswerId)
            .CanSort()
            .CanFilter();
    }
}