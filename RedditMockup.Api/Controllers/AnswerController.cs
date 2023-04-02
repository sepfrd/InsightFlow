using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedditMockup.Api.Base;
using RedditMockup.Business.Businesses;
using RedditMockup.Business.Contracts;
using RedditMockup.Common.Dtos;
using RedditMockup.Model.Entities;

namespace RedditMockup.Api.Controllers;

public class AnswerController : BaseController<Answer>
{
    private readonly AnswerBusiness _answerBusiness;

    public AnswerController(IBaseBusiness<Answer> business) : base(business) =>
        _answerBusiness = (AnswerBusiness)business;

    [HttpGet]
    [Route("AnswersByQuestionId")]
    public async Task<CustomResponse?> GetAnswersByQuestionIdAsync(int questionId, CancellationToken cancellationToken) =>
        await _answerBusiness.LoadAnswersByQuestionIdAsync(questionId, cancellationToken);

    [Authorize]
    [HttpPost]
    [Route("SubmitVote")]
    public async Task<CustomResponse?> SubmitVoteAsync(int answerId, bool kind, CancellationToken cancellationToken) =>
    await _answerBusiness.SubmitVoteAsync(answerId, kind, cancellationToken);

    [HttpGet]
    [Route("AnswerVotes")]
    public async Task<CustomResponse?> GetVotesAsync(int answerId, CancellationToken cancellationToken) =>
        await _answerBusiness.LoadVotesAsync(answerId, cancellationToken);
}