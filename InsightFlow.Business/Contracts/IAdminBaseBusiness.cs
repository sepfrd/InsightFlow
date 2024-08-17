using InsightFlow.Common.Dtos.CustomResponses;
using Sieve.Models;

namespace InsightFlow.Business.Contracts;

public interface IAdminBaseBusiness<TEntity, in TDto>
{
    Task<CustomResponse<TEntity?>> CreateAsync(TDto answerDto, CancellationToken cancellationToken = default);

    Task<CustomResponse<TEntity?>> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<CustomResponse<TEntity?>> GetByGuidAsync(Guid guid, CancellationToken cancellationToken = default);

    Task<PagedCustomResponse<List<TEntity>?>> GetAllAsync(SieveModel sieveModel, CancellationToken cancellationToken = default);

    Task<CustomResponse<TEntity?>> DeleteByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<CustomResponse<TEntity?>> DeleteByGuidAsync(Guid guid, CancellationToken cancellationToken = default);

    Task<CustomResponse<TEntity?>> UpdateAsync(TDto dto, CancellationToken cancellationToken = default);
}