using System.Net;
using AutoMapper;
using InsightFlow.Business.Base;
using InsightFlow.Common.Constants;
using InsightFlow.Common.Dtos;
using InsightFlow.Common.Dtos.CustomResponses;
using InsightFlow.DataAccess.Contracts;
using InsightFlow.DataAccess.Repositories;
using InsightFlow.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;

namespace InsightFlow.Business.Businesses.AdminBusinesses;

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
            var message = string.Format(MessageConstants.EntityNotFoundMessage, nameof(User), answerDto.UserGuid);

            return CustomResponse<Question?>.CreateUnsuccessfulResponse(HttpStatusCode.BadRequest, message);
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

    public async override Task<PagedCustomResponse<List<Question>?>> GetAllAsync(SieveModel sieveModel, CancellationToken cancellationToken = default)
    {
        sieveModel.Page ??= 1;
        sieveModel.PageSize ??= 10;

        var result = await _questionRepository.GetAllAsync(
            sieveModel,
            questions => questions.Include(question => question.User),
            cancellationToken);

        var currentCount = result.Entities?.Count ?? 0;

        return PagedCustomResponse<List<Question>?>.CreateSuccessfulResponse(
            result.Entities,
            null,
            HttpStatusCode.OK,
            result.TotalCount,
            currentCount,
            sieveModel.Page.Value,
            sieveModel.PageSize.Value);
    }

    public async Task<PagedCustomResponse<List<Answer>>> GetAnswersByQuestionGuidAsync(
        Guid questionGuid,
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var question = await _unitOfWork.QuestionRepository!.GetByGuidAsync(questionGuid, null, cancellationToken);

        if (question is null)
        {
            var message = string.Format(MessageConstants.EntityNotFoundMessage, nameof(Question), questionGuid);

            return PagedCustomResponse<List<Answer>>.CreateUnsuccessfulResponse(HttpStatusCode.NotFound, message);
        }

        var sieveModel = new SieveModel
        {
            Filters = nameof(Answer.QuestionId) + "==" + question.Id,
            Page = pageNumber > 0 ? pageNumber : 1,
            PageSize = pageSize >= 10 && pageSize <= 100 ? pageSize : 10
        };

        var result = await _unitOfWork.AnswerRepository!.GetAllAsync(sieveModel, null, cancellationToken);

        var currentCount = result.Entities?.Count ?? 0;

        return PagedCustomResponse<List<Answer>>.CreateSuccessfulResponse(
            result.Entities,
            null,
            HttpStatusCode.OK,
            result.TotalCount,
            currentCount,
            sieveModel.Page.Value,
            sieveModel.PageSize.Value);
    }

    public async Task<CustomResponse<List<QuestionVote>>> GetVotesByQuestionGuidAsync(Guid questionGuid, CancellationToken cancellationToken = default)
    {
        var question = await _questionRepository.GetByGuidAsync(
            questionGuid,
            questions =>
                questions
                    .Include(question => question.Votes)
                    .Include(question => question.Answers), cancellationToken);

        if (question is null)
        {
            var message = string.Format(MessageConstants.EntityNotFoundMessage, nameof(Question), questionGuid);

            return CustomResponse<List<QuestionVote>>.CreateUnsuccessfulResponse(HttpStatusCode.NotFound, message);
        }

        var votes = question.Votes!.ToList();

        return CustomResponse<List<QuestionVote>>.CreateSuccessfulResponse(votes);
    }

    public async Task<CustomResponse> SubmitVoteAsync(Guid questionGuid, bool kind, CancellationToken cancellationToken = default)
    {
        var question = await _questionRepository.GetByGuidAsync(
            questionGuid,
            questions =>
                questions
                    .Include(question => question.User)
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