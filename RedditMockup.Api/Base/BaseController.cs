using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedditMockup.Business.Contracts;
using RedditMockup.Common.Constants;
using RedditMockup.Common.Dtos;
using RedditMockup.Model.BaseEntities;
using Sieve.Models;

namespace RedditMockup.Api.Base;

[Authorize(PolicyConstants.Admin)]
[ApiController]
[Route("api")]
public class BaseController<TEntity, TDto>(IBaseBusiness<TEntity, TDto> business) : ControllerBase
    where TEntity : BaseEntity
    where TDto : BaseDto
{
    [HttpPost]
    public async Task<TEntity?> CreateAsync([FromBody] TDto dto, CancellationToken cancellationToken) =>
        await business.CreateAsync(dto, cancellationToken);

    [HttpGet]
    public async Task<List<TEntity>?> GetAllAsync([FromQuery] SieveModel sieveModel, CancellationToken cancellationToken) =>
        await business.GetAllAsync(sieveModel, cancellationToken);

    [HttpGet]
    [Route("id/{id}")]
    public async Task<TEntity?> GetByIdAsync([FromRoute] int id, CancellationToken cancellationToken) =>
        await business.GetByIdAsync(id, cancellationToken);

    [HttpGet]
    [Route("guid/{guid}")]
    public async Task<TEntity?> GetByGuidAsync([FromRoute] Guid guid, CancellationToken cancellationToken) =>
        await business.GetByGuidAsync(guid, cancellationToken);

    [HttpDelete]
    [Route("id/{id}")]
    public async Task<TEntity?> DeleteByIdAsync([FromRoute] int id, CancellationToken cancellationToken) =>
        await business.DeleteByIdAsync(id, cancellationToken);

    [HttpDelete]
    [Route("guid/{guid}")]
    public async Task<TEntity?> DeleteByGuidAsync([FromRoute] Guid guid, CancellationToken cancellationToken) =>
        await business.DeleteByGuidAsync(guid, cancellationToken);

    [HttpPut]
    public async Task<TEntity?> UpdateAsync([FromBody] TDto dto, CancellationToken
        cancellationToken) =>
        await business.UpdateAsync(dto, cancellationToken);

    [HttpOptions]
    public void Options() =>
        Response.Headers.Add("Allow", "POST,PUT,DELETE,GET");
}