using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using RedditMockup.Api.Base;
using RedditMockup.Business.Contracts;
using RedditMockup.Common.Dtos;
using RedditMockup.Model.Entities;

namespace RedditMockup.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : BaseController<User, UserDto>
{
    public UserController(IBaseBusiness<User, UserDto> business) : base(business)
    {
    }
    
    [HttpOptions]
    public new IActionResult Options()
    {
        Response
            .Headers
            .Add(new KeyValuePair<string, StringValues>("Allow", $"{HttpMethods.Post},{HttpMethods.Get},{HttpMethods.Put},{HttpMethods.Delete}"));

        return Ok();
    }
}