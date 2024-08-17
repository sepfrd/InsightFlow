using InsightFlow.Common.Dtos.CustomResponses;
using Sieve.Models;

namespace InsightFlow.Business.Contracts;

public interface IBaseBusiness<TDto>
{
    Task<CustomResponse<TDto>> CreateAsync(TDto dto, CancellationToken cancellationToken = default);

    Task<CustomResponse<TDto>> GetByGuidAsync(Guid guid, CancellationToken cancellationToken = default);

    Task<PagedCustomResponse<List<TDto>>> GetAllAsync(SieveModel sieveModel, CancellationToken cancellationToken = default);

    Task<CustomResponse<TDto>> UpdateAsync(TDto dto, CancellationToken cancellationToken = default);

    Task<CustomResponse<TDto>> DeleteByGuidAsync(Guid guid, CancellationToken cancellationToken = default);
}