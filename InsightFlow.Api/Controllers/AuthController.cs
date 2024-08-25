using DNTCaptcha.Core;
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

    [HttpGet]
    [Route("captcha/new-captcha")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true, Duration = 0)]
    [EnableRateLimiting(DNTCaptchaRateLimiterPolicy.Name)]
    public ActionResult<DNTCaptchaApiResponse> GetNewCaptcha() =>
        _authBusiness.CreateCaptcha();

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<CustomResponse<string>>> LoginAsync(
        [FromForm] CaptchaDto captchaDto,
        [FromForm] LoginDto login,
        CancellationToken cancellationToken)
    {
        var result = await _authBusiness.LoginAsync(captchaDto, login, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }
}