using InsightFlow.Common.Dtos;
using InsightFlow.Common.Dtos.CustomResponses;
using InsightFlow.Common.Dtos.Requests;
using InsightFlow.Model.Entities;
using InsightFlow.Model.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace InsightFlow.Business.Interfaces;

public interface IUserBusiness
{
    Task<CustomResponse<UserDto>> CreateUserAsync(CreateUserRequestDto requestDto, CancellationToken cancellationToken = default);

    Task<CustomResponse<User>> CreateAdminAsync(CreateUserRequestDto requestDto, CancellationToken cancellationToken = default);

    Task<PagedCustomResponse<List<User>>> GetAllUsersAsync(SieveModel sieveModel, CancellationToken cancellationToken = default);

    Task<CustomResponse<User>> GetUserByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<CustomResponse<UserWithBioDto>> GetCurrentUserAsync(CancellationToken cancellationToken = default);

    Task<CustomResponse<FileContentResult>> GetCurrentUserProfileImageAsync(CancellationToken cancellationToken = default);

    Task<CustomResponse> AddProfileImageForCurrentUserAsync(IFormFile profileImage, CancellationToken cancellationToken = default);

    Task<CustomResponse<User>> UpdateUserStateAsync(int userId, BaseEntityState newState, CancellationToken cancellationToken = default);

    Task<CustomResponse<UserWithBioDto>> UpdateCurrentUserAsync(UpdateUserRequestDto requestDto, CancellationToken cancellationToken = default);

    Task<CustomResponse> ChangeCurrentUserPasswordAsync(ChangePasswordRequestDto requestDto, CancellationToken cancellationToken = default);

    Task<CustomResponse> SoftDeleteCurrentUserAsync(SoftDeleteCurrentUserRequestDto requestDto, CancellationToken cancellationToken = default);

    Task<CustomResponse> HardDeleteUserByIdAsync(int userId, CancellationToken cancellationToken = default);
}