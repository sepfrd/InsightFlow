using System.Net;
using System.Security.Claims;
using AutoMapper;
using InsightFlow.Business.Interfaces;
using InsightFlow.Common.Constants;
using InsightFlow.Common.Dtos;
using InsightFlow.Common.Dtos.CustomResponses;
using InsightFlow.Common.Dtos.Requests;
using InsightFlow.Common.Helpers;
using InsightFlow.DataAccess.Interfaces;
using InsightFlow.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Profile = InsightFlow.Model.Entities.Profile;

namespace InsightFlow.Business.Businesses;

public class UserBusiness : IUserBusiness
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IBaseRepository<User> _userRepository;

    public UserBusiness(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _userRepository = unitOfWork.UserRepository;
    }

    // TODO: Make sure this nested setup works
    public async Task<CustomResponse<UserDto>> CreateUserAsync(CreateUserRequestDto requestDto, CancellationToken cancellationToken = default)
    {
        var password = PasswordHelper.HashPassword(requestDto.Password);

        var user = new User
        {
            Person = new Person
            {
                FirstName = requestDto.FirstName,
                LastName = requestDto.LastName
            },
            Profile = new Profile
            {
                Bio = requestDto.Bio ?? string.Empty,
                ProfileImage = new ProfileImage()
            },
            Email = requestDto.Email,
            Username = requestDto.Username,
            Password = password
        };

        var createdUser = await _userRepository.CreateAsync(user, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        var userDto = _mapper.Map<UserDto>(createdUser);

        return CustomResponse<UserDto>.CreateSuccessfulResponse(userDto, null, HttpStatusCode.Created);
    }

    public async Task<PagedCustomResponse<List<User>>> GetAllUsersAsync(SieveModel sieveModel, CancellationToken cancellationToken = default)
    {
        sieveModel.Page ??= 1;
        sieveModel.PageSize ??= 10;

        var result = await _userRepository.GetAllAsync(
            sieveModel, users =>
                users
                    .Include(user => user.Person)
                    .Include(user => user.Profile),
            cancellationToken);

        var currentCount = result.Entities?.Count ?? 0;

        return PagedCustomResponse<List<User>>.CreateSuccessfulResponse(
            result.Entities,
            null,
            HttpStatusCode.OK,
            result.TotalCount,
            currentCount,
            sieveModel.Page.Value,
            sieveModel.PageSize.Value);
    }

    public async Task<CustomResponse<User>> GetUserByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(
            id,
            users => users
                .Include(user => user.Person)
                .Include(userEntity => userEntity.Profile),
            cancellationToken);

        if (user is null)
        {
            return CustomResponse<User>.CreateUnsuccessfulResponse(HttpStatusCode.NotFound);
        }

        return CustomResponse<User>.CreateSuccessfulResponse(user);
    }

    public async Task<CustomResponse<UserDto>> GetCurrentUserAsync(CancellationToken cancellationToken = default)
    {
        var userGuid = _httpContextAccessor.HttpContext?.User.FindFirstValue(ApplicationConstants.ExternalIdClaim)!;

        var user = await _userRepository.GetByGuidAsync(
            Guid.Parse(userGuid),
            users => users
                .Include(userEntity => userEntity.Person)
                .Include(user => user.Profile),
            cancellationToken);

        var userDto = _mapper.Map<UserDto>(user);

        return CustomResponse<UserDto>.CreateSuccessfulResponse(userDto);
    }

    public async Task<CustomResponse<FileContentResult>> GetCurrentUserProfileImageAsync(CancellationToken cancellationToken = default)
    {
        var userGuid = _httpContextAccessor.HttpContext?.User.FindFirstValue(ApplicationConstants.ExternalIdClaim)!;

        var user = await _userRepository.GetByGuidAsync(
            Guid.Parse(userGuid),
            users => users
                .Include(user => user.Profile)
                .ThenInclude(profile => profile!.ProfileImage),
            cancellationToken);

        var profileImage = user!.Profile!.ProfileImage!;

        if (profileImage.ImageBytes is null)
        {
            return new CustomResponse<FileContentResult>
            {
                Message = MessageConstants.NoProfileImageUploadedYetMessage,
                IsSuccess = false,
                HttpStatusCode = HttpStatusCode.NotFound
            };
        }

        var imageName = user.Username + '-' + "profile-picture";
        var contentType = "image/" + profileImage.ImageFormat;

        var profileImageFile = new FileContentResult(profileImage.ImageBytes, contentType)
        {
            FileDownloadName = imageName,
            LastModified = profileImage.LastUpdated
        };

        return CustomResponse<FileContentResult>.CreateSuccessfulResponse(profileImageFile);
    }

    public async Task<CustomResponse> AddProfileImageForCurrentUserAsync(IFormFile profileImage, CancellationToken cancellationToken = default)
    {
        var imageFileExtension = Path.GetExtension(profileImage.FileName).ToLowerInvariant().TrimStart('.');

        if (string.IsNullOrEmpty(imageFileExtension) || !ApplicationConstants.ValidProfileImageFormats.Contains(imageFileExtension))
        {
            var message = string.Format(
                MessageConstants.InvalidProfileImageFormatMessage,
                imageFileExtension,
                string.Join(", ", ApplicationConstants.ValidProfileImageFormats));

            return CustomResponse.CreateUnsuccessfulResponse(HttpStatusCode.BadRequest, message);
        }

        var userGuid = _httpContextAccessor.HttpContext?.User.FindFirstValue(ApplicationConstants.ExternalIdClaim)!;

        var user = await _unitOfWork.UserRepository.GetByGuidAsync(
            Guid.Parse(userGuid),
            users => users
                .Include(user => user.Profile)
                .ThenInclude(profile => profile!.ProfileImage),
            cancellationToken);

        await using var memoryStream = new MemoryStream();

        await profileImage.CopyToAsync(memoryStream, cancellationToken);

        var profileImageBytes = memoryStream.ToArray();

        var imageFormat = imageFileExtension == ApplicationConstants.Jpg ? ApplicationConstants.Jpeg : imageFileExtension;

        user!.Profile!.ProfileImage!.ImageBytes = profileImageBytes;
        user.Profile.ProfileImage.ImageFormat = imageFormat;

        await _unitOfWork.CommitAsync(cancellationToken);

        return CustomResponse.CreateSuccessfulResponse(MessageConstants.SuccessfulProfileImageUploadMessage);
    }
}