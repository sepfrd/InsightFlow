using InsightFlow.Api.Base;
using InsightFlow.Business.Businesses.AdminBusinesses;
using InsightFlow.Business.Contracts;
using InsightFlow.Common.Dtos;
using InsightFlow.Common.Dtos.CustomResponses;
using InsightFlow.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InsightFlow.Api.Controllers.AdminControllers;

[Route("api/admin/questions", Name = "Admin - Questions")]
public class AdminQuestionController : AdminBaseController<Question, QuestionDto>
{
    private readonly AdminQuestionBusiness _business;

    public AdminQuestionController(IAdminBaseBusiness<Question, QuestionDto> business) : base(business)
    {
        _business = (AdminQuestionBusiness)business;
    }

    [HttpGet]
    [Route("{guid:guid}/answers")]
    public async Task<ActionResult<CustomResponse<List<Answer>>>> GetAnswersByQuestionGuidAsync(
        [FromRoute] Guid guid,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        CancellationToken cancellationToken)
    {
        pageNumber ??= 1;
        pageSize ??= 10;

        var result = await _business.GetAnswersByQuestionGuidAsync(
            guid,
            pageNumber.Value,
            pageSize.Value,
            cancellationToken);

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