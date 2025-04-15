using InsightFlow.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InsightFlow.Application.Features.Users.Commands.UpdateProfileImage;

public record UpdateProfileImageCommand(Guid Uuid, IFormFile ImageFile) : IRequest<DomainResponse>;