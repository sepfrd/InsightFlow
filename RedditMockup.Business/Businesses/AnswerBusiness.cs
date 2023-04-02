using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using RedditMockup.Business.Base;
using RedditMockup.Business.Contracts;
using RedditMockup.Common.Dtos;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.DataAccess.Repositories;
using RedditMockup.Model.Entities;
using Sieve.Models;

namespace RedditMockup.Business.Businesses;

public class AnswerBusiness : BaseBusiness<Answer>
{
    private readonly AnswerRepository _answerRepository;
    private readonly AnswerVoteRepository _answerVoteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    //private readonly QuestionBusiness _questionBusiness;
    //private readonly UserRepository _userRepository;
    //private readonly UserBusiness _userBusiness;

    public AnswerBusiness(IUnitOfWork unitOfWork, IMapper mapper, IBaseBusiness<User> userBusiness, IBaseBusiness<Question> questionBusiness) : base(unitOfWork, unitOfWork.AnswerRepository!)
    {
        _answerRepository = unitOfWork.AnswerRepository!;
        _answerVoteRepository = unitOfWork.AnswerVoteRepository!;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        //_questionBusiness = (QuestionBusiness)questionBusiness;
        //_userBusiness = (UserBusiness)userBusiness;
        //_userRepository = unitOfWork.UserRepository!;
    }

    public async Task<CustomResponse?> LoadAnswersByQuestionIdAsync(int questionId, CancellationToken cancellationToken = new())
    {
        //var question = await _questionRepository.LoadByIdAsync(questionId, cancellationToken);

        SieveModel sieveModel = new()
        {
            Filters = $"QuestionId=={questionId}"
        };

        var answers = await _answerRepository.LoadAllAsync(sieveModel, null, cancellationToken);

        if (answers.IsNullOrEmpty())
        {
            return new CustomResponse
            {
                IsSuccess = false,
                Message = $"No question found with ID of {questionId}"
            };
        }

        var response = _mapper.Map<List<AnswerDto>>(answers);

        return new CustomResponse
        {
            Data = response,
            IsSuccess = true
        };
    }

    public async Task<CustomResponse?> SubmitVoteAsync(int id, bool kind, CancellationToken cancellationToken = new())
    {
        var answer = await _answerRepository.LoadByIdAsync(id, cancellationToken);

        if (answer is null)
        {
            return new CustomResponse
            {
                IsSuccess = false,
                Message = $"No answer found with ID of {id}"
            };
        }

        if (kind)
        {
            answer.Question!.User!.Score += 1;
        }

        var vote = new AnswerVote
        {
            Kind = kind,
            AnswerId = answer.Id
        };

        await _answerVoteRepository.CreateAsync(vote, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return new CustomResponse
        {
            IsSuccess = true,
            Message = $"{(kind ? "Up" : "Down")}vote submitted"
        };
    }

    public async Task<CustomResponse?> LoadVotesAsync(int id, CancellationToken cancellationToken = new())
    {
        var answer = await _answerRepository.LoadByIdAsync(id, cancellationToken);

        if (answer is null)
        {
            return new CustomResponse
            {
                IsSuccess = false,
                Message = $"No answer found with ID of {id}"
            };
        }

        var votes = answer.Votes!.ToList();

        var response = _mapper.Map<List<VoteDto>>(votes);

        return new CustomResponse
        {
            Data = response,
            IsSuccess = true,
        };
    }

    /*
    public override async Task<CustomResponse?> CreateAsync(AnswerDto dto, HttpContext httpContext, CancellationToken
    cancellationToken = new())
    {
        var question = await _questionBusiness.LoadModelByIdAsync(dto.QuestionId, cancellationToken);

        var stringUserId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);


        if (stringUserId is null)
        {
            return new CustomResponse
            {
                IsSuccess = false,
                Message = $"No logged in user found."
            };
        }

        if (question is null)
        {
            return new CustomResponse
            {
                IsSuccess = false,
                Message = $"No question found with ID of {dto.QuestionId}"
            };
        }

        var userId = int.Parse(stringUserId);

        var answer = _mapper.Map<Answer>(dto);

        answer.UserId = userId;

        answer.QuestionId = question.Id;

        var user = await _userBusiness.LoadModelByIdAsync(userId, cancellationToken);

        user!.Score += 1;

        _userRepository.Update(user);

        return await CreateAsync(answer, cancellationToken);
    }

    private async Task<Answer?> LoadModelByIdAsync(int questionId, CancellationToken cancellationToken = new())
    {
        SieveModel sieveModel = new()
        {
            Filters = $"Id=={questionId}"
        };


        var answers =
        await _answerRepository.LoadAllAsync(sieveModel, include => include
                            .Include(x => x.User)
                            .Include(x => x.Votes)
                            .Include(x => x.Question)
                            .ThenInclude(x => x!.User),
                            cancellationToken);

        if (answers.Count == 0)
        {
            return null;
        }

        return answers.Single();
    }

    public override async Task<CustomResponse?> LoadByIdAsync(int questionId, CancellationToken cancellationToken = new())
    {
        var answer = await LoadModelByIdAsync(questionId, cancellationToken);

        if (answer is null)
        {
            return new CustomResponse
            {
                IsSuccess = false,
                Message = $"No answer found with ID of {questionId}"
            };
        }

        var response = _mapper.Map<AnswerDto>(answer);

        return new CustomResponse
        {
            Data = response,
            IsSuccess = true
        };
    }

    public override async Task<CustomResponse?> UpdateAsync(int questionId, AnswerDto dto, CancellationToken cancellationToken = new())
    {
        var answer = await LoadModelByIdAsync(questionId, cancellationToken);

        if (answer is null)
        {
            return new CustomResponse
            {
                IsSuccess = false,
                Message = $"No answer found with ID of {questionId}"
            };
        }
        if (answer.QuestionId != dto.QuestionId)
        {
            return new CustomResponse
            {
                IsSuccess = false,
                Message = $"No answer found with QuestionId of {dto.QuestionId}"
            };
        }

        _mapper.Map(dto, answer);

        return await UpdateAsync(answer, cancellationToken);
    }

    public override async Task<CustomResponse?> DeleteAsync(int questionId, CancellationToken cancellationToken = new())
    {
        var answer = await LoadModelByIdAsync(questionId, cancellationToken);

        if (answer is null)
        {
            return new CustomResponse
            {
                IsSuccess = false,
                Message = $"No answer found with questionId of {questionId}"
            };
        }

        return await DeleteAsync(answer, cancellationToken);
    }
*/
}