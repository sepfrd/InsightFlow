using InsightFlow.Common.Dtos;
using InsightFlow.Common.Dtos.Requests;
using InsightFlow.Model.Entities;
using Profile = AutoMapper.Profile;

namespace InsightFlow.Common.Profiles;

public class AnswerProfile : Profile
{
    public AnswerProfile()
    {
        CreateMap<Answer, AnswerDto>()
            .ForMember(
                answerDto => answerDto.User,
                option => option.MapFrom(answer => answer.User))
            .ForMember(
                answerDto => answerDto.QuestionGuid,
                option => option.MapFrom(answer => answer.Question!.Guid));

        CreateMap<CreateAnswerRequestDto, Answer>();

        CreateMap<AnswerDto, Answer>();
    }
}