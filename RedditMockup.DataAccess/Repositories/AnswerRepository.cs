using RedditMockup.DataAccess.Base;
using RedditMockup.DataAccess.Context;
using RedditMockup.Model.Entities;
using Sieve.Services;

namespace RedditMockup.DataAccess.Repositories;

public class AnswerRepository : BaseRepository<Answer>
{
    private readonly RedditMockupDbContext _dbContext;

    public AnswerRepository(RedditMockupDbContext dbContext, ISieveProcessor sieveProcessor) :
        base(dbContext, sieveProcessor) =>
        _dbContext = dbContext;

    public async Task SubmitVoteAsync(AnswerVote answerVote, CancellationToken cancellationToken = default) =>
        await _dbContext.AnswerVotes!.AddAsync(answerVote, cancellationToken);
}