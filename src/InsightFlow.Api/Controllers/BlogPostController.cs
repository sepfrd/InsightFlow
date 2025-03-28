using InsightFlow.Api.Common.Dtos.Requests;
using InsightFlow.Application.Features.BlogPosts.Commands.CreateBlogPost;
using InsightFlow.Application.Features.BlogPosts.Commands.UpdateBlogPost;
using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Application.Features.BlogPosts.Queries.GetAllBlogPostsByFilter;
using InsightFlow.Application.Features.BlogPosts.Queries.GetSingleBlogPost;
using InsightFlow.Application.Features.BlogPosts.Queries.GetUserBlogPosts;
using InsightFlow.Domain.Common;
using InsightFlow.Infrastructure.Common.Constants;
using InsightFlow.Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InsightFlow.Api.Controllers;

[ApiController]
[Route("api/blog-posts")]
public class BlogPostController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IAuthService _authService;

    public BlogPostController(ISender sender, IAuthService authService)
    {
        _sender = sender;
        _authService = authService;
    }

    [HttpPost]
    [Authorize(ApplicationConstants.UserPolicyName)]
    public async Task<ActionResult<DomainResponse<BlogPostResponseDto>>> CreateBlogPostAsync([FromBody] CreateBlogPostRequestDto request, CancellationToken cancellationToken)
    {
        var signedInUserUuid = _authService.GetSignedInUserUuid();

        var command = new CreateBlogPostCommand(
            request.Title,
            request.Body,
            Guid.Parse(signedInUserUuid));

        var result = await _sender.Send(command, cancellationToken);

        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedDomainResponse<IEnumerable<BlogPostResponseDto>>>> GetAllBlogPostsByFilterAsync(
        [FromQuery] uint pageNumber,
        [FromQuery] uint pageSize,
        [FromQuery] BlogPostFilterDto filterDto,
        CancellationToken cancellationToken)
    {
        var request = new GetAllBlogPostsByFilterQuery(filterDto, pageNumber, pageSize);

        var response = await _sender.Send(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
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

        var request = new GetUserBlogPostsQuery(Guid.Parse(stringUuid), pageNumber, pageSize);

        var response = await _sender.Send(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    [Route("{uuid:guid}")]
    public async Task<ActionResult<DomainResponse<BlogPostResponseDto>>> GetSingleBlogPostAsync([FromRoute] Guid uuid, CancellationToken cancellationToken)
    {
        var request = new GetSingleBlogPostQuery(uuid);

        var response = await _sender.Send(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

    [HttpPatch]
    [Authorize]
    public async Task<ActionResult<DomainResponse<BlogPostResponseDto>>> UpdateBlogPostAsync([FromBody] UpdateBlogPostRequestDto request, CancellationToken cancellationToken)
    {
        var signedInUserUuid = _authService.GetSignedInUserUuid();

        var command = new UpdateBlogPostCommand(
            Guid.Parse(signedInUserUuid),
            request.BlogPostUuid,
            request.NewTitle,
            request.NewBody);

        var response = await _sender.Send(command, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }
}