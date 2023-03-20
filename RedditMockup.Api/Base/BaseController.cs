using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedditMockup.Api.Contracts;
using RedditMockup.Business.Contracts;
using RedditMockup.Common.Dtos;
using RedditMockup.Model.Entities;
using Sieve.Models;

namespace RedditMockup.Api.Base;

[ApiController]
[Route("api/[controller]")]
public class BaseController<T, DTO> : ControllerBase, IBaseController<DTO>
    where T : BaseEntity
{
    private readonly IBaseBusiness<T, DTO> _business;

    public BaseController(IBaseBusiness<T, DTO> business) =>
        _business = business;

    [HttpGet]
    public virtual async Task<CustomResponse<IEnumerable<DTO>>?> GetAllAsync([FromQuery] SieveModel sieveModel, CancellationToken cancellationToken) =>
        await _business.LoadAllAsync(sieveModel, cancellationToken);

    [Authorize]
    [Route("id")]
    [HttpGet]
    public async Task<CustomResponse?> GetByIdAsync(int id, CancellationToken cancellationToken) =>
        await _business.LoadByIdAsync(id, cancellationToken);

    [Authorize]
    [HttpPost]
    public async Task<CustomResponse?> CreateAsync(DTO dto, CancellationToken cancellationToken) =>
        await _business.CreateAsync(dto, HttpContext, cancellationToken);

    [Authorize]
    [HttpDelete]
    public async Task<CustomResponse?> DeleteAsync(int id, CancellationToken cancellationToken) =>
        await _business.DeleteAsync(id, cancellationToken);

    [Authorize]
    [HttpPut]
    public async Task<CustomResponse?> UpdateAsync(int id, DTO dto, CancellationToken
    cancellationToken)
    {

        var response = await _business.UpdateAsync(id, dto, cancellationToken);
        return response;
    }

    [Authorize]
    [HttpOptions]
    public void Options() =>
      Response.Headers.Add("Allow", "POST,PUT,DELETE,GET");

}