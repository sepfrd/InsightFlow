using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RedditMockup.Business.Base;
using RedditMockup.Common.Dtos;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.DataAccess.Repositories;
using RedditMockup.Model.Entities;
using Sieve.Models;
using System.Net;

namespace RedditMockup.Business.DomainEntityBusinesses;

public class QuestionBusiness : BaseBusiness<Question, QuestionDto>
{
    #region [Fields]

    private readonly QuestionRepository _questionRepository;

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    #endregion

    #region [Constructor]

    public QuestionBusiness(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, unitOfWork.QuestionRepository!, mapper)
    {
        _questionRepository = unitOfWork.QuestionRepository!;

        _unitOfWork = unitOfWork;

        _mapper = mapper;
    }

    #endregion

    #region [Methods]

    public override async Task<Question?> CreateAsync(QuestionDto questionDto, CancellationToken cancellationToken = default)
    {
        var question = _mapper.Map<Question>(questionDto);

        var userId = await _unitOfWork.UserRepository!.GetIdByGuidAsync(questionDto.UserGuid, cancellationToken);

        if (userId is null)
        {
            return null;
        }

        question.UserId = (int)userId;

        return await CreateAsync(question, cancellationToken);
    }

    public override async Task<Question?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        await _questionRepository.GetByIdAsync(id,
            questions => questions.Include(question => question.User),
            cancellationToken);

    public override async Task<Question?> GetByGuidAsync(Guid guid, CancellationToken cancellationToken = default) =>
        await _questionRepository.GetByGuidAsync(guid,
            questions => questions.Include(question => question.User),
            cancellationToken);

    public override async Task<List<Question>?> GetAllAsync(SieveModel sieveModel, CancellationToken cancellationToken = default) =>
        await _questionRepository.GetAllAsync(sieveModel,
            questions => questions.Include(question => question.User),
            cancellationToken);

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

    #endregion
}