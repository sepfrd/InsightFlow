using Microsoft.Extensions.Options;
using RedditMockup.Common.Dtos;
using RedditMockup.DataAccess.Base;
using RedditMockup.DataAccess.Context;
using RedditMockup.Model.Entities;
using Sieve.Services;

namespace RedditMockup.DataAccess.Repositories;

public class PersonRepository : BaseRepository<Person>
{
    #region [Constructor]

    public PersonRepository(RedditMockupContext context, ISieveProcessor sieveProcessor, IOptions<MongoDbSettings> mongoDbSettings) :
        base(context, sieveProcessor, mongoDbSettings)
    {
    }

    #endregion
}