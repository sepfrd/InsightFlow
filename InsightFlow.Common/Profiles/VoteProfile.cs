using InsightFlow.Common.Dtos;
using InsightFlow.Model.Entities;
using Profile = AutoMapper.Profile;

namespace InsightFlow.Common.Profiles;

public class VoteProfile : Profile
{
    public VoteProfile()
    {
        CreateMap<VoteDto, AnswerVote>()
            .ReverseMap();

        CreateMap<VoteDto, QuestionVote>()
            .ReverseMap();
    }
}