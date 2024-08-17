using InsightFlow.Common.Dtos;
using InsightFlow.Model.Entities;
using Profile = AutoMapper.Profile;

namespace InsightFlow.Common.Profiles;

public class AnswerProfile : Profile
{
    public AnswerProfile()
    {
        CreateMap<Answer, AnswerDto>()
            .ForMember(answerDto => answerDto.UserGuid,
                option => option.MapFrom(answer => answer.User!.Guid))
            .ForMember(answerDto => answerDto.QuestionGuid,
                option => option.MapFrom(answer => answer.Question!.Guid));

        CreateMap<AnswerDto, Answer>();
    }
}