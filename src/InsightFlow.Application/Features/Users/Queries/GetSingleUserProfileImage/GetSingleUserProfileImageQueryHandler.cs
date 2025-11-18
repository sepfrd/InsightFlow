using Humanizer;
using InsightFlow.Application.Features.Users.Dtos;
using InsightFlow.Application.Interfaces;
using InsightFlow.Common.Constants;
using InsightFlow.Common.Cqrs.Queries;
using InsightFlow.Domain.Common;
using InsightFlow.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace InsightFlow.Application.Features.Users.Queries.GetSingleUserProfileImage;

public class GetSingleUserProfileImageQueryHandler : IQueryHandler<GetSingleUserProfileImageQuery, DomainResponse<ProfileImageResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSingleUserProfileImageQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DomainResponse<ProfileImageResponseDto>> HandleAsync(GetSingleUserProfileImageQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetOneAsync(
            user => user.Uuid == request.UserUuid,
            [user => user.ProfileImage],
            disableTracking: true,
            cancellationToken: cancellationToken);

        if (user is null)
        {
            var message = string.Format(
                StringConstants.EntityNotFoundByUuidTemplate,
                nameof(User).Humanize(LetterCasing.LowerCase),
                request.UserUuid);

            return DomainResponse<ProfileImageResponseDto>.CreateFailure(message, StatusCodes.Status404NotFound);
        }

        if (user.ProfileImage!.ImageBytes is null)
        {
            return DomainResponse<ProfileImageResponseDto>.CreateFailure(StringConstants.NoProfileImageUploadedYetMessage, StatusCodes.Status404NotFound);
        }

        var responseDto = new ProfileImageResponseDto(
            user.ProfileImage.ImageBytes,
            user.ProfileImage.ImageFormat!,
            user.ProfileImage.UpdatedAt);

        return DomainResponse<ProfileImageResponseDto>.CreateSuccess(null, StatusCodes.Status200OK, responseDto);
    }
}