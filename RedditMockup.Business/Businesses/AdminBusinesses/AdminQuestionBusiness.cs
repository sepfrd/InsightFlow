using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RedditMockup.Business.Base;
using RedditMockup.Common.Dtos;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.DataAccess.Repositories;
using RedditMockup.Model.Entities;
using Sieve.Models;

namespace RedditMockup.Business.Businesses.AdminBusinesses;

public class AdminQuestionBusiness : AdminBaseBusiness<Question, QuestionDto>
{
    private readonly QuestionRepository _questionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AdminQuestionBusiness(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, unitOfWork.QuestionRepository!, mapper)
    {
        _questionRepository = (QuestionRepository)unitOfWork.QuestionRepository!;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async override Task<CustomResponse<Question?>> CreateAsync(QuestionDto answerDto, CancellationToken cancellationToken = default)
    {
        var question = _mapper.Map<Question>(answerDto);

        var user = await _unitOfWork.UserRepository!.GetByGuidAsync(answerDto.UserGuid, null, cancellationToken);

        if (user is null)
        {
            return null;
        }

        question.UserId = user.Id;

        return await CreateAsync(question, cancellationToken);
    }

    public async override Task<CustomResponse<Question?>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var question = await _questionRepository.GetByIdAsync(
            id,
            questions => questions.Include(questionEntity => questionEntity.User),
            cancellationToken);

        if (question is null)
        {
            return CustomResponse<Question?>.CreateUnsuccessfulResponse(HttpStatusCode.NotFound);
        }

        return CustomResponse<Question?>.CreateSuccessfulResponse(question);
    }

    public async override Task<CustomResponse<Question?>> GetByGuidAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        var question = await _questionRepository.GetByGuidAsync(
            guid,
            questions => questions.Include(questionEntity => questionEntity.User),
            cancellationToken);

        if (question is null)
        {
            return CustomResponse<Question?>.CreateUnsuccessfulResponse(HttpStatusCode.NotFound);
        }

        return CustomResponse<Question?>.CreateSuccessfulResponse(question);
    }

    public async override Task<CustomResponse<List<Question>?>> GetAllAsync(SieveModel sieveModel, CancellationToken cancellationToken = default)
    {
        var questions = await _questionRepository.GetAllAsync(sieveModel,
            questions => questions.Include(question => question.User),
            cancellationToken);

        return CustomResponse<List<Question>?>.CreateSuccessfulResponse(questions);
    }

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

        var answers = await _unitOfWork.AnswerRepository!.GetAllAsync(sieveModel, null, cancellationToken);

        if (answers.IsNullOrEmpty())
        {
            return CustomResponse<List<Answer>>.CreateUnsuccessfulResponse(HttpStatusCode.NotFound, $"No answer found with question guid of {questionGuid}");
        }

        return CustomResponse<List<Answer>>.CreateSuccessfulResponse(answers);
    }

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
}