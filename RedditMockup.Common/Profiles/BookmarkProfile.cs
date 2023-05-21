using RedditMockup.Common.Dtos;
using RedditMockup.Model.Entities;
using Profile = AutoMapper.Profile;

namespace RedditMockup.Common.Profiles;

public class BookmarkProfile : Profile
{
    public BookmarkProfile() =>
        CreateMap<Bookmark, BookmarkDto>()
            .ReverseMap();

}

