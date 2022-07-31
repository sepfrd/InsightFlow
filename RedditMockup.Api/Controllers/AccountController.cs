using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedditMockup.Business.Businesses;
using RedditMockup.Common.Dtos;
using RedditMockup.Common.ViewModels;
using Sieve.Models;

namespace RedditMockup.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountBusiness _accountBusiness;

    public AccountController(AccountBusiness accountBusiness)
    {
        _accountBusiness = accountBusiness;
    }

    [Authorize]
    [HttpGet]
    public async Task<List<UserViewModel>> GetAllUsersAsync([FromQuery] SieveModel sieveModel,
        CancellationToken cancellationToken) =>
        await _accountBusiness.LoadAllUsersViewModelAsync(sieveModel, cancellationToken);

    [HttpGet]
    [Route("Logout")]
    public async Task<SamanSalamatResponse> Logout()
    {
        return await AccountBusiness.LogoutAsync(HttpContext);
    }

    [HttpPost]
    [Route("Login")]
    public async Task<SamanSalamatResponse> LoginAsync(LoginDto login, CancellationToken cancellationToken)
    {
        return await _accountBusiness.LoginAsync(login, HttpContext, cancellationToken);
    }
}