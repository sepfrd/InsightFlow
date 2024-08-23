using InsightFlow.Common.Dtos;
using InsightFlow.Common.Dtos.CustomResponses;
using InsightFlow.Common.Dtos.Requests;
using InsightFlow.Model.Entities;
using Sieve.Models;

namespace InsightFlow.Business.Contracts;

public interface IUserBusiness
{
    Task<CustomResponse<UserDto>> CreateUserAsync(CreateUserRequestDto requestDto, CancellationToken cancellationToken = default);

    Task<PagedCustomResponse<List<User>>> GetAllUsersAsync(SieveModel sieveModel, CancellationToken cancellationToken = default);

    Task<CustomResponse<User>> GetUserByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<CustomResponse<UserDto>> GetCurrentUserAsync(CancellationToken cancellationToken = default);
}