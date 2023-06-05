using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedditMockup.Api.Contracts;
using RedditMockup.Business.Contracts;
using RedditMockup.Common.Constants;
using RedditMockup.Common.Dtos;
using RedditMockup.Model.BaseEntities;
using RedditMockup.Model.Entities;
using Sieve.Models;

namespace RedditMockup.Api.Base;

[Authorize(PolicyConstants.Admin)]
[ApiController]
[Route("api/[controller]")]
public class BaseController<TEntity, TDto> : ControllerBase, IBaseController<TEntity, TDto>
    where TEntity : BaseEntity
    where TDto : BaseDto
{
    private readonly IBaseBusiness<TEntity, TDto> _business;

    public BaseController(IBaseBusiness<TEntity, TDto> business) =>
        _business = business;

    [HttpGet]
    public async Task<List<TEntity>?> GetAllAsync([FromQuery] SieveModel sieveModel, CancellationToken cancellationToken) =>
        await _business.GetAllAsync(sieveModel, null, cancellationToken);

    [Route("id")]
    [HttpGet]
    public async Task<TEntity?> GetByIdAsync([FromQuery] int id, CancellationToken cancellationToken) =>
        await _business.GetByIdAsync(id, null, cancellationToken);

    [Route("guid")]
    [HttpGet]
    public async Task<TEntity?> GetByGuidAsync([FromQuery] Guid guid, CancellationToken cancellationToken) =>
        await _business.GetByGuidAsync(guid, null, cancellationToken);

    [HttpPost]
    public async Task<TEntity?> CreateAsync([FromBody] TDto dto, CancellationToken cancellationToken) =>
        await _business.CreateAsync(dto, cancellationToken);

    [Route("id")]
    [HttpDelete]
    public async Task<TEntity?> DeleteByIdAsync([FromQuery] int id, CancellationToken cancellationToken) =>
        await _business.DeleteByIdAsync(id, cancellationToken);


    [Route("guid")]
    [HttpDelete]
    public async Task<TEntity?> DeleteByGuidAsync([FromQuery] Guid guid, CancellationToken cancellationToken) =>
        await _business.DeleteByGuidAsync(guid, cancellationToken);


    [HttpPut]
    public async Task<TEntity?> UpdateAsync([FromBody] TDto dto, CancellationToken
        cancellationToken) =>
        await _business.UpdateAsync(dto, cancellationToken);

    [HttpOptions]
    public void Options() =>
        Response.Headers.Add("Allow", "POST,PUT,DELETE,GET");
}