using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedditMockup.Api.Base;
using RedditMockup.Business.Businesses;
using RedditMockup.Business.Contracts;
using RedditMockup.Common.Constants;
using RedditMockup.Common.Dtos;

namespace RedditMockup.Api.Controllers;

[Route("api/answers")]
public class AnswerController : BaseController<AnswerDto>
{
    private readonly AnswerBusiness _answerBusiness;

    public AnswerController(IBaseBusiness<AnswerDto> baseBusiness) : base(baseBusiness) =>
        _answerBusiness = (AnswerBusiness)baseBusiness;

    [HttpGet]
    [Route("guid/{guid:guid}/votes")]
    public async Task<ActionResult<CustomResponse<List<VoteDto>>>> GetVotesByAnswerGuidAsync([FromRoute] Guid guid, CancellationToken cancellationToken)
    {
        var result = await _answerBusiness.GetVotesByAnswerGuidAsync(guid, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpPost]
    [Route("guid/{guid:guid}/votes")]
    [Authorize(ApplicationConstants.UserPolicyName)]
    public async Task<ActionResult<CustomResponse>> SubmitVoteAsync([FromRoute] Guid guid, [FromBody] bool kind, CancellationToken cancellationToken)
    {
        var result = await _answerBusiness.SubmitVoteAsync(guid, kind, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }
}