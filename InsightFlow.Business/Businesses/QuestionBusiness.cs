using System.Net;
using System.Security.Claims;
using AutoMapper;
using InsightFlow.Business.Interfaces;
using InsightFlow.Common.Constants;
using InsightFlow.Common.Dtos;
using InsightFlow.Common.Dtos.CustomResponses;
using InsightFlow.Common.Dtos.Requests;
using InsightFlow.DataAccess.Interfaces;
using InsightFlow.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;

namespace InsightFlow.Business.Businesses;

public class QuestionBusiness : IQuestionBusiness
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IBaseRepository<Question> _questionRepository;

    public QuestionBusiness(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _questionRepository = unitOfWork.QuestionRepository;
    }

    public async Task<CustomResponse<QuestionDto>> CreateQuestionAsync(CreateQuestionRequestDto requestDto, CancellationToken cancellationToken = default)
    {
        var question = _mapper.Map<Question>(requestDto);

        var userGuid = _httpContextAccessor.HttpContext?.User.FindFirstValue(ApplicationConstants.ExternalIdClaim)!;

        var user = await _unitOfWork.UserRepository.GetByGuidAsync(Guid.Parse(userGuid), null, cancellationToken);

        question.UserId = user!.Id;

        var createdQuestion = await _questionRepository.CreateAsync(question, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        var questionDto = _mapper.Map<QuestionDto>(createdQuestion);

        return CustomResponse<QuestionDto>.CreateSuccessfulResponse(questionDto, null, HttpStatusCode.Created);
    }

    public async Task<CustomResponse<Question>> GetQuestionByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var question = await _questionRepository.GetByIdAsync(
            id,
            questions => questions.Include(questionEntity => questionEntity.User),
            cancellationToken);

        if (question is null)
        {
            return CustomResponse<Question>.CreateUnsuccessfulResponse(HttpStatusCode.NotFound);
        }

        return CustomResponse<Question>.CreateSuccessfulResponse(question);
    }

    public async Task<CustomResponse<QuestionDto>> GetQuestionByGuidAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        var question = await _questionRepository.GetByGuidAsync(
            guid,
            questions => questions.Include(questionEntity => questionEntity.User),
            cancellationToken);

        if (question is null)
        {
            return CustomResponse<QuestionDto>.CreateUnsuccessfulResponse(HttpStatusCode.NotFound);
        }

        var questionDto = _mapper.Map<QuestionDto>(question);

        return CustomResponse<QuestionDto>.CreateSuccessfulResponse(questionDto);
    }

    public async Task<PagedCustomResponse<List<Question>>> GetAllQuestionsAsync(SieveModel sieveModel, CancellationToken cancellationToken = default)
    {
        sieveModel.Page ??= 1;
        sieveModel.PageSize ??= 10;

        var result = await _questionRepository.GetAllAsync(
            sieveModel,
            questions => questions.Include(question => question.User),
            cancellationToken);

        var currentCount = result.Entities?.Count ?? 0;

        return PagedCustomResponse<List<Question>>.CreateSuccessfulResponse(
            result.Entities,
            null,
            HttpStatusCode.OK,
            result.TotalCount,
            currentCount,
            sieveModel.Page.Value,
            sieveModel.PageSize.Value);
    }

    public async Task<PagedCustomResponse<List<QuestionDto>>> GetCurrentUserQuestionDtosAsync(
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var userGuid = _httpContextAccessor.HttpContext?.User.FindFirstValue(ApplicationConstants.ExternalIdClaim)!;

        var user = await _unitOfWork.UserRepository.GetByGuidAsync(
            Guid.Parse(userGuid),
            users => users.Include(user => user.Questions),
            cancellationToken);

        if (user!.Questions.Count == 0)
        {
            return PagedCustomResponse<List<QuestionDto>>.CreateSuccessfulResponse([]);
        }

        pageNumber = pageNumber > 0 ? pageNumber : 1;
        pageSize = pageSize >= 10 && pageSize <= 100 ? pageSize : 10;

        var skipCount = (pageNumber - 1) * pageSize;

        var questions = user
            .Questions
            .OrderByDescending(question => question.Id)
            .Skip(skipCount)
            .Take(pageSize)
            .ToList();

        var questionDtos = _mapper.Map<List<QuestionDto>>(questions);

        return PagedCustomResponse<List<QuestionDto>>.CreateSuccessfulResponse(
            questionDtos,
            null,
            HttpStatusCode.OK,
            user.Questions.Count,
            questionDtos.Count,
            pageNumber,
            pageSize);
    }

    public async Task<CustomResponse<QuestionDto>> UpdateQuestionAsync(
        Guid questionGuid,
        UpdateQuestionRequestDto requestDto,
        CancellationToken cancellationToken = default)
    {
        var question = await _questionRepository.GetByGuidAsync(questionGuid, null, cancellationToken);

        if (question is null)
        {
            var message = string.Format(MessageConstants.EntityNotFoundByGuidMessage, nameof(Question), questionGuid);

            return CustomResponse<QuestionDto>.CreateUnsuccessfulResponse(HttpStatusCode.NotFound, message);
        }

        var userGuid = _httpContextAccessor.HttpContext?.User.FindFirstValue(ApplicationConstants.ExternalIdClaim)!;

        var user = await _unitOfWork.UserRepository.GetByGuidAsync(Guid.Parse(userGuid), null, cancellationToken);

        if (user!.Id != question.UserId)
        {
            var message = string.Format(MessageConstants.ForbiddenActionMessage, "update", nameof(Question));

            return CustomResponse<QuestionDto>.CreateUnsuccessfulResponse(HttpStatusCode.Forbidden, message);
        }

        question.Body = requestDto.NewBody;
        question.Title = requestDto.NewTitle;

        await _unitOfWork.CommitAsync(cancellationToken);

        var updatedQuestionDto = new QuestionDto
        {
            Guid = questionGuid,
            UserGuid = user.Guid,
            Title = requestDto.NewTitle,
            Body = requestDto.NewBody
        };

        var successMessage = string.Format(MessageConstants.SuccessfulUpdateMessage, nameof(Question));

        return CustomResponse<QuestionDto>.CreateSuccessfulResponse(updatedQuestionDto, successMessage);
    }

    public async Task<CustomResponse> DeleteQuestionByIdAsync(int questionId, CancellationToken cancellationToken = default)
    {
        var question = await _questionRepository.GetByIdAsync(questionId, null, cancellationToken);

        if (question is null)
        {
            var message = string.Format(MessageConstants.EntityNotFoundByIdMessage, nameof(Question), questionId);

            return CustomResponse.CreateUnsuccessfulResponse(HttpStatusCode.NotFound, message);
        }

        _questionRepository.Delete(question);

        var successMessage = string.Format(MessageConstants.SuccessfulDeleteMessage, nameof(Question));

        return CustomResponse.CreateSuccessfulResponse(successMessage);
    }

    public async Task<CustomResponse> DeleteQuestionByGuidAsync(Guid questionGuid, CancellationToken cancellationToken = default)
    {
        var question = await _questionRepository.GetByGuidAsync(questionGuid, null, cancellationToken);

        if (question is null)
        {
            var message = string.Format(MessageConstants.EntityNotFoundByGuidMessage, nameof(Question), questionGuid);

            return CustomResponse.CreateUnsuccessfulResponse(HttpStatusCode.NotFound, message);
        }

        var userGuid = _httpContextAccessor.HttpContext?.User.FindFirstValue(ApplicationConstants.ExternalIdClaim)!;

        var user = await _unitOfWork.UserRepository.GetByGuidAsync(Guid.Parse(userGuid), null, cancellationToken);

        if (question.UserId != user!.Id)
        {
            var message = string.Format(MessageConstants.ForbiddenActionMessage, "delete", nameof(Question));

            return CustomResponse.CreateUnsuccessfulResponse(HttpStatusCode.NotFound, message);
        }

        _questionRepository.Delete(question);

        var successMessage = string.Format(MessageConstants.SuccessfulDeleteMessage, nameof(Question));

        return CustomResponse.CreateSuccessfulResponse(successMessage);
    }
}