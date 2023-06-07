using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RedditMockup.Business.Base;
using RedditMockup.Common.Dtos;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.DataAccess.Repositories;
using RedditMockup.Model.Entities;
using System.Net;

namespace RedditMockup.Business.DomainEntityBusinesses;

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

    public async Task<CustomResponse<List<QuestionVote>>> GetVotesByQuestionGuidAsync(Guid questionGuid, CancellationToken cancellationToken = default)
    {
        var question = await _questionRepository.GetByGuidAsync(questionGuid,
            questions => questions
                .Include(question => question.Votes)
                .Include(question => question.Answers), cancellationToken);

        if (question is null)
        {
            return CustomResponse<List<QuestionVote>>.CreateUnsuccessfulResponse(HttpStatusCode.NotFound, $"No question found with guid of {questionGuid}");
        }

        var votes = question.Votes!.ToList();

        return CustomResponse<List<QuestionVote>>.CreateSuccessfulResponse(votes);
    }

    public async Task<CustomResponse> SubmitVoteAsync(Guid questionGuid, bool kind, CancellationToken cancellationToken = default)
    {
        var question = await _questionRepository.GetByGuidAsync(questionGuid,
            questions => questions.Include(question => question.User)
                .Include(question => question.Votes),
            cancellationToken);

        if (question is null)
        {
            return CustomResponse.CreateUnsuccessfulResponse(HttpStatusCode.NotFound, $"No question found with guid of {questionGuid}");
        }

        if (question.User is null)
        {
            return CustomResponse.CreateUnsuccessfulResponse(HttpStatusCode.InternalServerError);
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

        var voteResult = await _questionRepository.SubmitVoteAsync(vote, cancellationToken);

        if (voteResult is null)
        {
            return CustomResponse.CreateUnsuccessfulResponse(HttpStatusCode.InternalServerError);
        }

        await _unitOfWork.CommitAsync(cancellationToken);

        return CustomResponse.CreateSuccessfulResponse($"{(kind ? "Up" : "Down")}vote submitted");
    }

    #endregion
}