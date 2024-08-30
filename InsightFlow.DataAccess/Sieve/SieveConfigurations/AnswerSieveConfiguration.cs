using InsightFlow.Model.Entities;
using Sieve.Services;

namespace InsightFlow.DataAccess.Sieve.SieveConfigurations;

public class AnswerSieveConfiguration : BaseSieveConfiguration<Answer>
{
    public override void Configure(SievePropertyMapper mapper)
    {
        base.Configure(mapper);
        
        mapper
            .Property<Answer>(answer => answer.Body)
            .CanSort()
            .CanFilter();

        mapper
            .Property<Answer>(answer => answer.QuestionId)
            .CanSort()
            .CanFilter();

        mapper
            .Property<Answer>(answer => answer.UserId)
            .CanSort()
            .CanFilter();
        
        mapper
            .Property<Answer>(answer => answer.Question!.User!.Username)
            .CanSort()
            .CanFilter();
        
        mapper
            .Property<Answer>(answer => answer.Question!.User!.Email)
            .CanSort()
            .CanFilter();
        
        mapper
            .Property<Answer>(answer => answer.Question!.User!.FirstName)
            .CanSort()
            .CanFilter();
        
        mapper
            .Property<Answer>(answer => answer.Question!.User!.LastName)
            .CanSort()
            .CanFilter();
        
        mapper
            .Property<Answer>(answer => answer.Question!.Title)
            .CanSort()
            .CanFilter();
        
        mapper
            .Property<Answer>(answer => answer.Question!.Body)
            .CanSort()
            .CanFilter();
    }
}