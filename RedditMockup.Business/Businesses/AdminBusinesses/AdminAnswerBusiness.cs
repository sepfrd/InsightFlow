using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RedditMockup.Business.Base;
using RedditMockup.Common.Constants;
using RedditMockup.Common.Dtos;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.DataAccess.Repositories;
using RedditMockup.Model.Entities;
using Sieve.Models;

namespace RedditMockup.Business.Businesses.AdminBusinesses;

public class AdminAnswerBusiness : AdminBaseBusiness<Answer, AnswerDto>
{
    private readonly AnswerRepository _answerRepository;

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public AdminAnswerBusiness(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, unitOfWork.AnswerRepository!, mapper)
    {
        _answerRepository = (AnswerRepository)unitOfWork.AnswerRepository!;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async override Task<CustomResponse<Answer?>> CreateAsync(AnswerDto answerDto, CancellationToken cancellationToken = default)
    {
        var answer = _mapper.Map<Answer>(answerDto);

        var user = await _unitOfWork.UserRepository!.GetByGuidAsync(answerDto.UserGuid, null, cancellationToken);

        if (user is null)
        {
            return CustomResponse<Answer?>.CreateUnsuccessfulResponse(HttpStatusCode.BadRequest);
        }

        var question = await _unitOfWork.QuestionRepository!.GetByGuidAsync(answerDto.QuestionGuid, null, cancellationToken);

        if (question is null)
        {
            var message = string.Format(MessageConstants.QuestionNotFoundMessage, answerDto.QuestionGuid);

            return CustomResponse<Answer?>.CreateUnsuccessfulResponse(HttpStatusCode.NotFound, message);
        }

        answer.UserId = user.Id;

        answer.QuestionId = question.Id;

        return await CreateAsync(answer, cancellationToken);
    }

    public async override Task<CustomResponse<Answer?>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var answer = await _answerRepository.GetByIdAsync(id, answers =>
                answers
                    .Include(answer => answer.User)
                    .Include(answer => answer.Question),
            cancellationToken);

        if (answer is null)
        {
            return CustomResponse<Answer?>.CreateUnsuccessfulResponse(HttpStatusCode.NotFound);
        }

        return CustomResponse<Answer?>.CreateSuccessfulResponse(answer);
    }

    public async override Task<CustomResponse<Answer?>> GetByGuidAsync(Guid guid, CancellationToken cancellationToken = default)
    {
        var answer = await _answerRepository.GetByGuidAsync(guid, answers =>
                answers
                    .Include(answer => answer.User)
                    .Include(answer => answer.Question),
            cancellationToken);

        if (answer is null)
        {
            return CustomResponse<Answer?>.CreateUnsuccessfulResponse(HttpStatusCode.NotFound);
        }

        return CustomResponse<Answer?>.CreateSuccessfulResponse(answer);
    }

    public async override Task<CustomResponse<List<Answer>?>> GetAllAsync(SieveModel sieveModel, CancellationToken cancellationToken = default)
    {
        var answers = await _answerRepository.GetAllAsync(sieveModel,
            answers => answers.Include(answer => answer.User)
                .Include(answer => answer.Question),
            cancellationToken);

        return CustomResponse<List<Answer>?>.CreateSuccessfulResponse(answers);
    }

    public async Task<CustomResponse> SubmitVoteAsync(Guid answerGuid, bool kind, CancellationToken cancellationToken = default)
    {
        var answer = await _answerRepository.GetByGuidAsync(answerGuid,
            answers => answers.Include(answer => answer.User)
                .Include(answer => answer.Votes),
            cancellationToken);

        if (answer is null)
        {
            return CustomResponse.CreateUnsuccessfulResponse(HttpStatusCode.NotFound, $"No answer found with guid of {answerGuid}");
        }

        if (answer.User is null)
        {
            return CustomResponse.CreateUnsuccessfulResponse(HttpStatusCode.InternalServerError);
        }

        if (kind)
        {
            answer.User.Score += 1;
        }

        var vote = new AnswerVote
        {
            Kind = kind,
            AnswerId = answer.Id
        };

        await _answerRepository.SubmitVoteAsync(vote, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return CustomResponse.CreateSuccessfulResponse($"{(kind ? "Up" : "Down")}vote submitted");
    }

    public async Task<CustomResponse<List<AnswerVote>>> GetVotesByAnswerGuidAsync(Guid answerGuid, CancellationToken cancellationToken = default)
    {
        var answer = await _answerRepository.GetByGuidAsync(answerGuid,
            answers => answers.Include(answer => answer.Votes), cancellationToken);

        if (answer is null)
        {
            return CustomResponse<List<AnswerVote>>.CreateUnsuccessfulResponse(HttpStatusCode.NotFound);
        }

        var votes = answer.Votes!.ToList();

        return CustomResponse<List<AnswerVote>>.CreateSuccessfulResponse(votes);
    }
}