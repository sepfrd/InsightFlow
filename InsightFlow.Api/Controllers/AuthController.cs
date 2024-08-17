using InsightFlow.Business.Contracts;
using InsightFlow.Common.Dtos;
using InsightFlow.Common.Dtos.CustomResponses;
using Microsoft.AspNetCore.Mvc;

namespace InsightFlow.Api.Controllers;

[ApiController]
[Route("api/auth", Name = "Authentication")]
public class AuthController : ControllerBase
{
    private readonly IAuthBusiness _authBusiness;

    public AuthController(IAuthBusiness authBusiness) =>
        _authBusiness = authBusiness;

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<CustomResponse<string>>> LoginAsync(LoginDto login, CancellationToken cancellationToken)
    {
        var result = await _authBusiness.LoginAsync(login, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }
}