using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedditMockup.Api.Base;
using RedditMockup.Business.Contracts;
using RedditMockup.Business.PublicBusinesses;
using RedditMockup.Common.Constants;
using RedditMockup.Common.Dtos;

namespace RedditMockup.Api.PublicControllers;

[Route("api/public/questions")]
public class PublicQuestionController : PublicBaseController<QuestionDto>
{
    private readonly PublicQuestionBusiness _publicQuestionBusiness;

    public PublicQuestionController(IPublicBaseBusiness<QuestionDto> questionDtoBaseBusiness) : base(questionDtoBaseBusiness) =>
        _publicQuestionBusiness = (PublicQuestionBusiness)questionDtoBaseBusiness;

    [HttpGet]
    [Route("guid/{guid:guid}/answers")]
    public async Task<ActionResult<CustomResponse<List<AnswerDto>>>> GetAnswersByQuestionGuidAsync([FromRoute] Guid guid, CancellationToken cancellationToken)
    {
        var result = await _publicQuestionBusiness.GetAnswersByQuestionGuidAsync(guid, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpGet]
    [Route("guid/{guid:guid}/votes")]
    public async Task<ActionResult<CustomResponse<List<VoteDto>>>> GetVotesByQuestionGuidAsync([FromRoute] Guid guid, CancellationToken cancellationToken)
    {
        var result = await _publicQuestionBusiness.GetVotesByQuestionGuidAsync(guid, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpPost]
    [Route("guid/{guid:guid}/votes")]
    [Authorize(ApplicationConstants.UserPolicyName)]
    public async Task<ActionResult<CustomResponse>> SubmitVoteAsync([FromRoute] Guid guid, [FromBody] bool kind, CancellationToken cancellationToken)
    {
        var result = await _publicQuestionBusiness.SubmitVoteAsync(guid, kind, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }
}