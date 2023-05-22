using AutoMapper;
using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.DataAccess.Repositories;
using RedditMockup.Model.Entities;
using Sieve.Models;

namespace RedditMockup.Service.Grpc;

public class GrpcService : RedditMockupGrpc.RedditMockupGrpcBase
{
    private readonly QuestionRepository _questionRepository;

    private readonly IMapper _mapper;

    public GrpcService(IServiceScopeFactory serviceScopeFactory, IMapper mapper)
    {
        var unitOfWork = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<IUnitOfWork>();

        if (unitOfWork.QuestionRepository is null)
        {
            return;
        }

        _questionRepository = unitOfWork.QuestionRepository;

        _mapper = mapper;
    }

    public async override Task<GrpcResponse> GetAllQuestions(GetAllRequest request, ServerCallContext context)
    {
        var response = new GrpcResponse();

        var questions = await _questionRepository.LoadAllAsync(new SieveModel());

        var questionDtos = _mapper.Map<IEnumerable<GrpcQuestionModel>>(questions);

        response.Question.AddRange(questionDtos);

        return response;
    }
}
