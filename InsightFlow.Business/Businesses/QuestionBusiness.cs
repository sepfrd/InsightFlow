using System.Net;
using AutoMapper;
using InsightFlow.Business.Interfaces;
using InsightFlow.Common.Constants;
using InsightFlow.Common.Dtos;
using InsightFlow.Common.Dtos.CustomResponses;
using InsightFlow.Common.Dtos.Requests;
using InsightFlow.DataAccess.Interfaces;
using InsightFlow.Model.Entities;
using InsightFlow.Model.Enums;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;

namespace InsightFlow.Business.Businesses;

public class QuestionBusiness : IQuestionBusiness
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAuthBusiness _authBusiness;
    private readonly IBaseRepository<Question> _questionRepository;

    public QuestionBusiness(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IAuthBusiness authBusiness)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _authBusiness = authBusiness;
        _questionRepository = unitOfWork.QuestionRepository;
    }

    public async Task<CustomResponse<QuestionDto>> CreateQuestionAsync(CreateQuestionRequestDto requestDto, CancellationToken cancellationToken = default)
    {
        var userExternalId = _authBusiness.GetSignedInUserExternalId();

        var user = await _unitOfWork.UserRepository.GetByGuidAsync(Guid.Parse(userExternalId), null, cancellationToken);

        var question = _mapper.Map<Question>(requestDto);

        question.UserId = user!.Id;

        var createdQuestion = await _questionRepository.CreateAsync(question, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        var questionDto = _mapper.Map<QuestionDto>(createdQuestion);

        questionDto.User ??= _mapper.Map<UserDto>(user);

        return CustomResponse<QuestionDto>.CreateSuccessfulResponse(questionDto, null, HttpStatusCode.Created);
    }

    public async Task<CustomResponse<Question>> GetQuestionByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var question = await _questionRepository.GetByIdAsync(
            id,
            questions => questions.Include(question => question.User),
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
        sieveModel.PageSize ??= ApplicationConstants.MinimumPageSize;

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

    public async Task<PagedCustomResponse<List<QuestionDto>>> GetAllQuestionDtosAsync(SieveModel sieveModel, CancellationToken cancellationToken = default)
    {
        var result = await GetAllQuestionsAsync(sieveModel, cancellationToken);

        var questionDtos = result.IsSuccess ? _mapper.Map<List<QuestionDto>>(result.Data) : null;

        return new PagedCustomResponse<List<QuestionDto>>
        {
            Data = questionDtos,
            IsSuccess = result.IsSuccess,
            Message = result.Message,
            HttpStatusCode = result.HttpStatusCode,
            TotalCount = result.TotalCount,
            CurrentCount = result.CurrentCount,
            PageNumber = result.PageNumber,
            PageSize = result.PageSize
        };
    }

    public async Task<PagedCustomResponse<List<QuestionDto>>> GetCurrentUserQuestionDtosAsync(
        int pageNumber = 1,
        int pageSize = ApplicationConstants.MinimumPageSize,
        CancellationToken cancellationToken = default)
    {
        var userExternalId = _authBusiness.GetSignedInUserExternalId();

        var user = await _unitOfWork.UserRepository.GetByGuidAsync(
            Guid.Parse(userExternalId),
            users => users.Include(user => user.Questions),
            cancellationToken);

        if (user!.Questions.Count == 0)
        {
            return PagedCustomResponse<List<QuestionDto>>.CreateSuccessfulResponse([]);
        }

        pageNumber = pageNumber > 0 ? pageNumber : 1;

        pageSize = pageSize >= ApplicationConstants.MinimumPageSize && pageSize <= ApplicationConstants.MaximumPageSize
            ? pageSize
            : ApplicationConstants.MinimumPageSize;

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

        var userExternalId = _authBusiness.GetSignedInUserExternalId();

        var user = await _unitOfWork.UserRepository.GetByGuidAsync(Guid.Parse(userExternalId), null, cancellationToken);

        if (user!.Id != question.UserId)
        {
            var message = string.Format(MessageConstants.ForbiddenActionMessage, "update", nameof(Question).ToLowerInvariant());

            return CustomResponse<QuestionDto>.CreateUnsuccessfulResponse(HttpStatusCode.Forbidden, message);
        }

        question.Body = requestDto.NewBody;
        question.Title = requestDto.NewTitle;
        question.LastUpdated = DateTime.Now;

        await _unitOfWork.CommitAsync(cancellationToken);

        var updatedQuestionDto = _mapper.Map<QuestionDto>(question);

        updatedQuestionDto.User ??= _mapper.Map<UserDto>(user);

        var successMessage = string.Format(MessageConstants.SuccessfulUpdateMessage, nameof(Question).ToLowerInvariant());

        return CustomResponse<QuestionDto>.CreateSuccessfulResponse(updatedQuestionDto, successMessage);
    }

    public async Task<CustomResponse<Question>> UpdateQuestionStateAsync(
        int questionId,
        BaseEntityState newState,
        CancellationToken cancellationToken = default)
    {
        var question = await _questionRepository.GetByIdAsync(questionId, null, cancellationToken);

        if (question is null)
        {
            return CustomResponse<Question>.CreateUnsuccessfulResponse(HttpStatusCode.NotFound);
        }

        if (question.State == newState)
        {
            var message = string.Format(MessageConstants.IdenticalNewValue, "State", newState);

            return CustomResponse<Question>.CreateUnsuccessfulResponse(HttpStatusCode.BadRequest, message);
        }

        question.State = newState;
        question.LastUpdated = DateTime.Now;

        await _unitOfWork.CommitAsync(cancellationToken);

        var successMessage = string.Format(MessageConstants.SuccessfulUpdateMessage, nameof(Question).ToLowerInvariant());

        return CustomResponse<Question>.CreateSuccessfulResponse(question, successMessage);
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

        await _unitOfWork.CommitAsync(cancellationToken);

        var successMessage = string.Format(MessageConstants.SuccessfulDeleteMessage, nameof(Question).ToLowerInvariant());

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

        var userExternalId = _authBusiness.GetSignedInUserExternalId();

        var user = await _unitOfWork.UserRepository.GetByGuidAsync(Guid.Parse(userExternalId), null, cancellationToken);

        if (question.UserId != user!.Id)
        {
            var message = string.Format(MessageConstants.ForbiddenActionMessage, "delete", nameof(Question).ToLowerInvariant());

            return CustomResponse.CreateUnsuccessfulResponse(HttpStatusCode.Forbidden, message);
        }

        _questionRepository.Delete(question);

        await _unitOfWork.CommitAsync(cancellationToken);

        var successMessage = string.Format(MessageConstants.SuccessfulDeleteMessage, nameof(Question).ToLowerInvariant());

        return CustomResponse.CreateSuccessfulResponse(successMessage);
    }
}