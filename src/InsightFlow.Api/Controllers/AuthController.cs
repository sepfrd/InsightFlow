using InsightFlow.Application.Interfaces;
using InsightFlow.Domain.Common;
using InsightFlow.Infrastructure.Common.Dtos;
using InsightFlow.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace InsightFlow.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IDataValidator<LoginDto> _dataValidator;

    public AuthController(IAuthService authService, IDataValidator<LoginDto> dataValidator)
    {
        _authService = authService;
        _dataValidator = dataValidator;
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<DomainResponse<string>>> Login([FromBody] LoginDto loginDto, CancellationToken cancellationToken)
    {
        var dataValidationResult = await _dataValidator.ValidateAsync(loginDto, cancellationToken);

        if (!dataValidationResult.IsValid)
        {
            return BadRequest(DomainResponse<string>
                .CreateFailure(
                    string.Join(Environment.NewLine, dataValidationResult.ValidationErrors),
                    StatusCodes.Status400BadRequest));
        }

        var result = await _authService.AuthenticateAsync(loginDto, cancellationToken);

        return StatusCode(result.StatusCode, result);
    }

    [HttpOptions]
    public IActionResult AuthOptions()
    {
        Response
            .Headers
            .Add(new KeyValuePair<string, StringValues>("Allow", HttpMethods.Post));

        return Ok();
    }
}