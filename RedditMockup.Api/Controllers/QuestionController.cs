using Microsoft.AspNetCore.Mvc;
using RedditMockup.Api.Base;
using RedditMockup.Business.Contracts;
using RedditMockup.Business.EntityBusinesses;
using RedditMockup.Common.Dtos;
using RedditMockup.Model.Entities;

namespace RedditMockup.Api.Controllers;

public class QuestionController : BaseController<Question, QuestionDto>
{
    private readonly QuestionBusiness _business;

    public QuestionController(IBaseBusiness<Question, QuestionDto> business) : base(business)
    {
        _business = (QuestionBusiness)business;
    }

    [HttpGet]
    [Route("Votes")]
    public async Task<CustomResponse<IEnumerable<QuestionVote>>> GetVotesByQuestionGuidAsync(Guid questionGuid, CancellationToken cancellationToken) =>
         await _business.GetVotesByQuestionGuidAsync(questionGuid, cancellationToken);


    [HttpPost]
    [Route("SubmitVote")]
    public async Task<CustomResponse> SubmitVoteAsync(Guid questionGuid, bool kind, CancellationToken cancellationToken) =>
        await _business.SubmitVoteAsync(questionGuid, kind, cancellationToken);
}