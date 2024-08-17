using System.Net;
using AutoMapper;
using InsightFlow.Business.Base;
using InsightFlow.Business.Businesses.AdminBusinesses;
using InsightFlow.Business.Contracts;
using InsightFlow.Common.Dtos;
using InsightFlow.Common.Dtos.CustomResponses;
using InsightFlow.Model.Entities;

namespace InsightFlow.Business.Businesses;

public class QuestionBusiness : BaseBusiness<Question, QuestionDto>
{
    private readonly AdminQuestionBusiness _adminQuestionBusiness;
    private readonly IMapper _mapper;

    public QuestionBusiness(IAdminBaseBusiness<Question, QuestionDto> questionBusiness, IMapper mapper) : base(questionBusiness, mapper)
    {
        _adminQuestionBusiness = (AdminQuestionBusiness)questionBusiness;
        _mapper = mapper;
    }

    public async Task<PagedCustomResponse<List<AnswerDto>>> GetAnswersByQuestionGuidAsync(
        Guid questionGuid,
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var answersResponse = await _adminQuestionBusiness.GetAnswersByQuestionGuidAsync(
            questionGuid,
            pageNumber > 0 ? pageNumber : 1,
            pageSize >= 10 && pageSize <= 100 ? pageSize : 10,
            cancellationToken);

        if (!answersResponse.IsSuccess)
        {
            return PagedCustomResponse<List<AnswerDto>>.CreateUnsuccessfulResponse(answersResponse.HttpStatusCode, answersResponse.Message);
        }

        var answerDtos = _mapper.Map<List<AnswerDto>>(answersResponse.Data);

        return PagedCustomResponse<List<AnswerDto>>.CreateSuccessfulResponse(
            answerDtos,
            null,
            HttpStatusCode.OK,
            answersResponse.TotalCount,
            answerDtos.Count,
            answersResponse.PageNumber,
            answersResponse.PageSize);
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