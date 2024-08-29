using InsightFlow.Common.Dtos;
using InsightFlow.Model.Entities;
using Profile = AutoMapper.Profile;

namespace InsightFlow.Common.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ReverseMap();

        CreateMap<User, UserWithBioDto>()
            .ForMember(userWithBioDto => userWithBioDto.Bio,
                option => option.MapFrom(user => user.Profile!.Bio));
    }
}