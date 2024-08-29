using InsightFlow.Common.Dtos;
using InsightFlow.Model.Entities;
using Profile = AutoMapper.Profile;

namespace InsightFlow.Common.Profiles;

public class QuestionProfile : Profile
{
    public QuestionProfile()
    {
        CreateMap<Question, QuestionDto>()
            .ForMember(questionDto => questionDto.AskingUser,
                option => option.MapFrom(question => question.User!));

        CreateMap<QuestionDto, Question>();
    }
}