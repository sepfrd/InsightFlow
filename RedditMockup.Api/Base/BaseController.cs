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
[Authorize]
public class BaseController<T, DTO> : ControllerBase, IBaseController<DTO>
    where T : BaseEntity
{
    private readonly IBaseBusiness<T, DTO> _business;

    public BaseController(IBaseBusiness<T, DTO> business) =>
        _business = business;

    [HttpGet]
    [AllowAnonymous]
    public async Task<CustomResponse<IEnumerable<DTO>>?> GetAllAsync([FromQuery] SieveModel sieveModel, CancellationToken cancellationToken) =>
        await _business.LoadAllAsync(sieveModel, cancellationToken);

    [Route("id")]
    [HttpGet]
    public async Task<CustomResponse?> GetByIdAsync(int id, CancellationToken cancellationToken) =>
        await _business.LoadByIdAsync(id, cancellationToken);

    [HttpPost]
    public async Task<CustomResponse?> CreateAsync(DTO dto, CancellationToken cancellationToken) =>
        await _business.CreateAsync(dto, HttpContext, cancellationToken);

    [HttpDelete]
    public async Task<CustomResponse?> DeleteAsync(int id, CancellationToken cancellationToken) =>
        await _business.DeleteAsync(id, cancellationToken);

    [HttpPut]
    public async Task<CustomResponse?> UpdateAsync(int id, DTO dto, CancellationToken
    cancellationToken)
    {

        var response = await _business.UpdateAsync(id, dto, cancellationToken);
        return response;
    }

    [HttpOptions]
    public void Options() =>
      Response.Headers.Add("Allow", "POST,PUT,DELETE,GET");

}