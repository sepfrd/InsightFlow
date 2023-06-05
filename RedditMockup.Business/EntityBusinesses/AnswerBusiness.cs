using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RedditMockup.Business.Base;
using RedditMockup.Common.Dtos;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.DataAccess.Repositories;
using RedditMockup.Model.Entities;
using Sieve.Models;
using System.Net;

namespace RedditMockup.Business.EntityBusinesses;

public class AnswerBusiness : BaseBusiness<Answer, AnswerDto>
{
    #region [Fields]

    private readonly AnswerRepository _answerRepository;

    private readonly IUnitOfWork _unitOfWork;

    #endregion

    #region [Constructor]

    protected AnswerBusiness(IUnitOfWork unitOfWork, IBaseRepository<Answer> repository, IMapper mapper) : base(unitOfWork, unitOfWork.AnswerRepository!, mapper)
    {
        _answerRepository = unitOfWork.AnswerRepository!;
        _unitOfWork = unitOfWork;
    }

    #endregion

    #region [Methods]

    public async Task<CustomResponse<IEnumerable<Answer>>> GetAnswersByQuestionGuidAsync(Guid questionGuid, CancellationToken cancellationToken = default)
    {

        var question = await _unitOfWork.QuestionRepository?.GetByGuidAsync(questionGuid, null, cancellationToken);

        if (question is null)
        {
            return CustomResponse<IEnumerable<Answer>>.CustomNotFoundResponse;
        }

        SieveModel sieveModel = new()
        {
            Filters = $"QuestionId=={question.Id}"
        };

        var answers = await _answerRepository.GetAllAsync(sieveModel, null, cancellationToken);

        if (answers.IsNullOrEmpty())
        {
            return new CustomResponse<IEnumerable<Answer>>
            {
                IsSuccess = false,
                Message = $"No answer found with question guid of {questionGuid}",
                HttpStatusCode = HttpStatusCode.NotFound
            };
        }

        return new CustomResponse<IEnumerable<Answer>>
        {
            Data = answers,
            IsSuccess = true,
            HttpStatusCode = HttpStatusCode.OK
        };
    }

    public async Task<CustomResponse> SubmitVoteAsync(Guid answerGuid, bool kind, CancellationToken cancellationToken = default)
    {
        var answer = await _answerRepository.GetByGuidAsync(answerGuid, 
            answers => answers.Include(answer => answer.User), 
            cancellationToken);

        if (answer is null)
        {
            return new CustomResponse
            {
                IsSuccess = false,
                Message = $"No answer found with guid of {answerGuid}",
                HttpStatusCode = HttpStatusCode.NotFound
            };
        }

        if (answer.User is null)
        {
            return new CustomResponse
            {
                IsSuccess = false,
                HttpStatusCode = HttpStatusCode.InternalServerError
            };
        }

        if (kind)
        {
            answer.User.Score += 1;
        }

        var vote = new AnswerVote
        {
            Kind = kind,
            AnswerId = answer.Id
        };

        await _answerVoteRepository.CreateAsync(vote, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return new CustomResponse
        {
            IsSuccess = true,
            Message = $"{(kind ? "Up" : "Down")}vote submitted",
            HttpStatusCode = HttpStatusCode.OK
        };
    }

    public async Task<CustomResponse<IEnumerable<AnswerVote>>> GetVotesByAnswerIdAsync(int answerId, CancellationToken cancellationToken = default)
    {
        var answer = await _answerRepository.GetByIdAsync(answerId,
            answers => answers.Include(answer => answer.Votes), cancellationToken);

        if (answer is null)
        {
            return new CustomResponse<IEnumerable<AnswerVote>>
            {
                IsSuccess = false,
                Message = $"No answer found with ID of {answerId}",
                HttpStatusCode = HttpStatusCode.NotFound
            };
        }

        var votes = answer.Votes!.ToList();

        return new CustomResponse<IEnumerable<AnswerVote>>
        {
            Data = votes,
            IsSuccess = true,
            HttpStatusCode = HttpStatusCode.OK
        };
    }

    #endregion
}