using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedditMockup.Api.Base;
using RedditMockup.Business.Contracts;
using RedditMockup.Business.DtoBusinesses;
using RedditMockup.Common.Dtos;
using RedditMockup.ExternalService.RabbitMQService.Contracts;

namespace RedditMockup.Api.PublicControllers;

public class QuestionController : PublicBaseController<QuestionDto>
{
    #region [Fields]

    private readonly QuestionDtoBusiness _questionDtoBusiness;

    private readonly IMessageBusClient _messageBusClient;

    #endregion

    #region [Constructor]

    public QuestionController(IDtoBaseBusiness<QuestionDto> questionDtoBaseBusiness, IMessageBusClient messageBusClient) : base(questionDtoBaseBusiness)
    {
        _questionDtoBusiness = (QuestionDtoBusiness)questionDtoBaseBusiness;

        _messageBusClient = messageBusClient;
    }

    #endregion

    [HttpGet]
    [Route("QuestionVotes")]
    public async Task<CustomResponse<IEnumerable<VoteDto>>> GetVotesByQuestionIdAsync(int questionId, CancellationToken cancellationToken) =>
       await _questionDtoBusiness.GetVotesByQuestionIdAsync(questionId, cancellationToken);

    [Authorize]
    [HttpPost]
    [Route("SubmitVote")]
    public async Task<CustomResponse?> SubmitVoteAsync(int questionId, bool kind, CancellationToken cancellationToken) =>
        await _questionDtoBusiness.SubmitVoteAsync(questionId, kind, cancellationToken);


    // ------------------------------------------------------------------------>

    [HttpPost]
    [Route("[action]")]
    public async Task CreateAndPublishAsync(QuestionDto questionDto, CancellationToken cancellationToken)
    {
        await CreateAsync(questionDto, cancellationToken);

        var questionPublishedDto = new QuestionPublishedDto
        {
            Title = questionDto.Title,
            Description = questionDto.Description,
            Event = "Question_Published"
        };

        _messageBusClient.PublishNewQuestion(questionPublishedDto);

    }



    // <-----------------------------------------------------------------------

}

