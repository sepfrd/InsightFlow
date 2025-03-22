using InsightFlow.Api.Common.Dtos.Requests;
using InsightFlow.Application.Features.Users.Commands.CreateUser;
using InsightFlow.Application.Features.Users.Commands.UpdateUser;
using InsightFlow.Application.Features.Users.Dtos;
using InsightFlow.Application.Features.Users.Queries.GetSingleUser;
using InsightFlow.Application.Interfaces;
using InsightFlow.Domain.Common;
using InsightFlow.Infrastructure.Common.Constants;
using InsightFlow.Infrastructure.Common.Dtos;
using InsightFlow.Infrastructure.Common.Helpers;
using InsightFlow.Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsightFlow.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IDataValidator<CreateUserRequestDto> _dataValidator;
    private readonly IAuthService _authService;

    public UserController(
        ISender sender,
        IDataValidator<CreateUserRequestDto> dataValidator,
        IAuthService authService)
    {
        _sender = sender;
        _dataValidator = dataValidator;
        _authService = authService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<DomainResponse<UserResponseDto>>> CreateUserAsync([FromBody] CreateUserRequestDto createUserRequestDto, CancellationToken cancellationToken)
    {
        var dataValidationResult = await _dataValidator.ValidateAsync(createUserRequestDto, cancellationToken);

        if (!dataValidationResult.IsValid)
        {
            return BadRequest(DomainResponse<UserResponseDto>
                .CreateFailure(
                    string.Join(Environment.NewLine, dataValidationResult.ValidationErrors),
                    StatusCodes.Status400BadRequest));
        }

        if (_authService.IsSignedIn())
        {
            return Forbid();
        }

        var command = new CreateUserCommand(
            createUserRequestDto.Username,
            PasswordHelper.HashPassword(createUserRequestDto.Password),
            createUserRequestDto.Email,
            createUserRequestDto.FirstName,
            createUserRequestDto.LastName);

        var result = await _sender.Send(command, cancellationToken);

        return StatusCode(result.StatusCode, result);
    }

    [HttpPost]
    [Route("admins")]
    [Authorize(ApplicationConstants.AdminPolicyName)]
    public async Task<ActionResult<DomainResponse<UserResponseDto>>> CreateAdminAsync([FromBody] CreateUserRequestDto createUserRequestDto, CancellationToken cancellationToken)
    {
        var dataValidationResult = await _dataValidator.ValidateAsync(createUserRequestDto, cancellationToken);

        if (!dataValidationResult.IsValid)
        {
            return BadRequest(DomainResponse<UserResponseDto>
                .CreateFailure(
                    string.Join(Environment.NewLine, dataValidationResult.ValidationErrors),
                    StatusCodes.Status400BadRequest));
        }

        var command = new CreateUserCommand(
            createUserRequestDto.Username,
            PasswordHelper.HashPassword(createUserRequestDto.Password),
            createUserRequestDto.Email,
            createUserRequestDto.FirstName,
            createUserRequestDto.LastName,
            [DomainConstants.AdminRoleTitle]);

        var result = await _sender.Send(command, cancellationToken);

        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    [Authorize]
    [Route("my-information")]
    public async Task<ActionResult<UserResponseDto>> GetCurrentUserInformationAsync(CancellationToken cancellationToken)
    {
        var signedInUserUuid = _authService.GetSignedInUserUuid();

        var request = new GetSingleUserQuery(Guid.Parse(signedInUserUuid));

        var response = await _sender.Send(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

    [HttpPatch]
    [Authorize]
    public async Task<ActionResult<UserResponseDto>> UpdateUserAsync([FromBody] UpdateUserInformationRequestDto request, CancellationToken cancellationToken)
    {
        var signedInUserUuid = _authService.GetSignedInUserUuid();

        var command = new UpdateUserCommand(
            Guid.Parse(signedInUserUuid),
            request.NewEmail,
            request.NewFirstName,
            request.NewLastName);

        var response = await _sender.Send(command, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
}