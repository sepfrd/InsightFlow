using Humanizer;
using InsightFlow.Api.Common.Dtos.Requests;
using InsightFlow.Application.Features.BlogPosts.Commands.CreateBlogPost;
using InsightFlow.Application.Features.BlogPosts.Commands.UpdateBlogPost;
using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Application.Features.BlogPosts.Queries.GetAllBlogPostsByFilter;
using InsightFlow.Application.Features.BlogPosts.Queries.GetSingleBlogPost;
using InsightFlow.Application.Features.BlogPosts.Queries.GetUserBlogPosts;
using InsightFlow.Application.Features.Users.Queries.GetUserIdByUserUuid;
using InsightFlow.Application.Interfaces;
using InsightFlow.Common.Constants;
using InsightFlow.Domain.Common;
using InsightFlow.Infrastructure.Common.Constants;
using InsightFlow.Infrastructure.Common.Dtos;
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
    private readonly IDataValidator<PaginationDto> _paginationDtoValidator;

    public BlogPostController(
        ISender sender,
        IAuthService authService,
        IDataValidator<PaginationDto> paginationDtoValidator)
    {
        _sender = sender;
        _authService = authService;
        _paginationDtoValidator = paginationDtoValidator;
    }

    [HttpPost]
    [Authorize(InfrastructureConstants.UserPolicyName)]
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
        [FromQuery] PaginationDto pagination,
        [FromQuery] string? title,
        [FromQuery] string? body,
        [FromQuery] Guid? authorUuid,
        CancellationToken cancellationToken)
    {
        var paginationDtoValidationResult = await _paginationDtoValidator.ValidateAsync(pagination, cancellationToken);

        if (!paginationDtoValidationResult.IsValid)
        {
            return BadRequest(PaginatedDomainResponse<IEnumerable<BlogPostResponseDto>>
                .CreateFailure(
                    string.Join(Environment.NewLine, paginationDtoValidationResult.ValidationErrors),
                    StatusCodes.Status400BadRequest));
        }

        var filterDto = new BlogPostFilterDto
        {
            Title = title,
            Body = body
        };

        if (authorUuid.HasValue)
        {
            var userRequest = new GetUserIdByUserUuidQuery(authorUuid.Value);

            var userIdResponse = await _sender.Send(userRequest, cancellationToken);

            if (!userIdResponse.IsSuccess)
            {
                var message = string.Format(
                    StringConstants.EntityNotFoundByUuidTemplate,
                    nameof(User).Humanize(LetterCasing.LowerCase),
                    authorUuid);

                return BadRequest(PaginatedDomainResponse<IEnumerable<BlogPostResponseDto>>
                    .CreateFailure(message, StatusCodes.Status400BadRequest));
            }

            filterDto.AuthorId = userIdResponse.Data;
        }

        var request = new GetAllBlogPostsByFilterQuery(filterDto, pagination.PageNumber, pagination.PageSize);

        var response = await _sender.Send(request, cancellationToken);

        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    [Route("users/blog-posts")]
    [Authorize(InfrastructureConstants.UserPolicyName)]
    public async Task<ActionResult<PaginatedDomainResponse<IEnumerable<BlogPostResponseDto>>>> GetAllCurrentUserBlogPostsAsync(
        [FromQuery] PaginationDto pagination,
        CancellationToken cancellationToken)
    {
        var paginationDtoValidationResult = await _paginationDtoValidator.ValidateAsync(pagination, cancellationToken);

        if (!paginationDtoValidationResult.IsValid)
        {
            return BadRequest(PaginatedDomainResponse<IEnumerable<BlogPostResponseDto>>
                .CreateFailure(
                    string.Join(Environment.NewLine, paginationDtoValidationResult.ValidationErrors),
                    StatusCodes.Status400BadRequest));
        }

        var stringUuid = _authService.GetSignedInUserUuid();

        var request = new GetUserBlogPostsQuery(Guid.Parse(stringUuid), pagination.PageNumber, pagination.PageSize);

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