using Humanizer;
using InsightFlow.Application.Features.Users.Dtos;
using InsightFlow.Application.Interfaces;
using InsightFlow.Common.Constants;
using InsightFlow.Domain.Common;
using InsightFlow.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace InsightFlow.Application.Features.Users.Queries.GetSingleUser;

public class GetSingleUserQueryHandler : IRequestHandler<GetSingleUserQuery, DomainResponse<UserResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;
    private readonly ILogger<GetSingleUserQueryHandler> _logger;

    public GetSingleUserQueryHandler(
        IUnitOfWork unitOfWork,
        IMappingService mappingService,
        ILogger<GetSingleUserQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<DomainResponse<UserResponseDto>> Handle(GetSingleUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetOneAsync(
            user => user.Uuid == request.Uuid,
            disableTracking: true,
            cancellationToken: cancellationToken);

        if (user is null)
        {
            var message = string.Format(
                StringConstants.EntityNotFoundByUuidTemplate,
                nameof(User).Humanize(LetterCasing.LowerCase),
                request.Uuid);

            return DomainResponse<UserResponseDto>.CreateFailure(message, StatusCodes.Status404NotFound);
        }

        var userResponseDto = _mappingService.Map<User, UserResponseDto>(user);

        if (userResponseDto is not null)
        {
            return DomainResponse<UserResponseDto>.CreateSuccess(null, StatusCodes.Status200OK, userResponseDto);
        }

        _logger.LogCritical(StringConstants.MappingErrorLogTemplate, typeof(User), typeof(UserResponseDto));

        return DomainResponse<UserResponseDto>.CreateFailure(
            StringConstants.InternalServerError,
            StatusCodes.Status500InternalServerError);
    }
}