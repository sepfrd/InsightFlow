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
[Route("api/[controller]/[action]")]
public class BaseController<TEntity, TDto> : ControllerBase
    where TEntity : BaseEntity
    where TDto : BaseDto
{
    private readonly IBaseBusiness<TEntity, TDto> _business;

    public BaseController(IBaseBusiness<TEntity, TDto> business) =>
        _business = business;

    [HttpPost]
    public async Task<TEntity?> CreateAsync([FromBody] TDto dto, CancellationToken cancellationToken) =>
        await _business.CreateAsync(dto, cancellationToken);

    [HttpGet]
    public async Task<List<TEntity>?> GetAllAsync([FromQuery] SieveModel sieveModel, CancellationToken cancellationToken) =>
        await _business.GetAllAsync(sieveModel, cancellationToken);

    [HttpGet]
    public async Task<TEntity?> GetByIdAsync([FromQuery] int id, CancellationToken cancellationToken) =>
        await _business.GetByIdAsync(id, cancellationToken);

    [HttpGet]
    public async Task<TEntity?> GetByGuidAsync([FromQuery] Guid guid, CancellationToken cancellationToken) =>
        await _business.GetByGuidAsync(guid, cancellationToken);

    [HttpDelete]
    public async Task<TEntity?> DeleteByIdAsync([FromQuery] int id, CancellationToken cancellationToken) =>
        await _business.DeleteByIdAsync(id, cancellationToken);

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