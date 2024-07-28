using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using RedditMockup.Business.Contracts;
using RedditMockup.Common.Dtos;
using Sieve.Models;

namespace RedditMockup.Api.Base;

[ApiController]
public class PublicBaseController<TDto> : ControllerBase
    where TDto : BaseDto
{
    private readonly IPublicBaseBusiness<TDto> _publicBaseBusiness;

    public PublicBaseController(IPublicBaseBusiness<TDto> publicBaseBusiness)
    {
        _publicBaseBusiness = publicBaseBusiness;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<CustomResponse<TDto>>> CreateAsync([FromBody] TDto dto, CancellationToken cancellationToken)
    {
        var result = await _publicBaseBusiness.PublicCreateAsync(dto, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpGet]
    public async Task<ActionResult<CustomResponse<List<TDto>>>> GetAllAsync([FromQuery] SieveModel sieveModel, CancellationToken cancellationToken)
    {
        var result = await _publicBaseBusiness.PublicGetAllAsync(sieveModel, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpGet]
    [Route("guid/{guid:guid}")]
    public async Task<ActionResult<CustomResponse<TDto>>> GetByGuidAsync([FromRoute] Guid guid, CancellationToken cancellationToken)
    {
        var result = await _publicBaseBusiness.PublicGetByGuidAsync(guid, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [Authorize]
    [HttpDelete]
    [Route("guid/{guid:guid}")]
    public async Task<ActionResult<CustomResponse<TDto>>> DeleteByGuidAsync([FromRoute] Guid guid, CancellationToken cancellationToken)
    {
        var result = await _publicBaseBusiness.PublicDeleteByGuidAsync(guid, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<CustomResponse<TDto>>> UpdateAsync([FromBody] TDto dto, CancellationToken
        cancellationToken)
    {
        var result = await _publicBaseBusiness.PublicUpdateAsync(dto, cancellationToken);

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