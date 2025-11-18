using InsightFlow.Common.Cqrs.Commands;
using InsightFlow.Domain.Common;
using Microsoft.AspNetCore.Http;

namespace InsightFlow.Application.Features.Users.Commands.UpdateProfileImage;

public record UpdateProfileImageCommand(Guid Uuid, IFormFile ImageFile) : ICommand<DomainResponse>;