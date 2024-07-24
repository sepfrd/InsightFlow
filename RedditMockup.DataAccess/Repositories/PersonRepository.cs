using RedditMockup.DataAccess.Base;
using RedditMockup.DataAccess.Context;
using RedditMockup.Model.Entities;
using Sieve.Services;

namespace RedditMockup.DataAccess.Repositories;

public class PersonRepository : BaseRepository<Person>
{
    // [Constructor]

    public PersonRepository(RedditMockupDbContext dbContext, ISieveProcessor sieveProcessor) :
        base(dbContext, sieveProcessor)
    {
    }

    // --------------------------------------
}