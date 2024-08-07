using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedditMockup.Api.Base;
using RedditMockup.Business.Contracts;
using RedditMockup.Business.PublicBusinesses;
using RedditMockup.Common.Constants;
using RedditMockup.Common.Dtos;

namespace RedditMockup.Api.PublicControllers;

[Route("api/public/answers")]
public class PublicAnswerController : PublicBaseController<AnswerDto>
{
    private readonly PublicAnswerBusiness _publicAnswerBusiness;

    public PublicAnswerController(IPublicBaseBusiness<AnswerDto> publicBaseBusiness) : base(publicBaseBusiness) =>
        _publicAnswerBusiness = (PublicAnswerBusiness)publicBaseBusiness;

    [HttpGet]
    [Route("guid/{guid:guid}/votes")]
    public async Task<ActionResult<CustomResponse<List<VoteDto>>>> GetVotesByAnswerGuidAsync([FromRoute] Guid guid, CancellationToken cancellationToken)
    {
        var result = await _publicAnswerBusiness.GetVotesByAnswerGuidAsync(guid, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpPost]
    [Route("guid/{guid:guid}/votes")]
    [Authorize(ApplicationConstants.UserPolicyName)]
    public async Task<ActionResult<CustomResponse>> SubmitVoteAsync([FromRoute] Guid guid, [FromBody] bool kind, CancellationToken cancellationToken)
    {
        var result = await _publicAnswerBusiness.SubmitVoteAsync(guid, kind, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }
}