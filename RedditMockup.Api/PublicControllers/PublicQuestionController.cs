using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedditMockup.Api.Base;
using RedditMockup.Business.Contracts;
using RedditMockup.Business.PublicBusinesses;
using RedditMockup.Common.Dtos;
using RedditMockup.Model.Entities;

namespace RedditMockup.Api.PublicControllers;

[Route("public/questions")]
public class PublicQuestionController : PublicBaseController<Question, QuestionDto>
{
    #region [Fields]

    private readonly PublicQuestionBusiness _publicQuestionBusiness;

    #endregion

    #region [Constructor]

    public PublicQuestionController(IPublicBaseBusiness<Question, QuestionDto> questionDtoBaseBusiness) : base(questionDtoBaseBusiness) =>
        _publicQuestionBusiness = (PublicQuestionBusiness)questionDtoBaseBusiness;

    #endregion

    [HttpGet]
    [Route("guid/{guid}/answers")]
    public async Task<CustomResponse<List<AnswerDto>>> GetAnswersByQuestionGuidAsync([FromRoute] Guid guid, CancellationToken cancellationToken) =>
        await _publicQuestionBusiness.GetAnswersByQuestionGuidAsync(guid, cancellationToken);

    [HttpGet]
    [Route("guid/{guid}/votes")]
    public async Task<CustomResponse<List<VoteDto>>> GetVotesByQuestionGuidAsync([FromRoute] Guid guid, CancellationToken cancellationToken) =>
        await _publicQuestionBusiness.GetVotesByQuestionGuidAsync(guid, cancellationToken);

    [Authorize]
    [HttpPost]
    [Route("guid/{guid}/votes")]
    public async Task<CustomResponse> SubmitVoteAsync([FromRoute] Guid guid, [FromBody] bool kind, CancellationToken cancellationToken) =>
        await _publicQuestionBusiness.SubmitVoteAsync(guid, kind, cancellationToken);
}