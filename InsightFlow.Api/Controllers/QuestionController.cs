using InsightFlow.Api.Base;
using InsightFlow.Business.Businesses;
using InsightFlow.Business.Contracts;
using InsightFlow.Common.Constants;
using InsightFlow.Common.Dtos;
using InsightFlow.Common.Dtos.CustomResponses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsightFlow.Api.Controllers;

[Route("api/questions", Name = "Questions")]
public class QuestionController : BaseController<QuestionDto>
{
    private readonly QuestionBusiness _questionBusiness;

    public QuestionController(IBaseBusiness<QuestionDto> questionDtoBaseBusiness) : base(questionDtoBaseBusiness) =>
        _questionBusiness = (QuestionBusiness)questionDtoBaseBusiness;

    [HttpGet]
    [Route("guid/{guid:guid}/answers")]
    public async Task<ActionResult<CustomResponse<List<AnswerDto>>>> GetAnswersByQuestionGuidAsync(
        [FromRoute] Guid guid,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        CancellationToken cancellationToken)
    {
        pageNumber ??= 1;
        pageSize ??= 10;

        var result = await _questionBusiness.GetAnswersByQuestionGuidAsync(
            guid,
            pageNumber.Value,
            pageSize.Value,
            cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpGet]
    [Route("guid/{guid:guid}/votes")]
    public async Task<ActionResult<CustomResponse<List<VoteDto>>>> GetVotesByQuestionGuidAsync([FromRoute] Guid guid, CancellationToken cancellationToken)
    {
        var result = await _questionBusiness.GetVotesByQuestionGuidAsync(guid, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpPost]
    [Route("guid/{guid:guid}/votes")]
    [Authorize(ApplicationConstants.UserPolicyName)]
    public async Task<ActionResult<CustomResponse>> SubmitVoteAsync([FromRoute] Guid guid, [FromBody] bool kind, CancellationToken cancellationToken)
    {
        var result = await _questionBusiness.SubmitVoteAsync(guid, kind, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }
}