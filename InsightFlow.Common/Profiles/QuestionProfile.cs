using InsightFlow.Common.Dtos;
using InsightFlow.Common.Dtos.Requests;
using InsightFlow.Model.Entities;
using Profile = AutoMapper.Profile;

namespace InsightFlow.Common.Profiles;

public class QuestionProfile : Profile
{
    public QuestionProfile()
    {
        CreateMap<Question, QuestionDto>()
            .ForMember(
                questionDto => questionDto.User,
                option => option.MapFrom(question => question.User!));

        CreateMap<QuestionDto, Question>();

        CreateMap<CreateQuestionRequestDto, Question>();
    }
}