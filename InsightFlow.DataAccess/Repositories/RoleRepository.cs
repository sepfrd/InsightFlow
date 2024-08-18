using InsightFlow.DataAccess.Base;
using InsightFlow.Model.Entities;
using Sieve.Services;

namespace InsightFlow.DataAccess.Repositories;

public class RoleRepository : BaseRepository<Role>
{
    public RoleRepository(InsightFlowDbContext dbContext, ISieveProcessor sieveProcessor) :
        base(dbContext, sieveProcessor)
    {
    }
}