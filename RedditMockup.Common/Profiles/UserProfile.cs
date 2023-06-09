using RedditMockup.Common.Dtos;
using RedditMockup.Model.Entities;
using Profile = AutoMapper.Profile;

namespace RedditMockup.Common.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(destination => destination.FirstName,
                option => option.MapFrom(source => source.Person!.FirstName))
            .ForMember(destination => destination.LastName,
                option => option.MapFrom(source => source.Person!.LastName));

        CreateMap<UserDto, User>();
    }
}