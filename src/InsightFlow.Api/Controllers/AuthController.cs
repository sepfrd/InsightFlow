using InsightFlow.Infrastructure.Common.Dtos;
using InsightFlow.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsightFlow.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto, CancellationToken cancellationToken)
    {
        var result = await _authService.AuthenticateAsync(loginDto, cancellationToken);

        return StatusCode(result.StatusCode, result);
    }
}