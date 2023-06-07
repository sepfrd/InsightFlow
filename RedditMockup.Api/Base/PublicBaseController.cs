using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedditMockup.Business.Contracts;
using RedditMockup.Common.Dtos;
using Sieve.Models;

namespace RedditMockup.Api.Base;

[ApiController]
[Route("public/api/[controller]/[action]")]
public class PublicBaseController<TDto> : ControllerBase
    where TDto : BaseDto
{
    #region [Fields]

    private readonly IPublicBaseBusiness<TDto> _publicBaseBusiness;

    #endregion

    #region [Constructor]

    public PublicBaseController(IPublicBaseBusiness<TDto> publicBaseBusiness) =>
        _publicBaseBusiness = publicBaseBusiness;

    #endregion

    #region [Methods]

    [Authorize]
    [HttpPost]
    public async Task<CustomResponse<TDto>> CreateAsync([FromBody] TDto dto, CancellationToken cancellationToken) =>
        await _publicBaseBusiness.PublicCreateAsync(dto, cancellationToken);

    [HttpGet]
    public async Task<CustomResponse<List<TDto>>> GetAllAsync([FromQuery] SieveModel sieveModel, CancellationToken cancellationToken) =>
        await _publicBaseBusiness.PublicGetAllAsync(sieveModel, cancellationToken);

    [HttpGet]
    public async Task<CustomResponse<TDto>> GetByGuidAsync([FromQuery] Guid guid, CancellationToken cancellationToken) =>
        await _publicBaseBusiness.PublicGetByGuidAsync(guid, cancellationToken);

    [Authorize]
    [HttpDelete]
    public async Task<CustomResponse<TDto>> DeleteByGuidAsync([FromQuery] Guid guid, CancellationToken cancellationToken) =>
        await _publicBaseBusiness.PublicDeleteByGuidAsync(guid, cancellationToken);

    [Authorize]
    [HttpPut]
    public async Task<CustomResponse<TDto>> UpdateAsync([FromBody] TDto dto, CancellationToken
        cancellationToken) =>
        await _publicBaseBusiness.PublicUpdateAsync(dto, cancellationToken);

    [HttpOptions]
    public void Options() =>
        Response.Headers.Add("Allow", "POST,PUT,DELETE,GET");

    #endregion
}