using AutoMapper;
using RedditMockup.Business.Base;
using RedditMockup.Business.Contracts;
using RedditMockup.Business.EntityBusinesses;
using RedditMockup.Common.Dtos;
using RedditMockup.Model.Entities;
using System.Net;

namespace RedditMockup.Business.DtoBusinesses;

public class QuestionDtoBusiness : DtoBaseBusiness<QuestionDto, Question>
{
    #region [Constructor]

    public QuestionDtoBusiness(IMapper mapper) : base(mapper)
    {
    }

    #endregion

    #region [Methods]

    public async Task<CustomResponse> SubmitVoteAsync(int id, bool kind, CancellationToken cancellationToken = new()) =>
        await ((QuestionBusiness)BaseBusiness).SubmitVoteAsync(id, kind, cancellationToken);


    public async Task<CustomResponse<IEnumerable<VoteDto>>> LoadVotesAsync(int id, CancellationToken cancellationToken = new())
    {
        var votes = await ((QuestionBusiness)BaseBusiness).LoadVotesAsync(id, cancellationToken);

        if (votes.Data is null)
        {
            return new()
            {
                IsSuccess = votes.IsSuccess,
                Message = votes.Message,
                HttpStatusCode = votes.HttpStatusCode
            };
        }

        var voteDtos = Mapper.Map<IEnumerable<VoteDto>>(votes);

        return new()
        {
            Data = voteDtos,
            IsSuccess = true,
            HttpStatusCode = HttpStatusCode.OK
        };
    }

    #endregion
}
