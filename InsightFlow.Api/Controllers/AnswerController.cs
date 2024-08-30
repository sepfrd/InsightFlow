using System.ComponentModel.DataAnnotations;
using InsightFlow.Business.Interfaces;
using InsightFlow.Common.Constants;
using InsightFlow.Common.Dtos;
using InsightFlow.Common.Dtos.CustomResponses;
using InsightFlow.Common.Dtos.Requests;
using InsightFlow.Model.Entities;
using InsightFlow.Model.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Sieve.Models;

namespace InsightFlow.Api.Controllers;

[ApiController]
[Route("api/answers", Name = "Answers")]
public class AnswerController : ControllerBase
{
    private readonly IAnswerBusiness _answerBusiness;

    public AnswerController(IAnswerBusiness answerBusiness)
    {
        _answerBusiness = answerBusiness;
    }

    [HttpGet]
    [Authorize(ApplicationConstants.AdminPolicyName)]
    public async Task<ActionResult<CustomResponse<List<Answer>>>> GetAllAnswersAsync([FromQuery] SieveModel sieveModel, CancellationToken cancellationToken)
    {
        var result = await _answerBusiness.GetAllAnswersAsync(sieveModel, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpGet]
    [Route("id/{answerId:int}")]
    [Authorize(ApplicationConstants.AdminPolicyName)]
    public async Task<ActionResult<CustomResponse<AnswerDto>>> GetAnswerByIdAsync([FromRoute] int answerId, CancellationToken cancellationToken)
    {
        var result = await _answerBusiness.GetAnswerByIdAsync(answerId, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpGet]
    [Route("guid/{answerGuid:guid}")]
    public async Task<ActionResult<CustomResponse<AnswerDto>>> GetAnswerByGuidAsync([FromRoute] Guid answerGuid, CancellationToken cancellationToken)
    {
        var result = await _answerBusiness.GetAnswerByGuidAsync(answerGuid, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpGet]
    [Route("current-user")]
    [Authorize(ApplicationConstants.UserPolicyName)]
    public async Task<ActionResult<CustomResponse<AnswerDto>>> GetCurrentUserAnswerDtosAsync(
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize,
        CancellationToken cancellationToken)
    {
        var result = await _answerBusiness.GetCurrentUserAnswerDtosAsync(pageNumber, pageSize, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpPut]
    [Authorize(ApplicationConstants.UserPolicyName)]
    [Route("{answerGuid:guid}")]
    public async Task<ActionResult<CustomResponse<AnswerDto>>> UpdateAnswerByGuidAsync(
        [FromRoute] Guid answerGuid,
        [FromBody] UpdateAnswerRequestDto requestDto,
        CancellationToken cancellationToken)
    {
        var result = await _answerBusiness.UpdateAnswerAsync(answerGuid, requestDto, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpPut]
    [Authorize(ApplicationConstants.AdminPolicyName)]
    [Route("{answerId:int}/state")]
    public async Task<ActionResult<CustomResponse<Answer>>> UpdateAnswerStateAsync(
        [FromRoute] int answerId,
        [FromBody] [EnumDataType(typeof(BaseEntityState))]
        BaseEntityState newState,
        CancellationToken cancellationToken)
    {
        var result = await _answerBusiness.UpdateAnswerStateAsync(answerId, newState, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpDelete]
    [Route("id/{answerId:int}")]
    [Authorize(ApplicationConstants.AdminPolicyName)]
    public async Task<ActionResult<CustomResponse>> DeleteAnswerByIdAsync(int answerId, CancellationToken cancellationToken)
    {
        var result = await _answerBusiness.DeleteAnswerByIdAsync(answerId, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpDelete]
    [Route("guid/{answerGuid:guid}")]
    [Authorize(ApplicationConstants.UserPolicyName)]
    public async Task<ActionResult<CustomResponse>> DeleteAnswerByGuidAsync(Guid answerGuid, CancellationToken cancellationToken)
    {
        var result = await _answerBusiness.DeleteAnswerByGuidAsync(answerGuid, cancellationToken);

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