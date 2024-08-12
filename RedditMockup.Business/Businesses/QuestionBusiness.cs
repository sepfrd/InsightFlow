using AutoMapper;
using RedditMockup.Business.Base;
using RedditMockup.Business.Businesses.AdminBusinesses;
using RedditMockup.Business.Contracts;
using RedditMockup.Common.Dtos;
using RedditMockup.Model.Entities;

namespace RedditMockup.Business.Businesses;

public class QuestionBusiness : BaseBusiness<Question, QuestionDto>
{
    private readonly AdminQuestionBusiness _adminQuestionBusiness;
    private readonly IMapper _mapper;

    public QuestionBusiness(IAdminBaseBusiness<Question, QuestionDto> questionBusiness, IMapper mapper) : base(questionBusiness, mapper)
    {
        _adminQuestionBusiness = (AdminQuestionBusiness)questionBusiness;
        _mapper = mapper;
    }

    public async Task<CustomResponse<List<AnswerDto>>> GetAnswersByQuestionGuidAsync(Guid questionGuid, CancellationToken cancellationToken = default)
    {
        var answersResponse = await _adminQuestionBusiness.GetAnswersByQuestionGuidAsync(questionGuid, cancellationToken);

        if (!answersResponse.IsSuccess)
        {
            return CustomResponse<List<AnswerDto>>.CreateUnsuccessfulResponse(answersResponse.HttpStatusCode, answersResponse.Message);
        }

        var answerDtos = _mapper.Map<List<AnswerDto>>(answersResponse.Data);

        return CustomResponse<List<AnswerDto>>.CreateSuccessfulResponse(answerDtos);
    }

    public async Task<CustomResponse<List<VoteDto>>> GetVotesByQuestionGuidAsync(Guid questionGuid, CancellationToken cancellationToken = default)
    {
        var votesResponse = await _adminQuestionBusiness.GetVotesByQuestionGuidAsync(questionGuid, cancellationToken);

        if (!votesResponse.IsSuccess)
        {
            return CustomResponse<List<VoteDto>>.CreateUnsuccessfulResponse(votesResponse.HttpStatusCode, votesResponse.Message);
        }

        var voteDtos = _mapper.Map<List<VoteDto>>(votesResponse.Data);

        return CustomResponse<List<VoteDto>>.CreateSuccessfulResponse(voteDtos);
    }

    public async Task<CustomResponse> SubmitVoteAsync(Guid questionGuid, bool kind, CancellationToken cancellationToken = default) =>
        await _adminQuestionBusiness.SubmitVoteAsync(questionGuid, kind, cancellationToken);
}