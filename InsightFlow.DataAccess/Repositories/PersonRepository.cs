using InsightFlow.DataAccess.Base;
using InsightFlow.DataAccess.Context;
using InsightFlow.Model.Entities;
using Sieve.Services;

namespace InsightFlow.DataAccess.Repositories;

public class PersonRepository : BaseRepository<Person>
{
    public PersonRepository(InsightFlowDbContext dbContext, ISieveProcessor sieveProcessor) :
        base(dbContext, sieveProcessor)
    {
    }
}