using InsightFlow.Application.Features.Users.Commands;
using InsightFlow.Application.Features.Users.Dtos;
using InsightFlow.Application.Interfaces;
using InsightFlow.Domain.Common;
using InsightFlow.Infrastructure.Common.Constants;
using InsightFlow.Infrastructure.Common.Dtos;
using InsightFlow.Infrastructure.Common.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsightFlow.Api.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IDataValidator<CreateUserRequestDto> _dataValidator;

    public UserController(ISender sender, IDataValidator<CreateUserRequestDto> dataValidator)
    {
        _sender = sender;
        _dataValidator = dataValidator;
    }

    [HttpPost]
    [Authorize(ApplicationConstants.AdminPolicyName)]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequestDto createUserRequestDto, CancellationToken cancellationToken)
    {
        var dataValidationResult = await _dataValidator.ValidateAsync(createUserRequestDto, cancellationToken);

        if (!dataValidationResult.IsValid)
        {
            return BadRequest(DomainResponse<UserResponseDto>
                .CreateFailure(DomainErrors.BadRequest, DomainErrors.BadRequest.Description));
        }

        var command = new CreateUserCommand(
            createUserRequestDto.Username,
            PasswordHelper.HashPassword(createUserRequestDto.Password),
            createUserRequestDto.Email,
            createUserRequestDto.FirstName,
            createUserRequestDto.LastName);

        var result = await _sender.Send(command, cancellationToken);

        var statusCode = result.IsSuccess ? StatusCodes.Status200OK : result.Error.Code;

        return StatusCode(statusCode, result);
    }
}