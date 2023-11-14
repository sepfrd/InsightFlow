using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<CustomResponse<TDto>> CreateAsync([FromBody] TDto dto, CancellationToken cancellationToken) =>
        await publicBaseBusiness.PublicCreateAsync(dto, cancellationToken);

    [HttpGet]
    public async Task<CustomResponse<List<TDto>>> GetAllAsync([FromQuery] SieveModel sieveModel, CancellationToken cancellationToken) =>
        await publicBaseBusiness.PublicGetAllAsync(sieveModel, cancellationToken);

    [HttpGet]
    [Route("guid/{guid}")]
    public async Task<CustomResponse<TDto>> GetByGuidAsync([FromRoute] Guid guid, CancellationToken cancellationToken) =>
        await publicBaseBusiness.PublicGetByGuidAsync(guid, cancellationToken);

    [Authorize]
    [HttpDelete]
    [Route("guid/{guid}")]
    public async Task<CustomResponse<TDto>> DeleteByGuidAsync([FromRoute] Guid guid, CancellationToken cancellationToken) =>
        await publicBaseBusiness.PublicDeleteByGuidAsync(guid, cancellationToken);

    [Authorize]
    [HttpPut]
    public async Task<CustomResponse<TDto>> UpdateAsync([FromBody] TDto dto, CancellationToken
        cancellationToken) =>
        await publicBaseBusiness.PublicUpdateAsync(dto, cancellationToken);

    [HttpOptions]
    public void Options() =>
        Response.Headers.Add("Allow", "POST,PUT,DELETE,GET");

    // --------------------------------------
}