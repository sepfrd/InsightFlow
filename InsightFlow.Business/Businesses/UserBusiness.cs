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
using InsightFlow.Model.Enums;
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

    public async Task<CustomResponse<UserDto>> CreateUserAsync(CreateUserRequestDto requestDto, CancellationToken cancellationToken = default)
    {
        var isUsernameUnique = await IsUsernameUnique(requestDto.Username, cancellationToken);

        if (!isUsernameUnique)
        {
            var message = string.Format(
                MessageConstants.PropertyNotUniqueMessage,
                requestDto.Username,
                nameof(User.Username).ToLowerInvariant());

            return CustomResponse<UserDto>.CreateUnsuccessfulResponse(HttpStatusCode.BadRequest, message);
        }

        var isEmailUnique = await IsEmailUnique(requestDto.Email, cancellationToken);

        if (!isEmailUnique)
        {
            var message = string.Format(
                MessageConstants.PropertyNotUniqueMessage,
                requestDto.Email,
                nameof(User.Email).ToLowerInvariant());

            return CustomResponse<UserDto>.CreateUnsuccessfulResponse(HttpStatusCode.BadRequest, message);
        }

        var password = PasswordHelper.HashPassword(requestDto.Password);

        var user = new User
        {
            Profile = new Profile
            {
                Bio = requestDto.Bio ?? string.Empty,
                ProfileImage = new ProfileImage()
            },
            Email = requestDto.Email.ToLowerInvariant(),
            Username = requestDto.Username.ToLowerInvariant(),
            Password = password,
            FirstName = requestDto.FirstName,
            LastName = requestDto.LastName
        };

        var createdUser = await _userRepository.CreateAsync(user, cancellationToken);

        createdUser.UserRoles.Add(new UserRole
        {
            UserId = user.Id,
            RoleId = ApplicationConstants.UserRoleId
        });

        await _unitOfWork.CommitAsync(cancellationToken);

        var userDto = _mapper.Map<UserDto>(createdUser);

        return CustomResponse<UserDto>.CreateSuccessfulResponse(userDto, null, HttpStatusCode.Created);
    }

    public async Task<CustomResponse<User>> CreateAdminAsync(CreateUserRequestDto requestDto, CancellationToken cancellationToken = default)
    {
        var isUsernameUnique = await IsUsernameUnique(requestDto.Username, cancellationToken);

        if (!isUsernameUnique)
        {
            var message = string.Format(
                MessageConstants.PropertyNotUniqueMessage,
                requestDto.Username,
                nameof(User.Username).ToLowerInvariant());

            return CustomResponse<User>.CreateUnsuccessfulResponse(HttpStatusCode.BadRequest, message);
        }

        var isEmailUnique = await IsEmailUnique(requestDto.Email, cancellationToken);

        if (!isEmailUnique)
        {
            var message = string.Format(
                MessageConstants.PropertyNotUniqueMessage,
                requestDto.Email,
                nameof(User.Email).ToLowerInvariant());

            return CustomResponse<User>
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
            Email = requestDto.Email.ToLowerInvariant(),
            Username = requestDto.Username.ToLowerInvariant(),
            Password = password,
            FirstName = requestDto.FirstName,
            LastName = requestDto.LastName
        };

        var createdAdmin = await _userRepository.CreateAsync(user, cancellationToken);

        createdAdmin.UserRoles.Add(new UserRole
        {
            UserId = user.Id,
            RoleId = ApplicationConstants.AdminRoleId
        });

        await _unitOfWork.CommitAsync(cancellationToken);

        return CustomResponse<User>.CreateSuccessfulResponse(createdAdmin, null, HttpStatusCode.Created);
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
            users => EntityFrameworkQueryableExtensions.ThenInclude(users
                .Include(user => user.Profile), profile => profile!.ProfileImage),
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
            users => EntityFrameworkQueryableExtensions.ThenInclude(users
                .Include(user => user.Profile), profile => profile!.ProfileImage),
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

    public async Task<CustomResponse<User>> UpdateUserStateAsync(int userId, BaseEntityState newState, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(userId, null, cancellationToken);

        if (user is null)
        {
            return CustomResponse<User>.CreateUnsuccessfulResponse(HttpStatusCode.NotFound);
        }

        if (user.State == newState)
        {
            var message = string.Format(MessageConstants.IdenticalNewValue, "State", newState);

            return CustomResponse<User>.CreateUnsuccessfulResponse(HttpStatusCode.BadRequest, message);
        }

        user.State = newState;
        user.LastUpdated = DateTime.Now;

        await _unitOfWork.CommitAsync(cancellationToken);

        var successMessage = string.Format(MessageConstants.SuccessfulUpdateMessage, nameof(User).ToLowerInvariant());

        return CustomResponse<User>.CreateSuccessfulResponse(user, successMessage);
    }

    public async Task<CustomResponse<UserWithBioDto>> UpdateCurrentUserAsync(UpdateUserRequestDto requestDto, CancellationToken cancellationToken = default)
    {
        var userExternalId = _authBusiness.GetSignedInUserExternalId();

        var user = await _userRepository.GetByGuidAsync(
            Guid.Parse(userExternalId),
            users => users.Include(user => user.Profile),
            cancellationToken);

        if (string.Equals(requestDto.Username, user!.Username, StringComparison.InvariantCultureIgnoreCase) &&
            string.Equals(requestDto.Email, user.Email, StringComparison.InvariantCultureIgnoreCase) &&
            string.Equals(requestDto.FirstName, user.FirstName, StringComparison.InvariantCultureIgnoreCase) &&
            string.Equals(requestDto.LastName, user.LastName, StringComparison.InvariantCultureIgnoreCase) &&
            string.Equals(requestDto.Bio, user.Profile!.Bio, StringComparison.InvariantCultureIgnoreCase))
        {
            return CustomResponse<UserWithBioDto>.CreateUnsuccessfulResponse(
                HttpStatusCode.BadRequest,
                MessageConstants.IdenticalNewPropertyValues);
        }

        if (!string.Equals(requestDto.Username, user.Username, StringComparison.InvariantCultureIgnoreCase))
        {
            var isUsernameUnique = await IsUsernameUnique(requestDto.Username, cancellationToken);

            if (!isUsernameUnique)
            {
                var message = string.Format(
                    MessageConstants.PropertyNotUniqueMessage,
                    requestDto,
                    nameof(User.Username).ToLowerInvariant());

                return CustomResponse<UserWithBioDto>.CreateUnsuccessfulResponse(HttpStatusCode.BadRequest, message);
            }

            user.Username = requestDto.Username;
        }

        if (!string.Equals(requestDto.Email, user.Email, StringComparison.InvariantCultureIgnoreCase))
        {
            var isEmailUnique = await IsEmailUnique(requestDto.Email, cancellationToken);

            if (!isEmailUnique)
            {
                var message = string.Format(
                    MessageConstants.PropertyNotUniqueMessage,
                    requestDto,
                    nameof(User.Email).ToLowerInvariant());

                return CustomResponse<UserWithBioDto>.CreateUnsuccessfulResponse(HttpStatusCode.BadRequest, message);
            }

            user.Email = requestDto.Email;
        }

        user.FirstName = requestDto.FirstName;
        user.LastName = requestDto.LastName;
        user.Profile!.Bio = requestDto.Bio ?? user.Profile.Bio;

        user.LastUpdated = DateTime.Now;

        await _unitOfWork.CommitAsync(cancellationToken);

        var updatedUserDto = _mapper.Map<UserWithBioDto>(user);

        var successMessage = string.Format(MessageConstants.SuccessfulUpdateMessage, nameof(User).ToLowerInvariant());

        return CustomResponse<UserWithBioDto>.CreateSuccessfulResponse(updatedUserDto, successMessage);
    }

    public async Task<CustomResponse> ChangeCurrentUserPasswordAsync(ChangePasswordRequestDto requestDto, CancellationToken cancellationToken = default)
    {
        var userExternalId = _authBusiness.GetSignedInUserExternalId();

        var user = await _userRepository.GetByGuidAsync(Guid.Parse(userExternalId), null, cancellationToken);

        var isPasswordValid = PasswordHelper.ValidatePassword(requestDto.OldPassword, user!.Password);

        if (!isPasswordValid)
        {
            var message = string.Format(MessageConstants.InvalidParametersMessage, nameof(requestDto.OldPassword));

            return CustomResponse.CreateUnsuccessfulResponse(HttpStatusCode.BadRequest, message);
        }

        var newPassword = PasswordHelper.HashPassword(requestDto.NewPassword);

        user.Password = newPassword;
        user.LastUpdated = DateTime.Now;

        await _unitOfWork.CommitAsync(cancellationToken);

        var successMessage = string.Format(MessageConstants.SuccessfulUpdateMessage, nameof(User).ToLowerInvariant());

        return CustomResponse.CreateSuccessfulResponse(successMessage);
    }

    public async Task<CustomResponse> SoftDeleteCurrentUserAsync(SoftDeleteCurrentUserRequestDto requestDto, CancellationToken cancellationToken = default)
    {
        var userExternalId = _authBusiness.GetSignedInUserExternalId();

        var user = await _userRepository.GetByGuidAsync(
            Guid.Parse(userExternalId),
            users => EntityFrameworkQueryableExtensions.Include(users
                    .Include(user => user.Profile), user => user.UserRoles)
                .Include(user => user.Answers)
                .Include(user => user.Questions)
                .ThenInclude(question => question.Answers),
            cancellationToken);

        if (user is null)
        {
            var message = string.Format(MessageConstants.InvalidParametersMessage, nameof(requestDto.Username));

            return CustomResponse.CreateUnsuccessfulResponse(HttpStatusCode.BadRequest, message);
        }

        if (!string.Equals(requestDto.Username, user.Username, StringComparison.InvariantCultureIgnoreCase))
        {
            var message = string.Format(MessageConstants.InvalidParametersMessage, nameof(requestDto.Username));

            return CustomResponse.CreateUnsuccessfulResponse(HttpStatusCode.BadRequest, message);
        }

        var isPasswordValid = PasswordHelper.ValidatePassword(requestDto.Password, user.Password);

        if (!isPasswordValid)
        {
            var message = string.Format(MessageConstants.InvalidParametersMessage, nameof(requestDto.Password));

            return CustomResponse.CreateUnsuccessfulResponse(HttpStatusCode.BadRequest, message);
        }

        user.State = BaseEntityState.Deleted;
        user.LastUpdated = DateTime.Now;

        user.Profile!.State = BaseEntityState.Deleted;
        user.Profile!.LastUpdated = DateTime.Now;

        foreach (var answer in user.Answers)
        {
            answer.State = BaseEntityState.Deleted;
            answer.LastUpdated = DateTime.Now;
        }

        foreach (var question in user.Questions)
        {
            foreach (var answer in question.Answers)
            {
                answer.State = BaseEntityState.Deleted;
                answer.LastUpdated = DateTime.Now;
            }

            question.State = BaseEntityState.Deleted;
            question.LastUpdated = DateTime.Now;
        }

        foreach (var userRole in user.UserRoles)
        {
            userRole.State = BaseEntityState.Deleted;
            userRole.LastUpdated = DateTime.Now;
        }

        await _unitOfWork.CommitAsync(cancellationToken);

        var successMessage = string.Format(MessageConstants.SuccessfulDeleteMessage, nameof(User).ToLowerInvariant());

        return CustomResponse.CreateSuccessfulResponse(successMessage);
    }

    public async Task<CustomResponse> HardDeleteUserByIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(
            userId,
            users => EntityFrameworkQueryableExtensions.Include(users
                    .Include(user => user.Profile), user => user.UserRoles)
                .Include(user => user.Answers)
                .Include(user => user.Questions)
                .ThenInclude(question => question.Answers),
            cancellationToken);

        if (user is null)
        {
            var message = string.Format(MessageConstants.EntityNotFoundByIdMessage, nameof(User), userId);

            return CustomResponse.CreateUnsuccessfulResponse(HttpStatusCode.NotFound, message);
        }

        foreach (var question in user.Questions)
        {
            _unitOfWork.QuestionRepository.Delete(question);
        }

        _userRepository.Delete(user);

        await _unitOfWork.CommitAsync(cancellationToken);

        var successMessage = string.Format(MessageConstants.SuccessfulDeleteMessage, nameof(User).ToLowerInvariant());

        return CustomResponse.CreateSuccessfulResponse(successMessage);
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

        return user.Entities is null || user.Entities.Count == 0;
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

        return user.Entities is null || user.Entities.Count == 0;
    }
}