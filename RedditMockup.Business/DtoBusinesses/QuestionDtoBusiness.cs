using AutoMapper;
using RedditMockup.Business.Base;
using RedditMockup.Business.Contracts;
using RedditMockup.Business.EntityBusinesses;
using RedditMockup.Common.Dtos;
using RedditMockup.Model.Entities;
using System.Net;

namespace RedditMockup.Business.DtoBusinesses;

public class QuestionDtoBusiness : DtoBaseBusiness<Question, QuestionDto>
{
    #region [Fields]

    private readonly QuestionBusiness _questionBusiness;

    private readonly IMapper _mapper;

    #endregion

    #region [Constructor]

    public QuestionDtoBusiness(IBaseBusiness<Question, QuestionDto> questionBusiness, IMapper mapper) : base(questionBusiness, mapper)
    {
        _questionBusiness = (QuestionBusiness)questionBusiness;

        _mapper = mapper;
    }

    #endregion

    #region [Methods]
    
    public async Task<CustomResponse<IEnumerable<VoteDto>>> GetVotesByQuestionGuidAsync(Guid questionGuid, CancellationToken cancellationToken = default)
    {
        var votesResponse = await _questionBusiness.GetVotesByQuestionGuidAsync(questionGuid, cancellationToken);

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
