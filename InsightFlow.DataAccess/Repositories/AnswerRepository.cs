using InsightFlow.DataAccess.Base;
using InsightFlow.DataAccess.Context;
using InsightFlow.Model.Entities;
using Sieve.Services;

namespace InsightFlow.DataAccess.Repositories;

public class AnswerRepository : BaseRepository<Answer>
{
    private readonly InsightFlowDbContext _dbContext;

    public AnswerRepository(InsightFlowDbContext dbContext, ISieveProcessor sieveProcessor) :
        base(dbContext, sieveProcessor) =>
        _dbContext = dbContext;

    public async Task SubmitVoteAsync(AnswerVote answerVote, CancellationToken cancellationToken = default) =>
        await _dbContext.AnswerVotes!.AddAsync(answerVote, cancellationToken);
}