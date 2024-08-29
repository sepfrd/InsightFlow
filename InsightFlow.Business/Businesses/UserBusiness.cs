using System.Net;
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
using Microsoft.IdentityModel.Tokens;
using Sieve.Models;
using Profile = InsightFlow.Model.Entities.Profile;

namespace InsightFlow.Business.Businesses;

public class UserBusiness : IUserBusiness
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAuthBusiness _authBusiness;
    private readonly IBaseRepository<User> _userRepository;

    public UserBusiness(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IAuthBusiness authBusiness)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _authBusiness = authBusiness;
        _userRepository = unitOfWork.UserRepository;
    }

    // TODO: Make sure this nested setup works
    public async Task<CustomResponse<UserDto>> CreateUserAsync(CreateUserRequestDto requestDto, CancellationToken cancellationToken = default)
    {
        var isUsernameUnique = await IsUsernameUnique(requestDto.Username, cancellationToken);

        if (!isUsernameUnique)
        {
            var message = string.Format(
                MessageConstants.PropertyNotUniqueMessage,
                requestDto.Username,
                nameof(User.Username).ToLowerInvariant());

            return CustomResponse<UserDto>
                .CreateUnsuccessfulResponse(
                    HttpStatusCode.BadRequest,
                    message);
        }

        var isEmailUnique = await IsEmailUnique(requestDto.Email, cancellationToken);

        if (!isEmailUnique)
        {
            var message = string.Format(
                MessageConstants.PropertyNotUniqueMessage,
                requestDto.Email,
                nameof(User.Email).ToLowerInvariant());

            return CustomResponse<UserDto>
                .CreateUnsuccessfulResponse(
                    HttpStatusCode.BadRequest,
                    message);
        }

        var password = PasswordHelper.HashPassword(requestDto.Password);

        var user = new User
        {
            Profile = new Profile
            {
                Bio = requestDto.Bio ?? string.Empty,
                ProfileImage = new ProfileImage()
            },
            Email = requestDto.Email,
            Username = requestDto.Username,
            Password = password,
            FirstName = requestDto.FirstName,
            LastName = requestDto.LastName
        };

        var createdUser = await _userRepository.CreateAsync(user, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        var userDto = _mapper.Map<UserDto>(createdUser);

        return CustomResponse<UserDto>.CreateSuccessfulResponse(userDto, null, HttpStatusCode.Created);
    }

    public async Task<PagedCustomResponse<List<User>>> GetAllUsersAsync(SieveModel sieveModel, CancellationToken cancellationToken = default)
    {
        sieveModel.Page ??= 1;
        sieveModel.PageSize ??= ApplicationConstants.MinimumPageSize;

        var result = await _userRepository.GetAllAsync(
            sieveModel, users => users.Include(user => user.Profile),
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
            users => users.Include(user => user.Profile),
            cancellationToken);

        if (user is null)
        {
            return CustomResponse<User>.CreateUnsuccessfulResponse(HttpStatusCode.NotFound);
        }

        return CustomResponse<User>.CreateSuccessfulResponse(user);
    }

    public async Task<CustomResponse<UserWithBioDto>> GetCurrentUserAsync(CancellationToken cancellationToken = default)
    {
        var userExternalId = _authBusiness.GetSignedInUserExternalId();

        var user = await _userRepository.GetByGuidAsync(
            Guid.Parse(userExternalId),
            users => users.Include(user => user.Profile),
            cancellationToken);

        var userDto = _mapper.Map<UserWithBioDto>(user);

        return CustomResponse<UserWithBioDto>.CreateSuccessfulResponse(userDto);
    }

    public async Task<CustomResponse<FileContentResult>> GetCurrentUserProfileImageAsync(CancellationToken cancellationToken = default)
    {
        var userExternalId = _authBusiness.GetSignedInUserExternalId();

        var user = await _userRepository.GetByGuidAsync(
            Guid.Parse(userExternalId),
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

        var userExternalId = _authBusiness.GetSignedInUserExternalId();

        var user = await _unitOfWork.UserRepository.GetByGuidAsync(
            Guid.Parse(userExternalId),
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
        user.LastUpdated = DateTime.Now;
        user.Profile.LastUpdated = DateTime.Now;
        user.Profile.ProfileImage.LastUpdated = DateTime.Now;

        await _unitOfWork.CommitAsync(cancellationToken);

        return CustomResponse.CreateSuccessfulResponse(MessageConstants.SuccessfulProfileImageUploadMessage);
    }

    private async Task<bool> IsUsernameUnique(string username, CancellationToken cancellationToken = default)
    {
        var sieveModel = new SieveModel
        {
            Filters = nameof(User.Username) + "==" + username,
            Page = 1,
            PageSize = 1
        };

        var user = await _userRepository.GetAllAsync(sieveModel, null, cancellationToken);

        return user.Entities.IsNullOrEmpty();
    }

    private async Task<bool> IsEmailUnique(string email, CancellationToken cancellationToken = default)
    {
        var sieveModel = new SieveModel
        {
            Filters = nameof(User.Email) + "==" + email,
            Page = 1,
            PageSize = 1
        };

        var user = await _userRepository.GetAllAsync(sieveModel, null, cancellationToken);

        return user.Entities.IsNullOrEmpty();
    }
}