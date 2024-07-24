using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedditMockup.Business.DomainEntityBusinesses;
using RedditMockup.Common.Dtos;

namespace RedditMockup.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AccountController : ControllerBase
{
    private readonly AccountBusiness _accountBusiness;

    public AccountController(AccountBusiness accountBusiness) =>
        _accountBusiness = accountBusiness;

    [Authorize]
    [HttpPost]
    [Route("logout")]
    public async Task<ActionResult<CustomResponse>> Logout()
    {
        var result = await AccountBusiness.LogoutAsync(HttpContext);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<CustomResponse>> LoginAsync(LoginDto login, CancellationToken cancellationToken)
    {
        var result = await _accountBusiness.LoginAsync(login, HttpContext, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

}