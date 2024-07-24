using RedditMockup.DataAccess.Base;
using RedditMockup.DataAccess.Context;
using RedditMockup.Model.Entities;
using Sieve.Services;

namespace RedditMockup.DataAccess.Repositories;

public class AnswerRepository : BaseRepository<Answer>
{
    // [Fields]

    private readonly RedditMockupDbContext _dbContext;

    // --------------------------------------

    // [Constructor]

    public AnswerRepository(RedditMockupDbContext dbContext, ISieveProcessor sieveProcessor) :
        base(dbContext, sieveProcessor) =>
        _dbContext = dbContext;

    // --------------------------------------

    // [Methods]

    public async Task<AnswerVote?> SubmitVoteAsync(AnswerVote answerVote, CancellationToken cancellationToken = default)
    {
        var entityEntry = await _dbContext.AnswerVotes!.AddAsync(answerVote, cancellationToken);

        return entityEntry.Entity;
    }

    // --------------------------------------

}