using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedditMockup.Api.Base;
using RedditMockup.Business.Contracts;
using RedditMockup.Business.PublicBusinesses;
using RedditMockup.Common.Dtos;
using RedditMockup.Model.Entities;

namespace RedditMockup.Api.PublicControllers;

public class PublicAnswerController : PublicBaseController<Answer, AnswerDto>
{
    #region [Fields]

    private readonly PublicAnswerBusiness _publicAnswerBusiness;

    #endregion

    #region [Constructor]

    public PublicAnswerController(IPublicBaseBusiness<Answer, AnswerDto> publicBaseBusiness) : base(publicBaseBusiness) =>
        _publicAnswerBusiness = (PublicAnswerBusiness)publicBaseBusiness;

    #endregion

    #region [Methods]

    [HttpGet]
    public async Task<CustomResponse<List<AnswerDto>>> GetAnswersByQuestionGuidAsync(Guid questionGuid, CancellationToken cancellationToken) =>
        await _publicAnswerBusiness.GetAnswersByQuestionGuidAsync(questionGuid, cancellationToken);

    [HttpGet]
    public async Task<CustomResponse<List<VoteDto>>> GetVotesByAnswerGuidAsync(Guid answerGuid, CancellationToken cancellationToken) =>
        await _publicAnswerBusiness.GetVotesByAnswerGuidAsync(answerGuid, cancellationToken);

    [Authorize]
    [HttpPost]
    public async Task<CustomResponse> SubmitVoteAsync(Guid answerGuid, bool kind, CancellationToken cancellationToken) =>
        await _publicAnswerBusiness.SubmitVoteAsync(answerGuid, kind, cancellationToken);

    #endregion
}