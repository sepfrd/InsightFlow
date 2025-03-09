using Common.Constants;
using InsightFlow.Application.Features.Users.Dtos;
using InsightFlow.Application.Interfaces;
using InsightFlow.Domain.Common;
using InsightFlow.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InsightFlow.Application.Features.Users.Queries.Handlers;

public class GetSingleUserQueryHandler : IRequestHandler<GetSingleUserQuery, DomainResponse<UserResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;

    public GetSingleUserQueryHandler(IUnitOfWork unitOfWork, IMappingService mappingService)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
    }

    public async Task<DomainResponse<UserResponseDto>> Handle(GetSingleUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetOneAsync(
            user => user.Uuid == request.Uuid,
            disableTracking: true,
            cancellationToken: cancellationToken);

        if (user is null)
        {
            var message = string.Format(StringConstants.EntityNotFoundByUuidTemplate, nameof(User), request.Uuid);

            return DomainResponse<UserResponseDto>.CreateFailure(message, StatusCodes.Status404NotFound);
        }

        var userResponseDto = _mappingService.Map<User, UserResponseDto>(user);

        if (userResponseDto is null)
        {
            return DomainResponse<UserResponseDto>.CreateFailure(
                StringConstants.InternalServerError,
                StatusCodes.Status500InternalServerError);
        }

        return DomainResponse<UserResponseDto>.CreateSuccess(null, StatusCodes.Status200OK, userResponseDto);
    }
}