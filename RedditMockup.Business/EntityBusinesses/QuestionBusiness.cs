using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RedditMockup.Business.Base;
using RedditMockup.Common.Dtos;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.DataAccess.Repositories;
using RedditMockup.Model.Entities;
using System.Net;

namespace RedditMockup.Business.EntityBusinesses;

public class QuestionBusiness : BaseBusiness<Question, QuestionDto>
{
    #region [Fields]

    private readonly QuestionRepository _questionRepository;
    
    private readonly IUnitOfWork _unitOfWork;

    #endregion

    #region [Constructor]

    public QuestionBusiness(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, unitOfWork.QuestionRepository!, mapper)
    {
        _questionRepository = unitOfWork.QuestionRepository!;
        _unitOfWork = unitOfWork;
    }

    #endregion

    #region [Methods]    

    public async Task<CustomResponse<IEnumerable<QuestionVote>>> GetVotesByQuestionGuidAsync(Guid questionGuid, CancellationToken cancellationToken = default)
    {
        var question = await _questionRepository.GetByGuidAsync(questionGuid,
            questions => questions
                .Include(question => question.Votes)
                .Include(question => question.Answers), cancellationToken);

        if (question is null)
        {
            return new CustomResponse<IEnumerable<QuestionVote>>
            {
                IsSuccess = false,
                Message = $"No question found with guid of {questionGuid}",
                HttpStatusCode = HttpStatusCode.NotFound
            };
        }

        var votes = question.Votes!.ToList();

        return new CustomResponse<IEnumerable<QuestionVote>>
        {
            Data = votes,
            IsSuccess = true,
            HttpStatusCode = HttpStatusCode.OK
        };
    }

    public async Task<CustomResponse> SubmitVoteAsync(Guid questionGuid, bool kind, CancellationToken cancellationToken = default)
    {
        var question = await _questionRepository.GetByGuidAsync(questionGuid, 
            questions => questions.Include(question => question.User)
                .Include(question => question.Votes), 
            cancellationToken);

        if (question is null)
        {
            return new CustomResponse
            {
                IsSuccess = false,
                Message = $"No question found with guid of {questionGuid}",
                HttpStatusCode = HttpStatusCode.NotFound
            };
        }

        if (question.User is null)
        {
            return new CustomResponse
            {
                IsSuccess = false,
                HttpStatusCode = HttpStatusCode.InternalServerError
            };
        }

        if (kind)
        {
            question.User.Score += 1;
        }

        var vote = new QuestionVote
        {
            Kind = kind,
            QuestionId = question.Id
        };

        await _questionRepository.SubmitVoteAsync(vote, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return new CustomResponse
        {
            IsSuccess = true,
            Message = $"{(kind ? "Up" : "Down")}vote submitted",
            HttpStatusCode = HttpStatusCode.OK
        };

    }

    #endregion
}