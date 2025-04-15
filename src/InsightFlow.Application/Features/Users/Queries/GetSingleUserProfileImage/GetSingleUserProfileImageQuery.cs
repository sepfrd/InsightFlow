using InsightFlow.Application.Features.Users.Dtos;
using InsightFlow.Domain.Common;
using MediatR;

namespace InsightFlow.Application.Features.Users.Queries.GetSingleUserProfileImage;

public record GetSingleUserProfileImageQuery(Guid UserUuid) : IRequest<DomainResponse<ProfileImageResponseDto>>;