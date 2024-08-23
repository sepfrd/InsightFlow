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

public class AnswerBusiness : IAnswerBusiness
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IBaseRepository<Answer> _answerRepository;

    public AnswerBusiness(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _answerRepository = unitOfWork.AnswerRepository;
    }

    public async Task<CustomResponse<AnswerDto>> CreateAnswerAsync(Guid questionGuid, CreateAnswerRequestDto requestDto, CancellationToken cancellationToken = default)
    {
        var answer = _mapper.Map<Answer>(requestDto);

        var userGuid = _httpContextAccessor.HttpContext?.User.FindFirstValue(ApplicationConstants.ExternalIdClaim)!;

        var user = await _unitOfWork.UserRepository.GetByGuidAsync(Guid.Parse(userGuid), null, cancellationToken);

        var question = await _unitOfWork.QuestionRepository.GetByGuidAsync(questionGuid, null, cancellationToken);

        if (question is null)
        {
            var message = string.Format(MessageConstants.InvalidParametersMessage, nameof(questionGuid));

            return CustomResponse<AnswerDto>.CreateUnsuccessfulResponse(HttpStatusCode.BadRequest, message);
        }

        answer.UserId = user!.Id;

        answer.QuestionId = question.Id;

        var createdAnswer = await _answerRepository.CreateAsync(answer, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        var answerDto = _mapper.Map<AnswerDto>(createdAnswer);

        return CustomResponse<AnswerDto>.CreateSuccessfulResponse(answerDto, null, HttpStatusCode.Created);
    }

    public async Task<CustomResponse<Answer>> GetAnswerByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var answer = await _answerRepository.GetByIdAsync(id, answers =>
                answers
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
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var question = await _unitOfWork.QuestionRepository.GetByGuidAsync(
            questionGuid,
            questions => questions.Include(question => question.Answers),
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
        pageSize = pageSize >= 10 && pageSize <= 100 ? pageSize : 10;

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
        sieveModel.PageSize ??= 10;

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
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var userGuid = _httpContextAccessor.HttpContext?.User.FindFirstValue(ApplicationConstants.ExternalIdClaim)!;

        var user = await _unitOfWork.UserRepository.GetByGuidAsync(
            Guid.Parse(userGuid),
            users => users
                .Include(user => user.Answers)
                .ThenInclude(answer => answer.Question),
            cancellationToken);

        if (user!.Answers.Count == 0)
        {
            return PagedCustomResponse<List<AnswerDto>>.CreateSuccessfulResponse([]);
        }

        pageNumber = pageNumber > 0 ? pageNumber : 1;
        pageSize = pageSize >= 10 && pageSize <= 100 ? pageSize : 10;

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
            answers => answers
                .Include(answer => answer.Question),
            cancellationToken);

        if (answer is null)
        {
            var message = string.Format(MessageConstants.EntityNotFoundByGuidMessage, nameof(Answer), answerGuid);

            return CustomResponse<AnswerDto>.CreateUnsuccessfulResponse(HttpStatusCode.NotFound, message);
        }

        var userGuid = _httpContextAccessor.HttpContext?.User.FindFirstValue(ApplicationConstants.ExternalIdClaim)!;

        var user = await _unitOfWork.UserRepository.GetByGuidAsync(Guid.Parse(userGuid), null, cancellationToken);

        if (user!.Id != answer.UserId)
        {
            var message = string.Format(MessageConstants.ForbiddenActionMessage, "update", nameof(Answer));

            return CustomResponse<AnswerDto>.CreateUnsuccessfulResponse(HttpStatusCode.Forbidden, message);
        }

        answer.Body = requestDto.NewBody;

        await _unitOfWork.CommitAsync(cancellationToken);

        var updatedAnswerDto = new AnswerDto
        {
            Guid = answerGuid,
            UserGuid = user.Guid,
            QuestionGuid = answer.Question!.Guid,
            Body = requestDto.NewBody
        };

        var successMessage = string.Format(MessageConstants.SuccessfulUpdateMessage, nameof(Answer));

        return CustomResponse<AnswerDto>.CreateSuccessfulResponse(updatedAnswerDto, successMessage);
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

        var successMessage = string.Format(MessageConstants.SuccessfulDeleteMessage, nameof(Answer));

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

        var userGuid = _httpContextAccessor.HttpContext?.User.FindFirstValue(ApplicationConstants.ExternalIdClaim)!;

        var user = await _unitOfWork.UserRepository.GetByGuidAsync(Guid.Parse(userGuid), null, cancellationToken);

        if (answer.UserId != user!.Id)
        {
            var message = string.Format(MessageConstants.ForbiddenActionMessage, "delete", nameof(Answer));

            return CustomResponse.CreateUnsuccessfulResponse(HttpStatusCode.NotFound, message);
        }

        _answerRepository.Delete(answer);

        var successMessage = string.Format(MessageConstants.SuccessfulDeleteMessage, nameof(Answer));

        return CustomResponse.CreateSuccessfulResponse(successMessage);
    }
}