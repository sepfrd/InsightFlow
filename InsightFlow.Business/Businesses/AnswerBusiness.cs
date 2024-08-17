using AutoMapper;
using InsightFlow.Business.Base;
using InsightFlow.Business.Businesses.AdminBusinesses;
using InsightFlow.Business.Contracts;
using InsightFlow.Common.Dtos;
using InsightFlow.Common.Dtos.CustomResponses;
using InsightFlow.Model.Entities;

namespace InsightFlow.Business.Businesses;

public class AnswerBusiness : BaseBusiness<Answer, AnswerDto>
{
    private readonly IMapper _mapper;

    private readonly AdminAnswerBusiness _adminAnswerBusiness;

    public AnswerBusiness(IAdminBaseBusiness<Answer, AnswerDto> answerBusiness, IMapper mapper) : base(answerBusiness, mapper)
    {
        _mapper = mapper;
        _adminAnswerBusiness = (AdminAnswerBusiness)answerBusiness;
    }

    public async Task<CustomResponse<List<VoteDto>>> GetVotesByAnswerGuidAsync(Guid answerGuid, CancellationToken cancellationToken = default)
    {
        var votesResponse = await _adminAnswerBusiness.GetVotesByAnswerGuidAsync(answerGuid, cancellationToken);

        if (!votesResponse.IsSuccess)
        {
            return CustomResponse<List<VoteDto>>.CreateUnsuccessfulResponse(votesResponse.HttpStatusCode, votesResponse.Message);
        }

        var voteDtos = _mapper.Map<List<VoteDto>>(votesResponse.Data);

        return CustomResponse<List<VoteDto>>.CreateSuccessfulResponse(voteDtos);
    }

    public async Task<CustomResponse> SubmitVoteAsync(Guid answerGuid, bool kind, CancellationToken cancellationToken = default) =>
        await _adminAnswerBusiness.SubmitVoteAsync(answerGuid, kind, cancellationToken);
}