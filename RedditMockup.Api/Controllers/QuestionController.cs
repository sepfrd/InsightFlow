using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedditMockup.Api.Base;
using RedditMockup.Business.Businesses;
using RedditMockup.Business.Contracts;
using RedditMockup.Common.Constants;
using RedditMockup.Common.Dtos;

namespace RedditMockup.Api.Controllers;

[Route("api/questions")]
public class QuestionController : BaseController<QuestionDto>
{
    private readonly QuestionBusiness _questionBusiness;

    public QuestionController(IBaseBusiness<QuestionDto> questionDtoBaseBusiness) : base(questionDtoBaseBusiness) =>
        _questionBusiness = (QuestionBusiness)questionDtoBaseBusiness;

    [HttpGet]
    [Route("guid/{guid:guid}/answers")]
    public async Task<ActionResult<CustomResponse<List<AnswerDto>>>> GetAnswersByQuestionGuidAsync([FromRoute] Guid guid, CancellationToken cancellationToken)
    {
        var result = await _questionBusiness.GetAnswersByQuestionGuidAsync(guid, cancellationToken);

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