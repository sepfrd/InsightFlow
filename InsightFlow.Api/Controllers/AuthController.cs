using InsightFlow.Business.Interfaces;
using InsightFlow.Common.Constants;
using InsightFlow.Common.Dtos;
using InsightFlow.Common.Dtos.CustomResponses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace InsightFlow.Api.Controllers;

[ApiController]
[Route("api/auth", Name = "Authentication")]
[EnableRateLimiting(ApplicationConstants.FixedWindowRateLimiterPolicy)]
public class AuthController : ControllerBase
{
    private readonly IAuthBusiness _authBusiness;

    public AuthController(IAuthBusiness authBusiness) =>
        _authBusiness = authBusiness;

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<CustomResponse<string>>> LoginAsync([FromBody] LoginDto loginDto, CancellationToken cancellationToken)
    {
        var result = await _authBusiness.LoginAsync(loginDto, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }
}