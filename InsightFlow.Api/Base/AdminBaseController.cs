using InsightFlow.Business.Contracts;
using InsightFlow.Common.Constants;
using InsightFlow.Common.Dtos;
using InsightFlow.Common.Dtos.CustomResponses;
using InsightFlow.Model.BaseEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Sieve.Models;

namespace InsightFlow.Api.Base;

[ApiController]
[Route("api/admin")]
[Authorize(ApplicationConstants.AdminPolicyName)]
public abstract class AdminBaseController<TEntity, TDto> : ControllerBase
    where TEntity : BaseEntity
    where TDto : BaseDto
{
    private readonly IAdminBaseBusiness<TEntity, TDto> _business;

    protected AdminBaseController(IAdminBaseBusiness<TEntity, TDto> business)
    {
        _business = business;
    }

    [HttpPost]
    public async Task<ActionResult<CustomResponse<TEntity?>>> CreateAsync([FromBody] TDto dto, CancellationToken cancellationToken)
    {
        var result = await _business.CreateAsync(dto, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpGet]
    public async Task<ActionResult<CustomResponse<List<TEntity>?>>> GetAllAsync([FromQuery] SieveModel sieveModel, CancellationToken cancellationToken)
    {
        var result = await _business.GetAllAsync(sieveModel, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpGet]
    [Route("id/{id:int}")]
    public async Task<ActionResult<CustomResponse<TEntity?>>> GetByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _business.GetByIdAsync(id, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpGet]
    [Route("guid/{guid:guid}")]
    public async Task<ActionResult<CustomResponse<TEntity?>>> GetByGuidAsync([FromRoute] Guid guid, CancellationToken cancellationToken)
    {
        var result = await _business.GetByGuidAsync(guid, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpDelete]
    [Route("id/{id:int}")]
    public async Task<ActionResult<CustomResponse<TEntity?>>> DeleteByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _business.DeleteByIdAsync(id, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpDelete]
    [Route("guid/{guid:guid}")]
    public async Task<ActionResult<CustomResponse<TEntity?>>> DeleteByGuidAsync([FromRoute] Guid guid, CancellationToken cancellationToken)
    {
        var result = await _business.DeleteByGuidAsync(guid, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpPut]
    public async Task<ActionResult<CustomResponse<TEntity?>>> UpdateAsync([FromBody] TDto dto, CancellationToken
        cancellationToken)
    {
        var result = await _business.UpdateAsync(dto, cancellationToken);

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

}