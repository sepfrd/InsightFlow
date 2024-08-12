using Microsoft.AspNetCore.Mvc;
using RedditMockup.Api.Base;
using RedditMockup.Business.Businesses.AdminBusinesses;
using RedditMockup.Business.Contracts;
using RedditMockup.Common.Dtos;
using RedditMockup.Model.Entities;

namespace RedditMockup.Api.Controllers.AdminControllers;

[Route("api/admin/questions")]
public class AdminQuestionController : AdminBaseController<Question, QuestionDto>
{
    private readonly AdminQuestionBusiness _business;

    public AdminQuestionController(IAdminBaseBusiness<Question, QuestionDto> business) : base(business)
    {
        _business = (AdminQuestionBusiness)business;
    }

    [HttpGet]
    [Route("{guid:guid}/answers")]
    public async Task<ActionResult<CustomResponse<List<Answer>>>> GetAnswersByQuestionGuidAsync([FromRoute] Guid guid, CancellationToken cancellationToken)
    {
        var result = await _business.GetAnswersByQuestionGuidAsync(guid, cancellationToken);

        return StatusCode((int)result.HttpStatusCode, result);
    }

    [HttpGet]
    [Route("{guid:guid}/votes")]
    public async Task<ActionResult<CustomResponse<List<QuestionVote>>>> GetVotesByQuestionGuidAsync([FromRoute] Guid guid, CancellationToken cancellationToken)
    {
        var result = await _business.GetVotesByQuestionGuidAsync(guid, cancellationToken);
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