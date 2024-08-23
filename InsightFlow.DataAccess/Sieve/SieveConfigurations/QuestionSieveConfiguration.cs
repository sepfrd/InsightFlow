using InsightFlow.Model.Entities;
using Sieve.Services;

namespace InsightFlow.DataAccess.Sieve.SieveConfigurations;

public class QuestionSieveConfiguration : BaseSieveConfiguration<Question>
{
    public override void Configure(SievePropertyMapper mapper)
    {
        base.Configure(mapper);

        mapper
            .Property<Question>(entity => entity.Title)
            .CanSort()
            .CanFilter();

        mapper
            .Property<Question>(entity => entity.Body)
            .CanSort()
            .CanFilter();

        mapper
            .Property<Question>(entity => entity.UserId)
            .CanSort()
            .CanFilter();
    }
}