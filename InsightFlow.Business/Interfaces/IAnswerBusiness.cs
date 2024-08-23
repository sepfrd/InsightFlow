using InsightFlow.Common.Dtos;
using InsightFlow.Common.Dtos.CustomResponses;
using InsightFlow.Common.Dtos.Requests;
using InsightFlow.Model.Entities;
using Sieve.Models;

namespace InsightFlow.Business.Interfaces;

public interface IAnswerBusiness
{
    Task<CustomResponse<AnswerDto>> CreateAnswerAsync(Guid questionGuid, CreateAnswerRequestDto requestDto, CancellationToken cancellationToken = default);

    Task<CustomResponse<Answer>> GetAnswerByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<CustomResponse<AnswerDto>> GetAnswerByGuidAsync(Guid guid, CancellationToken cancellationToken = default);

    Task<PagedCustomResponse<List<AnswerDto>>> GetAnswerDtosByQuestionGuidAsync(
        Guid questionGuid,
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default);

    Task<PagedCustomResponse<List<Answer>>> GetAllAnswersAsync(SieveModel sieveModel, CancellationToken cancellationToken = default);

    Task<PagedCustomResponse<List<AnswerDto>>> GetCurrentUserAnswerDtosAsync(
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default);

    Task<CustomResponse<AnswerDto>> UpdateAnswerAsync(Guid answerGuid, UpdateAnswerRequestDto requestDto, CancellationToken cancellationToken = default);

    Task<CustomResponse> DeleteAnswerByIdAsync(int answerId, CancellationToken cancellationToken = default);

    Task<CustomResponse> DeleteAnswerByGuidAsync(Guid answerGuid, CancellationToken cancellationToken = default);
}