using InsightFlow.Common.Dtos;
using InsightFlow.Common.Dtos.CustomResponses;
using InsightFlow.Common.Dtos.Requests;
using InsightFlow.Model.Entities;
using Sieve.Models;

namespace InsightFlow.Business.Contracts;

public interface IQuestionBusiness
{
    Task<CustomResponse<QuestionDto>> CreateQuestionAsync(CreateQuestionRequestDto requestDto, CancellationToken cancellationToken = default);

    Task<CustomResponse<Question>> GetQuestionByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<CustomResponse<QuestionDto>> GetQuestionByGuidAsync(Guid guid, CancellationToken cancellationToken = default);

    Task<PagedCustomResponse<List<Question>>> GetAllQuestionsAsync(SieveModel sieveModel, CancellationToken cancellationToken = default);

    Task<PagedCustomResponse<List<QuestionDto>>> GetCurrentUserQuestionDtosAsync(
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default);
}