using InsightFlow.Business.Interfaces;
using InsightFlow.Common.Constants;
using InsightFlow.Common.Dtos;
using InsightFlow.Common.Dtos.CustomResponses;
using InsightFlow.Common.Dtos.Requests;
using InsightFlow.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Sieve.Models;

namespace InsightFlow.Api.Controllers;

[ApiController]
[Route("api/users", Name = "Users")]
public class UserController : ControllerBase
{
    private readonly IUserBusiness _userBusiness;

    public UserController(IUserBusiness userBusiness)
    {
        _userBusiness = userBusiness;
    }

    [HttpPost]
    [Authorize(ApplicationConstants.UserPolicyName)]
    public async Task<ActionResult<CustomResponse<UserDto>>> CreateUserAsync([FromBody] CreateUserRequestDto requestDto, CancellationToken cancellationToken)
    {
        var result = await _userBusiness.CreateUserAsync(requestDto, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpGet]
    [Authorize(ApplicationConstants.AdminPolicyName)]
    public async Task<ActionResult<CustomResponse<List<User>>>> GetAllUsersAsync([FromQuery] SieveModel sieveModel, CancellationToken cancellationToken)
    {
        var result = await _userBusiness.GetAllUsersAsync(sieveModel, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpGet]
    [Route("id/{userId:int}")]
    [Authorize(ApplicationConstants.AdminPolicyName)]
    public async Task<ActionResult<CustomResponse<UserDto>>> GetUserByIdAsync([FromRoute] int userId, CancellationToken cancellationToken)
    {
        var result = await _userBusiness.GetUserByIdAsync(userId, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpGet]
    [Authorize]
    [Route("current-user")]
    public async Task<ActionResult<CustomResponse<UserDto>>> GetCurrentUserAsync(CancellationToken cancellationToken)
    {
        var result = await _userBusiness.GetCurrentUserAsync(cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpGet]
    [Authorize]
    [Route("current-user/profile/image")]
    public async Task<ActionResult> GetCurrentUserProfileImageAsync(CancellationToken cancellationToken)
    {
        var result = await _userBusiness.GetCurrentUserProfileImageAsync(cancellationToken);

        return result.Data is null ? StatusCode((int)result.HttpStatusCode, result) : result.Data;
    }

    [HttpPost]
    [Authorize]
    [Route("current-user/profile/image")]
    [RequestSizeLimit(2097152L)]
    public async Task<ActionResult<CustomResponse>> UploadProfileImageForCurrentUserAsync(
        IFormFile profileImage,
        CancellationToken cancellationToken)
    {
        var result = await _userBusiness.AddProfileImageForCurrentUserAsync(profileImage, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpOptions]
    public IActionResult Options()
    {
        Response
            .Headers
            .Add(new KeyValuePair<string, StringValues>("Allow", $"{HttpMethods.Post},{HttpMethods.Get},{HttpMethods.Put},{HttpMethods.Delete}"));

        return Ok();
    }
}