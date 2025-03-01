using InsightFlow.Application.Features.BlogPosts.Commands;
using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Application.Features.BlogPosts.Queries;
using InsightFlow.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InsightFlow.Api.Controllers;

[ApiController]
[Route("blog-posts")]
public class BlogPostController : ControllerBase
{
    private readonly ISender _sender;

    public BlogPostController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<ActionResult<DomainResponse<BlogPostResponseDto>>> GetSingleBlogPostAsync([FromRoute] Guid uuid, CancellationToken cancellationToken)
    {
        var request = new GetSingleBlogPostQuery(uuid);

        var response = await _sender.Send(request, cancellationToken);

        return response;
    }

    [HttpPost]
    public async Task<ActionResult<DomainResponse<BlogPostResponseDto>>> CreateBlogPostAsync([FromBody] CreateBlogPostCommand request, CancellationToken cancellationToken)
    {
        var response = await _sender.Send(request, cancellationToken);

        return response;
    }
}