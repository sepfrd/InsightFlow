using InsightFlow.DataAccess.Base;
using InsightFlow.DataAccess.Context;
using InsightFlow.Model.Entities;
using Sieve.Services;

namespace InsightFlow.DataAccess.Repositories;

public class QuestionRepository : BaseRepository<Question>
{
    private readonly InsightFlowDbContext _dbContext;

    public QuestionRepository(InsightFlowDbContext dbContext, ISieveProcessor sieveProcessor) : base(dbContext, sieveProcessor) =>
        _dbContext = dbContext;

    public async Task<QuestionVote?> SubmitVoteAsync(QuestionVote questionVote, CancellationToken cancellationToken = default)
    {
        var entityEntry = await _dbContext.QuestionVotes!.AddAsync(questionVote, cancellationToken);

        return entityEntry.Entity;
    }
}