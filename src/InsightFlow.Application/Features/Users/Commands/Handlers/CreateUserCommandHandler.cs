using InsightFlow.Application.Features.Users.Dtos;
using InsightFlow.Application.Interfaces;
using InsightFlow.Domain.Common;
using InsightFlow.Domain.Entities;
using MediatR;

namespace InsightFlow.Application.Features.Users.Commands.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, DomainResponse<UserResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork, IMappingService mappingService)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
    }

    public async Task<DomainResponse<UserResponseDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var isUnique = await IsUserInformationUnique(request, cancellationToken);

        if (!isUnique)
        {
            return DomainResponse<UserResponseDto>.CreateFailure(DomainErrors.BadRequest, DomainErrors.BadRequest.Description);
        }

        var userEntity = _mappingService.Map<CreateUserCommand, User>(request);

        if (userEntity is null)
        {
            return DomainResponse<UserResponseDto>.CreateFailure(DomainErrors.InternalServerError, DomainErrors.InternalServerError.Description);
        }

        await _unitOfWork.UserRepository.CreateAsync(userEntity, cancellationToken);

        var commitResult = await _unitOfWork.CommitChangesAsync(cancellationToken);

        if (commitResult < 1)
        {
            return DomainResponse<UserResponseDto>.CreateFailure(DomainErrors.InternalServerError, DomainErrors.InternalServerError.Description);
        }

        var userResponseDto = _mappingService.Map<User, UserResponseDto>(userEntity);

        if (userResponseDto is null)
        {
            return DomainResponse<UserResponseDto>.CreateFailure(DomainErrors.InternalServerError, DomainErrors.InternalServerError.Description);
        }

        return new DomainResponse<UserResponseDto>(userResponseDto);
    }

    private async Task<bool> IsUserInformationUnique(CreateUserCommand createUserCommand, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork
            .UserRepository
            .GetOneAsync(user =>
                    user.Username == createUserCommand.Username ||
                    user.Email == createUserCommand.Email,
                disableTracking: true,
                cancellationToken: cancellationToken);

        return user is null;
    }
}