using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedditMockup.Api.Contracts;
using RedditMockup.Business.Contracts;
using RedditMockup.Common.Constants;
using RedditMockup.Model.BaseEntities;
using RedditMockup.Model.Entities;
using Sieve.Models;

namespace RedditMockup.Api.Base;

[Authorize(PolicyConstants.Admin)]
[ApiController]
[Route("api/[controller]")]
public class BaseController<T> : ControllerBase, IBaseController<T>
    where T : BaseEntity
{
    private readonly IBaseBusiness<T> _business;

    public BaseController(IBaseBusiness<T> business) =>
        _business = business;

    [HttpGet]
    public async Task<IEnumerable<T>?> GetAllAsync([FromQuery] SieveModel sieveModel, CancellationToken cancellationToken) =>
        await _business.GetAllAsync(sieveModel, cancellationToken);

    [Route("id")]
    [HttpGet]
    public async Task<T?> GetByIdAsync([FromQuery] int id, CancellationToken cancellationToken) =>
        await _business.GetByIdAsync(id, cancellationToken);

    [HttpPost]
    public async Task<T?> CreateAsync([FromBody] T t, CancellationToken cancellationToken) =>
        await _business.CreateAsync(t, cancellationToken);

    [HttpDelete]
    public async Task<T?> DeleteAsync([FromQuery] int id, CancellationToken cancellationToken) =>
        await _business.DeleteByIdAsync(id, cancellationToken);

    [HttpPut]
    public async Task<T?> UpdateAsync([FromBody] T t, CancellationToken
    cancellationToken) =>
        await _business.UpdateAsync(t, cancellationToken);

    [HttpOptions]
    public void Options() =>
      Response.Headers.Add("Allow", "POST,PUT,DELETE,GET");
}