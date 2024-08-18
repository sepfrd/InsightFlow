using InsightFlow.Model.Entities;
using Sieve.Services;

namespace InsightFlow.DataAccess.Sieve.SieveConfigurations;

public class AnswerSieveConfiguration : BaseSieveConfiguration<Answer>
{
    public override void Configure(SievePropertyMapper mapper)
    {
        base.Configure(mapper);

        mapper
            .Property<Answer>(entity => entity.Title)
            .CanSort()
            .CanFilter();

        mapper
            .Property<Answer>(entity => entity.Description)
            .CanSort()
            .CanFilter();

        mapper
            .Property<Answer>(entity => entity.QuestionId)
            .CanSort()
            .CanFilter();

        mapper
            .Property<Answer>(entity => entity.UserId)
            .CanSort()
            .CanFilter();
    }
}