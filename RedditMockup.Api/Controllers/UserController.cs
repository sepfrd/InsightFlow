using Microsoft.AspNetCore.Authorization;
using RedditMockup.Api.Base;
using RedditMockup.Business.Contracts;
using RedditMockup.Common.Constants;
using RedditMockup.Common.Dtos;
using RedditMockup.Model.Entities;

namespace RedditMockup.Api.Controllers;

[Authorize(Policy = PolicyConstants.Admin)]
public class UserController : BaseController<User, UserDto>
{
    public UserController(IBaseBusiness<User, UserDto> business) : base(business)
    {
    }
}
