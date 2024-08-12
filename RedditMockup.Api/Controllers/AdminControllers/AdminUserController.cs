using Microsoft.AspNetCore.Mvc;
using RedditMockup.Api.Base;
using RedditMockup.Business.Contracts;
using RedditMockup.Common.Dtos;
using RedditMockup.Model.Entities;

namespace RedditMockup.Api.Controllers.AdminControllers;

[ApiController]
[Route("api/admin/users")]
public class AdminUserController : AdminBaseController<User, UserDto>
{
    public AdminUserController(IAdminBaseBusiness<User, UserDto> business) : base(business)
    {
    }
}