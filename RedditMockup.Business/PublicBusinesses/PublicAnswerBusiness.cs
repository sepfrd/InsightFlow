using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RedditMockup.Business.Base;
using RedditMockup.Business.Contracts;
using RedditMockup.Business.DomainEntityBusinesses;
using RedditMockup.Common.Dtos;
using RedditMockup.Model.Entities;
using Sieve.Models;

namespace RedditMockup.Business.PublicBusinesses;

public class PublicAnswerBusiness : PublicBaseBusiness<Answer, AnswerDto>
{
    #region [Fields]

    private readonly IMapper _mapper;

    private readonly AnswerBusiness _answerBusiness;

    #endregion

    #region [Constructor]

    public PublicAnswerBusiness(IBaseBusiness<Answer, AnswerDto> answerBusiness, IMapper mapper) : base(answerBusiness, mapper)
    {
        _mapper = mapper;
        _answerBusiness = (AnswerBusiness)answerBusiness;
    }

    #endregion

    #region [Methods]

    public async Task<CustomResponse<List<AnswerDto>>> GetAnswersByQuestionGuidAsync(Guid questionGuid, CancellationToken cancellationToken = default)
    {
        var answersResponse = await _answerBusiness.GetAnswersByQuestionGuidAsync(questionGuid, cancellationToken);

        if (!answersResponse.IsSuccess)
        {
            return CustomResponse<List<AnswerDto>>.CreateUnsuccessfulResponse(answersResponse.HttpStatusCode, answersResponse.Message);
        }

        var answerDtos = _mapper.Map<List<AnswerDto>>(answersResponse.Data);

        return CustomResponse<List<AnswerDto>>.CreateSuccessfulResponse(answerDtos);
    }

    public async Task<CustomResponse<List<VoteDto>>> GetVotesByAnswerGuidAsync(Guid answerGuid, CancellationToken cancellationToken = default)
    {
        var votesResponse = await _answerBusiness.GetVotesByAnswerGuidAsync(answerGuid, cancellationToken);

        if (!votesResponse.IsSuccess)
        {
            return CustomResponse<List<VoteDto>>.CreateUnsuccessfulResponse(votesResponse.HttpStatusCode, votesResponse.Message);
        }

        var voteDtos = _mapper.Map<List<VoteDto>>(votesResponse.Data);

        return CustomResponse<List<VoteDto>>.CreateSuccessfulResponse(voteDtos);
    }

    public async Task<CustomResponse> SubmitVoteAsync(Guid answerGuid, bool kind, CancellationToken cancellationToken = default) =>
        await _answerBusiness.SubmitVoteAsync(answerGuid, kind, cancellationToken);

    #endregion
}