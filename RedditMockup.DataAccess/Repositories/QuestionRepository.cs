using RedditMockup.DataAccess.Base;
using RedditMockup.DataAccess.Context;
using RedditMockup.Model.Entities;
using Sieve.Services;

namespace RedditMockup.DataAccess.Repositories;

public class QuestionRepository : BaseRepository<Question>
{
    // [Fields]

    private readonly RedditMockupDbContext _dbContext;

    // --------------------------------------

    // [Constructor]

    public QuestionRepository(RedditMockupDbContext dbContext, ISieveProcessor sieveProcessor) : base(dbContext, sieveProcessor) =>
        _dbContext = dbContext;

    // --------------------------------------

    // [Methods]

    public async Task<QuestionVote?> SubmitVoteAsync(QuestionVote questionVote, CancellationToken cancellationToken = default)
    {
        var entityEntry = await _dbContext.QuestionVotes!.AddAsync(questionVote, cancellationToken);

        return entityEntry.Entity;
    }

    // --------------------------------------
}