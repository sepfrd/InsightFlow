using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using RedditMockup.Business.Contracts;
using RedditMockup.Common.Dtos;
using RedditMockup.Model.BaseEntities;
using Sieve.Models;

namespace RedditMockup.Api.Base;

[ApiController]
[Route("public/api")]
public class PublicBaseController<TEntity, TDto>(IPublicBaseBusiness<TDto> publicBaseBusiness) : ControllerBase
    where TDto : BaseDto
    where TEntity : BaseEntityWithGuid
{
    // [Methods]

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<CustomResponse<TDto>>> CreateAsync([FromBody] TDto dto, CancellationToken cancellationToken)
    {
        var result = await publicBaseBusiness.PublicCreateAsync(dto, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpGet]
    public async Task<ActionResult<CustomResponse<List<TDto>>>> GetAllAsync([FromQuery] SieveModel sieveModel, CancellationToken cancellationToken)
    {
        var result = await publicBaseBusiness.PublicGetAllAsync(sieveModel, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpGet]
    [Route("guid/{guid:guid}")]
    public async Task<ActionResult<CustomResponse<TDto>>> GetByGuidAsync([FromRoute] Guid guid, CancellationToken cancellationToken)
    {
        var result = await publicBaseBusiness.PublicGetByGuidAsync(guid, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [Authorize]
    [HttpDelete]
    [Route("guid/{guid:guid}")]
    public async Task<ActionResult<CustomResponse<TDto>>> DeleteByGuidAsync([FromRoute] Guid guid, CancellationToken cancellationToken)
    {
        var result = await publicBaseBusiness.PublicDeleteByGuidAsync(guid, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<CustomResponse<TDto>>> UpdateAsync([FromBody] TDto dto, CancellationToken
        cancellationToken)
    {
        var result = await publicBaseBusiness.PublicUpdateAsync(dto, cancellationToken);

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

    // --------------------------------------
}