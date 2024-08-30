using InsightFlow.Model.Entities;
using Sieve.Services;

namespace InsightFlow.DataAccess.Sieve.SieveConfigurations;

public class QuestionSieveConfiguration : BaseSieveConfiguration<Question>
{
    public override void Configure(SievePropertyMapper mapper)
    {
        base.Configure(mapper);

        mapper
            .Property<Question>(question => question.Title)
            .CanSort()
            .CanFilter();

        mapper
            .Property<Question>(question => question.Body)
            .CanSort()
            .CanFilter();

        mapper
            .Property<Question>(question => question.UserId)
            .CanSort()
            .CanFilter();

        mapper
            .Property<Question>(question => question.User!.Username)
            .CanSort()
            .CanFilter();

        mapper
            .Property<Question>(question => question.User!.Email)
            .CanSort()
            .CanFilter();

        mapper
            .Property<Question>(question => question.User!.FirstName)
            .CanSort()
            .CanFilter();

        mapper
            .Property<Question>(question => question.User!.LastName)
            .CanSort()
            .CanFilter();
    }
}