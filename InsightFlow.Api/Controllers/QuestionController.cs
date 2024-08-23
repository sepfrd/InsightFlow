using InsightFlow.Business.Contracts;
using InsightFlow.Common.Constants;
using InsightFlow.Common.Dtos;
using InsightFlow.Common.Dtos.CustomResponses;
using InsightFlow.Common.Dtos.Requests;
using InsightFlow.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Sieve.Models;

namespace InsightFlow.Api.Controllers;

[ApiController]
[Route("api/questions", Name = "Questions")]
public class QuestionController : ControllerBase
{
    private readonly IQuestionBusiness _questionBusiness;
    private readonly IAnswerBusiness _answerBusiness;

    public QuestionController(IQuestionBusiness questionBusiness, IAnswerBusiness answerBusiness)
    {
        _questionBusiness = questionBusiness;
        _answerBusiness = answerBusiness;
    }

    [HttpPost]
    [Authorize(ApplicationConstants.UserPolicyName)]
    public async Task<ActionResult<CustomResponse<QuestionDto>>> CreateQuestionAsync([FromBody] CreateQuestionRequestDto requestDto, CancellationToken cancellationToken)
    {
        var result = await _questionBusiness.CreateQuestionAsync(requestDto, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpGet]
    [Authorize(ApplicationConstants.AdminPolicyName)]
    public async Task<ActionResult<CustomResponse<List<Question>>>> GetAllQuestionsAsync([FromQuery] SieveModel sieveModel, CancellationToken cancellationToken)
    {
        var result = await _questionBusiness.GetAllQuestionsAsync(sieveModel, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpGet]
    [Route("guid/{guid:guid}/answers")]
    public async Task<ActionResult<CustomResponse<List<AnswerDto>>>> GetAnswerDtosByQuestionGuidAsync(
        [FromRoute] Guid guid,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        CancellationToken cancellationToken)
    {
        pageNumber ??= 1;
        pageSize ??= 10;

        var result = await _answerBusiness.GetAnswerDtosByQuestionGuidAsync(
            guid,
            pageNumber.Value,
            pageSize.Value,
            cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpGet]
    [Route("id/{questionId:int}")]
    [Authorize(ApplicationConstants.AdminPolicyName)]
    public async Task<ActionResult<CustomResponse<QuestionDto>>> GetQuestionByIdAsync([FromRoute] int questionId, CancellationToken cancellationToken)
    {
        var result = await _questionBusiness.GetQuestionByIdAsync(questionId, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpGet]
    [Route("guid/{questionGuid:guid}")]
    public async Task<ActionResult<CustomResponse<QuestionDto>>> GetQuestionByGuidAsync([FromRoute] Guid questionGuid, CancellationToken cancellationToken)
    {
        var result = await _questionBusiness.GetQuestionByGuidAsync(questionGuid, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpGet]
    [Route("current-user")]
    [Authorize(ApplicationConstants.UserPolicyName)]
    public async Task<ActionResult<CustomResponse<QuestionDto>>> GetCurrentUserQuestionDtosAsync(
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize,
        CancellationToken cancellationToken)
    {
        var result = await _questionBusiness.GetCurrentUserQuestionDtosAsync(pageNumber, pageSize, cancellationToken);

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