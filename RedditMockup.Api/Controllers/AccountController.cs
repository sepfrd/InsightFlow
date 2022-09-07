using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedditMockup.Business.Businesses;
using RedditMockup.Common.Constants;
using RedditMockup.Common.Dtos;
using RedditMockup.Common.ViewModels;
using Sieve.Models;

namespace RedditMockup.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
        private readonly AccountBusiness _accountBusiness;

        public AccountController(AccountBusiness accountBusiness) =>
            _accountBusiness = accountBusiness;

        [Authorize(Policy = PolicyConstants.Admin)]
        [HttpGet]
        public async Task<List<UserViewModel>> GetAllUsersAsync([FromQuery] SieveModel sieveModel,
            CancellationToken cancellationToken) =>
            await _accountBusiness.LoadAllUsersViewModelAsync(sieveModel, cancellationToken);

        [Authorize]
        [HttpGet]
        [Route("Logout")]
        public async Task<CustomResponse> Logout() =>
            await AccountBusiness.LogoutAsync(HttpContext);

        [HttpPost]
        [Route("Login")]
        public async Task<CustomResponse> LoginAsync(LoginDto login, CancellationToken cancellationToken) =>
            await _accountBusiness.LoginAsync(login, HttpContext, cancellationToken);

}