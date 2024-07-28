using RedditMockup.DataAccess.Base;
using RedditMockup.DataAccess.Context;
using RedditMockup.Model.Entities;
using Sieve.Services;

namespace RedditMockup.DataAccess.Repositories;

public class QuestionRepository : BaseRepository<Question>
{
    private readonly RedditMockupDbContext _dbContext;

    public QuestionRepository(RedditMockupDbContext dbContext, ISieveProcessor sieveProcessor) : base(dbContext, sieveProcessor) =>
        _dbContext = dbContext;

    public async Task<QuestionVote?> SubmitVoteAsync(QuestionVote questionVote, CancellationToken cancellationToken = default)
    {
        var entityEntry = await _dbContext.QuestionVotes!.AddAsync(questionVote, cancellationToken);

        return entityEntry.Entity;
    }
}