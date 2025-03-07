using InsightFlow.Application.Features.BlogPosts.Commands;
using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Application.Features.BlogPosts.Queries;
using InsightFlow.Domain.Common;
using InsightFlow.Infrastructure.Common.Constants;
using InsightFlow.Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsightFlow.Api.Controllers;

[ApiController]
[Route("blog-posts")]
public class BlogPostController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IAuthService _authService;

    public BlogPostController(ISender sender, IAuthService authService)
    {
        _sender = sender;
        _authService = authService;
    }

    [HttpGet]
    [Route("users/blog-posts")]
    [Authorize(ApplicationConstants.UserPolicyName)]
    public async Task<ActionResult<PaginatedDomainResponse<IEnumerable<BlogPostResponseDto>>>> GetAllCurrentUserBlogPostsAsync(
        [FromQuery] uint pageNumber,
        [FromQuery] uint pageSize,
        CancellationToken cancellationToken)
    {
        var stringUuid = _authService.GetSignedInUserUuid();

        var request = new GetUserBlogPostsQuery(Guid.Parse(stringUuid));

        var response = await _sender.Send(request, cancellationToken);

        return response;
    }

    [HttpGet]
    [Route("{uuid:guid}")]
    public async Task<ActionResult<DomainResponse<BlogPostResponseDto>>> GetSingleBlogPostAsync([FromRoute] Guid uuid, CancellationToken cancellationToken)
    {
        var request = new GetSingleBlogPostQuery(uuid);

        var response = await _sender.Send(request, cancellationToken);

        return response;
    }

    [HttpPost]
    [Authorize(ApplicationConstants.UserPolicyName)]
    public async Task<ActionResult<DomainResponse<BlogPostResponseDto>>> CreateBlogPostAsync([FromBody] CreateBlogPostCommand request, CancellationToken cancellationToken)
    {
        var response = await _sender.Send(request, cancellationToken);

        return response;
    }
}