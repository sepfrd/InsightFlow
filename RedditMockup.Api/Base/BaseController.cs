using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using RedditMockup.Business.Contracts;
using RedditMockup.Common.Constants;
using RedditMockup.Common.Dtos;
using RedditMockup.Model.BaseEntities;
using Sieve.Models;

namespace RedditMockup.Api.Base;

[ApiController]
[Authorize(PolicyConstants.Admin)]
public abstract class BaseController<TEntity, TDto> : ControllerBase
    where TEntity : BaseEntity
    where TDto : BaseDto
{
    private readonly IBaseBusiness<TEntity, TDto> _business;

    protected BaseController(IBaseBusiness<TEntity, TDto> business)
    {
        _business = business;
    }

    [HttpPost]
    public async Task<ActionResult<CustomResponse<TEntity?>>> CreateAsync([FromBody] TDto dto, CancellationToken cancellationToken)
    {
        var result = await _business.CreateAsync(dto, cancellationToken);

        var response = new CustomResponse<TEntity?>
        {
            Data = result,
            HttpStatusCode = result is null ? HttpStatusCode.InternalServerError : HttpStatusCode.OK,
            IsSuccess = result is not null
        };

        return StatusCode((int)response.HttpStatusCode, result);
    }

    [HttpGet]
    public async Task<ActionResult<CustomResponse<List<TEntity>?>>> GetAllAsync([FromQuery] SieveModel sieveModel, CancellationToken cancellationToken)
    {
        var result = await _business.GetAllAsync(sieveModel, cancellationToken);

        var response = new CustomResponse<List<TEntity>?>
        {
            Data = result,
            HttpStatusCode = result is null ? HttpStatusCode.InternalServerError : HttpStatusCode.OK,
            IsSuccess = result is not null
        };

        return StatusCode((int)response.HttpStatusCode, result);
    }

    [HttpGet]
    [Route("id/{id:int}")]
    public async Task<ActionResult<CustomResponse<TEntity?>>> GetByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _business.GetByIdAsync(id, cancellationToken);

        var response = new CustomResponse<TEntity?>
        {
            Data = result,
            HttpStatusCode = result is null ? HttpStatusCode.InternalServerError : HttpStatusCode.OK,
            IsSuccess = result is not null
        };

        return StatusCode((int)response.HttpStatusCode, result);
    }

    [HttpGet]
    [Route("guid/{guid:guid}")]
    public async Task<ActionResult<CustomResponse<TEntity?>>> GetByGuidAsync([FromRoute] Guid guid, CancellationToken cancellationToken)
    {
        var result = await _business.GetByGuidAsync(guid, cancellationToken);

        var response = new CustomResponse<TEntity?>
        {
            Data = result,
            HttpStatusCode = result is null ? HttpStatusCode.InternalServerError : HttpStatusCode.OK,
            IsSuccess = result is not null
        };

        return StatusCode((int)response.HttpStatusCode, result);
    }

    [HttpDelete]
    [Route("id/{id:int}")]
    public async Task<ActionResult<CustomResponse<TEntity?>>> DeleteByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _business.DeleteByIdAsync(id, cancellationToken);

        var response = new CustomResponse<TEntity?>
        {
            Data = result,
            HttpStatusCode = result is null ? HttpStatusCode.InternalServerError : HttpStatusCode.OK,
            IsSuccess = result is not null
        };

        return StatusCode((int)response.HttpStatusCode, result);
    }

    [HttpDelete]
    [Route("guid/{guid:guid}")]
    public async Task<ActionResult<CustomResponse<TEntity?>>> DeleteByGuidAsync([FromRoute] Guid guid, CancellationToken cancellationToken)
    {
        var result = await _business.DeleteByGuidAsync(guid, cancellationToken);

        var response = new CustomResponse<TEntity?>
        {
            Data = result,
            HttpStatusCode = result is null ? HttpStatusCode.InternalServerError : HttpStatusCode.OK,
            IsSuccess = result is not null
        };

        return StatusCode((int)response.HttpStatusCode, result);
    }

    [HttpPut]
    public async Task<ActionResult<CustomResponse<TEntity?>>> UpdateAsync([FromBody] TDto dto, CancellationToken
        cancellationToken)
    {
        var result = await _business.UpdateAsync(dto, cancellationToken);

        var response = new CustomResponse<TEntity?>
        {
            Data = result,
            HttpStatusCode = result is null ? HttpStatusCode.InternalServerError : HttpStatusCode.OK,
            IsSuccess = result is not null
        };

        return StatusCode((int)response.HttpStatusCode, result);
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