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

public class AnswerBusiness : IAnswerBusiness
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAuthBusiness _authBusiness;
    private readonly IBaseRepository<Answer> _answerRepository;

    public AnswerBusiness(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IAuthBusiness authBusiness)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _authBusiness = authBusiness;
        _answerRepository = unitOfWork.AnswerRepository;
    }

    public async Task<CustomResponse<AnswerDto>> CreateAnswerAsync(
        Guid questionGuid,
        CreateAnswerRequestDto requestDto,
        CancellationToken cancellationToken = default)
    {
        var userExternalId = _authBusiness.GetSignedInUserExternalId();

        var user = await _unitOfWork.UserRepository.GetByGuidAsync(Guid.Parse(userExternalId), null, cancellationToken);

        var question = await _unitOfWork.QuestionRepository.GetByGuidAsync(questionGuid, null, cancellationToken);

        if (question is null)
        {
            var message = string.Format(MessageConstants.InvalidParametersMessage, nameof(questionGuid));

            return CustomResponse<AnswerDto>.CreateUnsuccessfulResponse(HttpStatusCode.BadRequest, message);
        }

        var answer = _mapper.Map<Answer>(requestDto);

        answer.UserId = user!.Id;

        answer.QuestionId = question.Id;

        var createdAnswer = await _answerRepository.CreateAsync(answer, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        var answerDto = _mapper.Map<AnswerDto>(createdAnswer);

        return CustomResponse<AnswerDto>.CreateSuccessfulResponse(answerDto, null, HttpStatusCode.Created);
    }

    public async Task<CustomResponse<Answer>> GetAnswerByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var answer = await _answerRepository.GetByIdAsync(
            id,
            answers => answers
                .Include(answer => answer.User)
                .Include(answer => answer.Question),
            cancellationToken);

        if (answer is null)
        {
            return CustomResponse<Answer>.CreateUnsuccessfulResponse(HttpStatusCode.NotFound);
        }

        return CustomResponse<Answer>.CreateSuccessfulResponse(answer);
    }

    public async Task<CustomResponse<AnswerDto>> GetAnswerByGuidAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        var answer = await _answerRepository.GetByGuidAsync(guid, answers =>
                answers
                    .Include(answer => answer.User)
                    .Include(answer => answer.Question),
            cancellationToken);

        if (answer is null)
        {
            var message = string.Format(MessageConstants.EntityNotFoundByGuidMessage, nameof(Answer), guid);

            return CustomResponse<AnswerDto>.CreateUnsuccessfulResponse(HttpStatusCode.NotFound, message);
        }

        var answerDto = _mapper.Map<AnswerDto>(answer);

        return CustomResponse<AnswerDto>.CreateSuccessfulResponse(answerDto);
    }

    public async Task<PagedCustomResponse<List<AnswerDto>>> GetAnswerDtosByQuestionGuidAsync(
        Guid questionGuid,
        int pageNumber = 1,
        int pageSize = ApplicationConstants.MinimumPageSize,
        CancellationToken cancellationToken = default)
    {
        var question = await _unitOfWork.QuestionRepository.GetByGuidAsync(
            questionGuid,
            questions => questions
                .Include(question => question.Answers)
                .ThenInclude(answer => answer.User),
            cancellationToken);

        if (question is null)
        {
            var message = string.Format(MessageConstants.EntityNotFoundByGuidMessage, nameof(Question), questionGuid);

            return PagedCustomResponse<List<AnswerDto>>.CreateUnsuccessfulResponse(HttpStatusCode.NotFound, message);
        }

        if (question.Answers.Count == 0)
        {
            return PagedCustomResponse<List<AnswerDto>>.CreateSuccessfulResponse([]);
        }

        pageNumber = pageNumber > 0 ? pageNumber : 1;

        pageSize = pageSize >= ApplicationConstants.MinimumPageSize && pageSize <= ApplicationConstants.MaximumPageSize
            ? pageSize
            : ApplicationConstants.MinimumPageSize;

        var skipCount = (pageNumber - 1) * pageSize;

        var answers = question
            .Answers
            .OrderByDescending(answer => answer.Id)
            .Skip(skipCount)
            .Take(pageSize)
            .ToList();

        var answerDtos = _mapper.Map<List<AnswerDto>>(answers);

        return PagedCustomResponse<List<AnswerDto>>.CreateSuccessfulResponse(
            answerDtos,
            null,
            HttpStatusCode.OK,
            question.Answers.Count,
            answerDtos.Count,
            pageNumber,
            pageSize);
    }

    public async Task<PagedCustomResponse<List<Answer>>> GetAllAnswersAsync(SieveModel sieveModel, CancellationToken cancellationToken = default)
    {
        sieveModel.Page ??= 1;
        sieveModel.PageSize ??= ApplicationConstants.MinimumPageSize;

        var result = await _answerRepository.GetAllAsync(
            sieveModel,
            answers =>
                answers
                    .Include(answer => answer.User)
                    .Include(answer => answer.Question),
            cancellationToken);

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

    public async Task<PagedCustomResponse<List<AnswerDto>>> GetCurrentUserAnswerDtosAsync(
        int pageNumber = 1,
        int pageSize = ApplicationConstants.MinimumPageSize,
        CancellationToken cancellationToken = default)
    {
        var userExternalId = _authBusiness.GetSignedInUserExternalId();

        var user = await _unitOfWork.UserRepository.GetByGuidAsync(
            Guid.Parse(userExternalId),
            users => users
                .Include(user => user.Answers)
                .ThenInclude(answer => answer.Question),
            cancellationToken);

        if (user!.Answers.Count == 0)
        {
            return PagedCustomResponse<List<AnswerDto>>.CreateSuccessfulResponse([]);
        }

        pageNumber = pageNumber > 0 ? pageNumber : 1;

        pageSize = pageSize >= ApplicationConstants.MinimumPageSize && pageSize <= ApplicationConstants.MaximumPageSize
            ? pageSize
            : ApplicationConstants.MinimumPageSize;

        var skipCount = (pageNumber - 1) * pageSize;

        var answers = user
            .Answers
            .OrderByDescending(Answer => Answer.Id)
            .Skip(skipCount)
            .Take(pageSize)
            .ToList();

        var answerDtos = _mapper.Map<List<AnswerDto>>(answers);

        return PagedCustomResponse<List<AnswerDto>>.CreateSuccessfulResponse(
            answerDtos,
            null,
            HttpStatusCode.OK,
            user.Answers.Count,
            answerDtos.Count,
            pageNumber,
            pageSize);
    }

    public async Task<CustomResponse<AnswerDto>> UpdateAnswerAsync(
        Guid answerGuid,
        UpdateAnswerRequestDto requestDto,
        CancellationToken cancellationToken = default)
    {
        var answer = await _answerRepository.GetByGuidAsync(
            answerGuid,
            answers => answers.Include(answer => answer.Question),
            cancellationToken);

        if (answer is null)
        {
            var message = string.Format(MessageConstants.EntityNotFoundByGuidMessage, nameof(Answer), answerGuid);

            return CustomResponse<AnswerDto>.CreateUnsuccessfulResponse(HttpStatusCode.NotFound, message);
        }

        var userExternalId = _authBusiness.GetSignedInUserExternalId();

        var user = await _unitOfWork.UserRepository.GetByGuidAsync(Guid.Parse(userExternalId), null, cancellationToken);

        if (user!.Id != answer.UserId)
        {
            var message = string.Format(MessageConstants.ForbiddenActionMessage, "update", nameof(Answer).ToLowerInvariant());

            return CustomResponse<AnswerDto>.CreateUnsuccessfulResponse(HttpStatusCode.Forbidden, message);
        }

        answer.Body = requestDto.NewBody;
        answer.LastUpdated = DateTime.Now;

        await _unitOfWork.CommitAsync(cancellationToken);

        var updatedAnswerDto = _mapper.Map<AnswerDto>(answer);

        updatedAnswerDto.User ??= _mapper.Map<UserDto>(user);

        var successMessage = string.Format(MessageConstants.SuccessfulUpdateMessage, nameof(Answer).ToLowerInvariant());

        return CustomResponse<AnswerDto>.CreateSuccessfulResponse(updatedAnswerDto, successMessage);
    }

    public async Task<CustomResponse<Answer>> UpdateAnswerStateAsync(int answerId, BaseEntityState newState, CancellationToken cancellationToken = default)
    {
        var answer = await _answerRepository.GetByIdAsync(answerId, null, cancellationToken);

        if (answer is null)
        {
            return CustomResponse<Answer>.CreateUnsuccessfulResponse(HttpStatusCode.NotFound);
        }

        if (answer.State == newState)
        {
            var message = string.Format(MessageConstants.IdenticalNewValue, "State", newState);

            return CustomResponse<Answer>.CreateUnsuccessfulResponse(HttpStatusCode.BadRequest, message);
        }

        answer.State = newState;
        answer.LastUpdated = DateTime.Now;

        await _unitOfWork.CommitAsync(cancellationToken);

        var successMessage = string.Format(MessageConstants.SuccessfulUpdateMessage, nameof(Answer).ToLowerInvariant());

        return CustomResponse<Answer>.CreateSuccessfulResponse(answer, successMessage);
    }

    public async Task<CustomResponse> DeleteAnswerByIdAsync(int answerId, CancellationToken cancellationToken = default)
    {
        var answer = await _answerRepository.GetByIdAsync(answerId, null, cancellationToken);

        if (answer is null)
        {
            var message = string.Format(MessageConstants.EntityNotFoundByIdMessage, nameof(Answer), answerId);

            return CustomResponse.CreateUnsuccessfulResponse(HttpStatusCode.NotFound, message);
        }

        _answerRepository.Delete(answer);

        await _unitOfWork.CommitAsync(cancellationToken);

        var successMessage = string.Format(MessageConstants.SuccessfulDeleteMessage, nameof(Answer).ToLowerInvariant());

        return CustomResponse.CreateSuccessfulResponse(successMessage);
    }

    public async Task<CustomResponse> DeleteAnswerByGuidAsync(Guid answerGuid, CancellationToken cancellationToken = default)
    {
        var answer = await _answerRepository.GetByGuidAsync(answerGuid, null, cancellationToken);

        if (answer is null)
        {
            var message = string.Format(MessageConstants.EntityNotFoundByGuidMessage, nameof(Answer), answerGuid);

            return CustomResponse.CreateUnsuccessfulResponse(HttpStatusCode.NotFound, message);
        }

        var userExternalId = _authBusiness.GetSignedInUserExternalId();

        var user = await _unitOfWork.UserRepository.GetByGuidAsync(Guid.Parse(userExternalId), null, cancellationToken);

        if (answer.UserId != user!.Id)
        {
            var message = string.Format(MessageConstants.ForbiddenActionMessage, "delete", nameof(Answer).ToLowerInvariant());

            return CustomResponse.CreateUnsuccessfulResponse(HttpStatusCode.Forbidden, message);
        }

        _answerRepository.Delete(answer);

        await _unitOfWork.CommitAsync(cancellationToken);

        var successMessage = string.Format(MessageConstants.SuccessfulDeleteMessage, nameof(Answer).ToLowerInvariant());

        return CustomResponse.CreateSuccessfulResponse(successMessage);
    }
}