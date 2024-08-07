using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedditMockup.Business.Contracts;
using RedditMockup.Common.Dtos;

namespace RedditMockup.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AccountController : ControllerBase
{
    private readonly IAuthBusiness _authBusiness;

    public AccountController(IAuthBusiness authBusiness) =>
        _authBusiness = authBusiness;

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<CustomResponse<string>>> LoginAsync(LoginDto login, CancellationToken cancellationToken)
    {
        var result = await _authBusiness.LoginAsync(login, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }
}