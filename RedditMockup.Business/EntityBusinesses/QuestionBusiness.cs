using RedditMockup.Business.Base;
using RedditMockup.Common.Dtos;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.DataAccess.Repositories;
using RedditMockup.Model.Entities;
using System.Net;

namespace RedditMockup.Business.EntityBusinesses;

public class QuestionBusiness : BaseBusiness<Question>
{
    #region [Fields]

    private readonly QuestionRepository _questionRepository;
    private readonly QuestionVoteRepository _questionVoteRepository;
    private readonly IUnitOfWork _unitOfWork;

    #endregion

    #region [Constructor]

    public QuestionBusiness(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.QuestionRepository!)
    {
        _questionRepository = unitOfWork.QuestionRepository!;
        _questionVoteRepository = unitOfWork.QuestionVoteRepository!;
        _unitOfWork = unitOfWork;
    }

    #endregion

    #region [Methods]

    public async Task<CustomResponse<IEnumerable<QuestionVote>>> GetVotesByQuestionIdAsync(int id, CancellationToken cancellationToken = new())
    {
        var question = await _questionRepository.LoadByIdAsync(id, cancellationToken);

        if (question is null)
        {
            return new()
            {
                IsSuccess = false,
                Message = $"No question found with ID of {id}",
                HttpStatusCode = HttpStatusCode.NotFound
            };
        }

        var votes = question.Votes!.ToList();

        return new()
        {
            Data = votes,
            IsSuccess = true,
            HttpStatusCode = HttpStatusCode.OK
        };
    }

    public async Task<CustomResponse> SubmitVoteAsync(int questionId, bool kind, CancellationToken cancellationToken = new())
    {
        var question = await LoadByIdAsync(questionId, cancellationToken);

        if (question is null)
        {
            return new CustomResponse
            {
                IsSuccess = false,
                Message = $"No question found with ID of {questionId}",
                HttpStatusCode = HttpStatusCode.NotFound
            };
        }

        if (kind)
        {
            question.User!.Score += 1;
        }

        var vote = new QuestionVote
        {
            Kind = kind,
            QuestionId = question.Id
        };

        await _questionVoteRepository.CreateAsync(vote, cancellationToken);

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