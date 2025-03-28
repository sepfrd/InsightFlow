using Humanizer;
using InsightFlow.Application.Interfaces;
using InsightFlow.Common.Constants;
using InsightFlow.Domain.Common;
using InsightFlow.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace InsightFlow.Application.Features.Users.Queries.GetUserIdByUserUuid;

public class GetUserIdByUserUuidQueryHandler : IRequestHandler<GetUserIdByUserUuidQuery, DomainResponse<long?>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserIdByUserUuidQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DomainResponse<long?>> Handle(GetUserIdByUserUuidQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetOneAsync(
            user => user.Uuid == request.Uuid,
            disableTracking: true,
            cancellationToken: cancellationToken);

        if (user is not null)
        {
            return DomainResponse<long?>.CreateSuccess(null, StatusCodes.Status200OK, user.Id);
        }

        var message = string.Format(
            StringConstants.EntityNotFoundByUuidTemplate,
            nameof(User).Humanize(LetterCasing.LowerCase),
            request.Uuid);

        return DomainResponse<long?>.CreateFailure(message, StatusCodes.Status404NotFound);
    }
}