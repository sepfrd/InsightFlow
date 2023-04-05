using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using RedditMockup.Business.Base;
using RedditMockup.Business.Contracts;
using RedditMockup.Business.EntityBusinesses;
using RedditMockup.Common.Dtos;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.Model.Entities;
using Sieve.Models;
using System.Net;

namespace RedditMockup.Business.DtoBusinesses;

public class AnswerDtoBusiness : DtoBaseBusiness<AnswerDto, Answer>
{
    private readonly IMapper _mapper;

    public AnswerDtoBusiness(IUnitOfWork unitOfWork, IBaseRepository<Answer> repository, IMapper mapper) : base(unitOfWork, repository, mapper)
    {
        _mapper = mapper;
    }
    #region [Constructor]

    #endregion

    #region [Methods]
    public async Task<CustomResponse<IEnumerable<AnswerDto>>> LoadAnswersByQuestionIdAsync(int questionId, CancellationToken cancellationToken = new())
    {
        var answers = await ((AnswerBusiness)BaseBusiness).LoadAnswersByQuestionIdAsync(questionId, cancellationToken);

        if (answers.Data is null)
        {
            return new()
            {
                IsSuccess = answers.IsSuccess,
                Message = answers.Message,
                HttpStatusCode = answers.HttpStatusCode
            };
        }

        var answerDtos = Mapper.Map<IEnumerable<AnswerDto>>(answers.Data);

        return new()
        {
            Data = answerDtos,
            IsSuccess = true,
            HttpStatusCode = HttpStatusCode.OK
        };

    }

    public async Task<CustomResponse?> SubmitVoteAsync(int id, bool kind, CancellationToken cancellationToken = new()) =>
        await ((AnswerBusiness)BaseBusiness).SubmitVoteAsync(id, kind, cancellationToken);

    public async Task<CustomResponse<IEnumerable<VoteDto>>> LoadVotesAsync(int questionId, CancellationToken cancellationToken = new())
    {
        var votes = await ((AnswerBusiness)BaseBusiness).LoadVotesAsync(questionId, cancellationToken);

        if (votes.Data is null)
        {
            return new()
            {
                IsSuccess = votes.IsSuccess,
                Message = votes.Message,
                HttpStatusCode = votes.HttpStatusCode
            };
        }

        var voteDtos = Mapper.Map<IEnumerable<VoteDto>>(votes.Data);

        return new()
        {
            Data = voteDtos,
            IsSuccess = true,
            HttpStatusCode = HttpStatusCode.OK
        };
    }


    #endregion
}
