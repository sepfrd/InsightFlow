using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedditMockup.Api.Contracts;
using RedditMockup.Business.Contracts;
using RedditMockup.Model.Entities;
using Sieve.Models;

namespace RedditMockup.Api.Base;

[ApiController]
[Route("api/[controller]")]
public class BaseController<T> : ControllerBase, IBaseController<T>
    where T : BaseEntity
{
    private readonly IBaseBusiness<T> _business;

    public BaseController(IBaseBusiness<T> business) =>
        _business = business;

    [Authorize]
    [HttpGet]
    public async Task<IEnumerable<T>?> GetAllAsync([FromQuery] SieveModel sieveModel, CancellationToken cancellationToken) =>
        await _business.LoadAllAsync(sieveModel, cancellationToken);

    [Authorize]
    [Route("id")]
    [HttpGet]
    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken) =>
        await _business.LoadByIdAsync(id, cancellationToken);

    [Authorize]
    [HttpPost]
    public async Task<T?> CreateAsync(T t, CancellationToken cancellationToken) =>
        await _business.CreateAsync(t, cancellationToken);

    [Authorize]
    [HttpDelete]
    public async Task<T?> DeleteAsync(int id, CancellationToken cancellationToken) =>
        await _business.DeleteAsync(id, cancellationToken);

    [Authorize]
    [HttpPut]
    public async Task<T?> UpdateAsync(T t, CancellationToken
    cancellationToken) =>
        await _business.UpdateAsync(t, cancellationToken);

    [Authorize]
    [HttpOptions]
    public void Options() =>
      Response.Headers.Add("Allow", "POST,PUT,DELETE,GET");

}