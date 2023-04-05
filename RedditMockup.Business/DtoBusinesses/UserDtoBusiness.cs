using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using RedditMockup.Business.Base;
using RedditMockup.Business.Contracts;
using RedditMockup.Common.Dtos;
using RedditMockup.Model.Entities;
using Sieve.Models;
using System.Net;

namespace RedditMockup.Business.DtoBusinesses;

public class UserDtoBusiness : DtoBaseBusiness<UserDto, User>
{
    public UserDtoBusiness(IMapper mapper) : base(mapper)
    {
    }

    private async Task<bool> UsernameExistsAsync(string username, CancellationToken cancellationToken = new())
    {
        SieveModel sieveModel = new()
        {
            Filters = $"Username=={username}"
        };

        var usersResponse = await LoadAllAsync(sieveModel, cancellationToken);

        if (usersResponse.Data.IsNullOrEmpty())
        {
            return false;
        }

        return true;
    }

    public override async Task<CustomResponse<UserDto>> CreateAsync(UserDto dto,
        CancellationToken cancellationToken = default)
    {
        var isUsernameValid = !await UsernameExistsAsync(dto.Username!, cancellationToken);

        if (!isUsernameValid)
        {
            return new()
            {
                IsSuccess = false,
                Message = $"{dto.Username} is already used, try another one",
                HttpStatusCode = HttpStatusCode.PreconditionFailed
            };
        }

        dto.Password = await dto.Password!.GetHashStringAsync();

        var user = Mapper.Map<User>(dto);

        user.Profile = new()
        {
            UserId = user.Id
        };

        var userRole = new UserRole
        {
            UserId = user.Id,
            RoleId = 2
        };

        user.UserRoles!.Add(userRole);

        return await CreateAsync(user, cancellationToken);
    }


}
