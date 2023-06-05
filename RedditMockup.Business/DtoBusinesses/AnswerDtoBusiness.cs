using AutoMapper;
using RedditMockup.Business.Base;
using RedditMockup.Business.Contracts;
using RedditMockup.Business.EntityBusinesses;
using RedditMockup.Common.Dtos;
using RedditMockup.Model.Entities;
using System.Net;

namespace RedditMockup.Business.DtoBusinesses;

public class AnswerDtoBusiness : DtoBaseBusiness<Answer, AnswerDto>
{
    #region [Fields]

    private readonly IMapper _mapper;

    private readonly AnswerBusiness _answerBusiness;

    #endregion

    #region [Constructor]

    public AnswerDtoBusiness(IBaseBusiness<Answer, AnswerDto> answerBusiness, IMapper mapper) : base(answerBusiness, mapper)
    {
        _mapper = mapper;
        _answerBusiness = (AnswerBusiness)answerBusiness;
    }

    #endregion

    #region [Methods]

    public async Task<CustomResponse<IEnumerable<AnswerDto>>> GetAnswersByQuestionGuidAsync(Guid questionGuid, CancellationToken cancellationToken = default)
    {
        var answersResponse = await _answerBusiness.GetAnswersByQuestionGuidAsync(questionGuid, cancellationToken);

        if (!answersResponse.IsSuccess)
        {
            return new CustomResponse<IEnumerable<AnswerDto>>
            {
                IsSuccess = answersResponse.IsSuccess,
                Message = answersResponse.Message,
                HttpStatusCode = answersResponse.HttpStatusCode
            };
        }

        var answerDtos = _mapper.Map<IEnumerable<AnswerDto>>(answersResponse.Data);

        return new CustomResponse<IEnumerable<AnswerDto>>
        {
            Data = answerDtos,
            IsSuccess = true,
            HttpStatusCode = HttpStatusCode.OK
        };
    }

    public async Task<CustomResponse<IEnumerable<VoteDto>>> GetVotesByAnswerGuidAsync(Guid answerGuid, CancellationToken cancellationToken = default)
    {
        var votesResponse = await _answerBusiness.GetVotesByAnswerGuidAsync(answerGuid, cancellationToken);

        if (!votesResponse.IsSuccess)
        {
            return new CustomResponse<IEnumerable<VoteDto>>
            {
                IsSuccess = votesResponse.IsSuccess,
                Message = votesResponse.Message,
                HttpStatusCode = votesResponse.HttpStatusCode
            };
        }

        var voteDtos = _mapper.Map<IEnumerable<VoteDto>>(votesResponse.Data);

        return new CustomResponse<IEnumerable<VoteDto>>
        {
            Data = voteDtos,
            IsSuccess = true,
            HttpStatusCode = HttpStatusCode.OK
        };
    }

    #endregion
}