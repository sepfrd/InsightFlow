using InsightFlow.Api.Base;
using InsightFlow.Business.Businesses.AdminBusinesses;
using InsightFlow.Business.Contracts;
using InsightFlow.Common.Dtos;
using InsightFlow.Common.Dtos.CustomResponses;
using InsightFlow.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InsightFlow.Api.Controllers.AdminControllers;

[Route("api/admin/answers", Name = "Admin - Answers")]
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