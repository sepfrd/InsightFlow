using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedditMockup.Api.Base;
using RedditMockup.Business.Contracts;
using RedditMockup.Business.PublicBusinesses;
using RedditMockup.Common.Dtos;
using RedditMockup.ExternalService.RabbitMQService.Contracts;

namespace RedditMockup.Api.PublicControllers;

public class PublicQuestionController : PublicBaseController<QuestionDto>
{
    #region [Fields]

    private readonly PublicQuestionBusiness _publicQuestionBusiness;

    private readonly IMessageBusClient _messageBusClient;

    #endregion

    #region [Constructor]

    public PublicQuestionController(IPublicBaseBusiness<QuestionDto> questionDtoBaseBusiness, IMessageBusClient messageBusClient) : base(questionDtoBaseBusiness)
    {
        _publicQuestionBusiness = (PublicQuestionBusiness)questionDtoBaseBusiness;

        _messageBusClient = messageBusClient;
    }

    #endregion

    [HttpGet]
    public async Task<CustomResponse<List<VoteDto>>> GetVotesByQuestionGuidAsync(Guid questionGuid, CancellationToken cancellationToken) =>
        await _publicQuestionBusiness.GetVotesByQuestionGuidAsync(questionGuid, cancellationToken);

    [Authorize]
    [HttpPost]
    public async Task<CustomResponse> SubmitVoteAsync(Guid questionGuid, bool kind, CancellationToken cancellationToken) =>
        await _publicQuestionBusiness.SubmitVoteAsync(questionGuid, kind, cancellationToken);
    
    // ------------------------------------------------------------------------>

    [HttpPost]
    public async Task CreateAndPublishAsync(QuestionDto questionDto, CancellationToken cancellationToken)
    {
        await CreateAsync(questionDto, cancellationToken);

        var questionPublishedDto = new QuestionPublishedDto
        {
            Title = questionDto.Title,
            Description = questionDto.Description,
            Event = "New Question Created"
        };

        _messageBusClient.PublishNewQuestion(questionPublishedDto);

    }
    
    // <-----------------------------------------------------------------------
}