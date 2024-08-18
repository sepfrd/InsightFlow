using InsightFlow.Model.Entities;
using Sieve.Services;

namespace InsightFlow.DataAccess.Sieve.SieveConfigurations;

public class QuestionVoteSieveConfiguration : BaseSieveConfiguration<QuestionVote>
{
    public override void Configure(SievePropertyMapper mapper)
    {
        base.Configure(mapper);

        mapper
            .Property<QuestionVote>(entity => entity.Kind)
            .CanSort()
            .CanFilter();

        mapper
            .Property<QuestionVote>(entity => entity.QuestionId)
            .CanSort()
            .CanFilter();
    }
}