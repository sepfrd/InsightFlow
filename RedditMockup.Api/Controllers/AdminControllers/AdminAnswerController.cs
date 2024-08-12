using Microsoft.AspNetCore.Mvc;
using RedditMockup.Api.Base;
using RedditMockup.Business.Businesses.AdminBusinesses;
using RedditMockup.Business.Contracts;
using RedditMockup.Common.Dtos;
using RedditMockup.Model.Entities;

namespace RedditMockup.Api.Controllers.AdminControllers;

[Route("api/admin/answers")]
public class AdminAnswerController : AdminBaseController<Answer, AnswerDto>
{
    private readonly AdminAnswerBusiness _business;

    public AdminAnswerController(IAdminBaseBusiness<Answer, AnswerDto> business) : base(business) =>
        _business = (AdminAnswerBusiness)business;

    [HttpGet]
    [Route("{guid:guid}/votes")]
    public async Task<ActionResult<CustomResponse<List<AnswerVote>>>> GetVotesByAnswerGuidAsync([FromRoute] Guid guid, CancellationToken cancellationToken)
    {
        var result = await _business.GetVotesByAnswerGuidAsync(guid, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpPost]
    [Route("{guid:guid}/votes")]
    public async Task<ActionResult<CustomResponse>> SubmitVoteAsync([FromRoute] Guid guid, [FromBody] bool kind, CancellationToken cancellationToken)
    {
        var result = await _business.SubmitVoteAsync(guid, kind, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }
}