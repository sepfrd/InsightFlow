using Microsoft.AspNetCore.Mvc;
using RedditMockup.Api.Base;
using RedditMockup.Business.Contracts;
using RedditMockup.Business.DomainEntityBusinesses;
using RedditMockup.Common.Dtos;
using RedditMockup.Model.Entities;

namespace RedditMockup.Api.Controllers;

public class AnswerController : BaseController<Answer, AnswerDto>
{
    #region [Fields]

    private readonly AnswerBusiness _business;

    #endregion

    #region [Constructor]

    public AnswerController(IBaseBusiness<Answer, AnswerDto> business) : base(business) =>
        _business = (AnswerBusiness)business;

    #endregion

    #region [Methods]

    [HttpGet]
    [Route("AnswersByQuestionId")]
    public async Task<CustomResponse<List<Answer>>> GetAnswersByQuestionGuidAsync(Guid questionGuid, CancellationToken cancellationToken) =>
        await _business.GetAnswersByQuestionGuidAsync(questionGuid, cancellationToken);

    [HttpGet]
    [Route("Votes")]
    public async Task<CustomResponse<List<AnswerVote>>> GetVotesByAnswerGuidAsync(Guid answerGuid, CancellationToken cancellationToken) =>
        await _business.GetVotesByAnswerGuidAsync(answerGuid, cancellationToken);

    [HttpPost]
    public async Task<CustomResponse> SubmitVoteAsync(Guid answerGuid, bool kind, CancellationToken cancellationToken) =>
        await _business.SubmitVoteAsync(answerGuid, kind, cancellationToken);

    #endregion
}