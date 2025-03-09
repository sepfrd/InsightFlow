using Common.Constants;
using InsightFlow.Application.Features.Users.Dtos;
using InsightFlow.Application.Interfaces;
using InsightFlow.Domain.Common;
using InsightFlow.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

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
        var isEmailUnique = await IsEmailUniqueAsync(request.Email, cancellationToken);

        if (!isEmailUnique)
        {
            var message = string.Format(
                StringConstants.PropertyNotUniqueTemplate,
                request.Email,
                nameof(CreateUserCommand.Email));

            return DomainResponse<UserResponseDto>.CreateFailure(message, StatusCodes.Status400BadRequest);
        }

        var isUsernameUnique = await IsUsernameUniqueAsync(request.Username, cancellationToken);

        if (!isUsernameUnique)
        {
            var message = string.Format(
                StringConstants.PropertyNotUniqueTemplate,
                request.Username,
                nameof(CreateUserCommand.Username));

            return DomainResponse<UserResponseDto>.CreateFailure(message, StatusCodes.Status400BadRequest);
        }

        var userEntity = _mappingService.Map<CreateUserCommand, User>(request);

        if (userEntity is null)
        {
            return DomainResponse<UserResponseDto>.CreateFailure(
                StringConstants.InternalServerError,
                StatusCodes.Status500InternalServerError);
        }

        await _unitOfWork.UserRepository.CreateAsync(userEntity, cancellationToken);

        var commitResult = await _unitOfWork.CommitChangesAsync(cancellationToken);

        if (commitResult < 1)
        {
            return DomainResponse<UserResponseDto>.CreateFailure(
                StringConstants.InternalServerError,
                StatusCodes.Status500InternalServerError);
        }

        var userResponseDto = _mappingService.Map<User, UserResponseDto>(userEntity);

        if (userResponseDto is null)
        {
            return DomainResponse<UserResponseDto>.CreateFailure(
                StringConstants.InternalServerError,
                StatusCodes.Status500InternalServerError);
        }

        return DomainResponse<UserResponseDto>.CreateSuccess(null, StatusCodes.Status201Created, userResponseDto);
    }

    private async Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork
            .UserRepository
            .GetOneAsync(
                user => user.Email == email,
                disableTracking: true,
                cancellationToken: cancellationToken);

        return user is null;
    }

    private async Task<bool> IsUsernameUniqueAsync(string username, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork
            .UserRepository
            .GetOneAsync(
                user => user.Username == username,
                disableTracking: true,
                cancellationToken: cancellationToken);

        return user is null;
    }
}