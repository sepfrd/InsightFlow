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

namespace RedditMockup.Business.DomainEntityBusinesses;

public class AnswerBusiness : BaseBusiness<Answer, AnswerDto>
{
    #region [Fields]

    private readonly AnswerRepository _answerRepository;

    private readonly IUnitOfWork _unitOfWork;

    #endregion

    #region [Constructor]

    public AnswerBusiness(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, unitOfWork.AnswerRepository!, mapper)
    {
        _answerRepository = unitOfWork.AnswerRepository!;
        _unitOfWork = unitOfWork;
    }

    #endregion

    #region [Methods]

    public async Task<CustomResponse<List<Answer>>> GetAnswersByQuestionGuidAsync(Guid questionGuid, CancellationToken cancellationToken = default)
    {

        var question = await _unitOfWork.QuestionRepository!.GetByGuidAsync(questionGuid, null, cancellationToken);

        if (question is null)
        {
            return CustomResponse<List<Answer>>.CreateUnsuccessfulResponse(HttpStatusCode.NotFound);
        }

        SieveModel sieveModel = new()
        {
            Filters = $"QuestionId=={question.Id}"
        };

        var answers = await _answerRepository.GetAllAsync(sieveModel, null, cancellationToken);

        if (answers.IsNullOrEmpty())
        {
            return CustomResponse<List<Answer>>.CreateUnsuccessfulResponse(HttpStatusCode.NotFound, $"No answer found with question guid of {questionGuid}");
        }

        return CustomResponse<List<Answer>>.CreateSuccessfulResponse(answers);
    }

    public async Task<CustomResponse> SubmitVoteAsync(Guid answerGuid, bool kind, CancellationToken cancellationToken = default)
    {
        var answer = await _answerRepository.GetByGuidAsync(answerGuid,
            answers => answers.Include(answer => answer.User)
                .Include(answer => answer.Votes),
            cancellationToken);

        if (answer is null)
        {
            return CustomResponse.CreateUnsuccessfulResponse(HttpStatusCode.NotFound, $"No answer found with guid of {answerGuid}");
        }

        if (answer.User is null)
        {
            return CustomResponse.CreateUnsuccessfulResponse(HttpStatusCode.InternalServerError);
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

        await _answerRepository.SubmitVoteAsync(vote, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return CustomResponse.CreateSuccessfulResponse($"{(kind ? "Up" : "Down")}vote submitted");
    }

    public async Task<CustomResponse<List<AnswerVote>>> GetVotesByAnswerGuidAsync(Guid answerGuid, CancellationToken cancellationToken = default)
    {
        var answer = await _answerRepository.GetByGuidAsync(answerGuid,
            answers => answers.Include(answer => answer.Votes), cancellationToken);

        if (answer is null)
        {
            return CustomResponse<List<AnswerVote>>.CreateUnsuccessfulResponse(HttpStatusCode.NotFound);
        }

        var votes = answer.Votes!.ToList();

        return CustomResponse<List<AnswerVote>>.CreateSuccessfulResponse(votes);

    }

    #endregion
}