using InsightFlow.Application.Features.Users.Dtos;
using InsightFlow.Application.Interfaces;
using InsightFlow.Domain.Common;
using InsightFlow.Domain.Entities;
using MediatR;

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
            return new DomainResponse<UserResponseDto>(null, DomainErrors.NotFound, DomainErrors.NotFound.Description);
        }

        var userResponseDto = _mappingService.Map<User, UserResponseDto>(user);

        return new DomainResponse<UserResponseDto>(userResponseDto);
    }
}