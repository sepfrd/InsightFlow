using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using RedditMockup.Business.Contracts;
using RedditMockup.Common.Constants;
using RedditMockup.Common.Dtos;
using Sieve.Models;

namespace RedditMockup.Api.Base;

[ApiController]
[Route("api")]
public class BaseController<TDto> : ControllerBase
    where TDto : BaseDto
{
    private readonly IBaseBusiness<TDto> _baseBusiness;

    public BaseController(IBaseBusiness<TDto> baseBusiness)
    {
        _baseBusiness = baseBusiness;
    }

    [HttpPost]
    [Authorize(ApplicationConstants.UserPolicyName)]
    public async Task<ActionResult<CustomResponse<TDto>>> CreateAsync([FromBody] TDto dto, CancellationToken cancellationToken)
    {
        var result = await _baseBusiness.CreateAsync(dto, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpGet]
    public async Task<ActionResult<CustomResponse<List<TDto>>>> GetAllAsync([FromQuery] SieveModel sieveModel, CancellationToken cancellationToken)
    {
        var result = await _baseBusiness.GetAllAsync(sieveModel, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpGet]
    [Route("guid/{guid:guid}")]
    public async Task<ActionResult<CustomResponse<TDto>>> GetByGuidAsync([FromRoute] Guid guid, CancellationToken cancellationToken)
    {
        var result = await _baseBusiness.GetByGuidAsync(guid, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpDelete]
    [Route("guid/{guid:guid}")]
    [Authorize(ApplicationConstants.UserPolicyName)]
    public async Task<ActionResult<CustomResponse<TDto>>> DeleteByGuidAsync([FromRoute] Guid guid, CancellationToken cancellationToken)
    {
        var result = await _baseBusiness.DeleteByGuidAsync(guid, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpPut]
    [Authorize(ApplicationConstants.UserPolicyName)]
    public async Task<ActionResult<CustomResponse<TDto>>> UpdateAsync([FromBody] TDto dto, CancellationToken
        cancellationToken)
    {
        var result = await _baseBusiness.UpdateAsync(dto, cancellationToken);

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