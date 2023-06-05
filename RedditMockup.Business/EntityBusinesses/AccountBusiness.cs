using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RedditMockup.Common.Dtos;
using RedditMockup.Common.Helpers;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.DataAccess.Repositories;
using RedditMockup.Model.Entities;
using Sieve.Models;
using System.Net;
using System.Security.Claims;

namespace RedditMockup.Business.EntityBusinesses;

public class AccountBusiness
{
    #region [Fields]

    private readonly UserRepository _userRepository;

    #endregion

    #region [Constructor]

    public AccountBusiness(IUnitOfWork unitOfWork) =>
        _userRepository = unitOfWork.UserRepository!;

    #endregion
    
    #region [Private Methods]

    private async Task<User?> ValidateAndGetUserByCredentialsAsync(LoginDto loginDto,
        CancellationToken cancellationToken = default)
    {
        var user = await LoadByUsernameAsync(loginDto.Username!, cancellationToken);

        if (user is null)
        {
            return null;
        }

        var password = await loginDto.Password!.GetHashStringAsync();

        var isPasswordValid = password == user.Password;

        return !isPasswordValid ? null : user;

    }

    private static bool IsSignedIn(HttpContext httpContext) =>
        httpContext.User.Identity is not null && httpContext.User.Identity.IsAuthenticated;

    private async Task<User?> LoadByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        SieveModel sieveModel = new()
        {
            Filters = $"Username=={username}"
        };

        var users = await _userRepository.GetAllAsync(sieveModel,
            users => users.Include(x => x.Person)
                .Include(x => x.Profile)
                .Include(x => x.Questions)
                .Include(x => x.Answers)
                .Include(x => x.UserRoles)!
                .ThenInclude(x => x.Role), cancellationToken);

        return users.Count == 0 ? null : users.Single();
    }

    #endregion
    
    #region [Public Methods]
    
    public async Task<CustomResponse> LoginAsync(LoginDto login, HttpContext httpContext,
        CancellationToken cancellationToken = default)
    {
        if (IsSignedIn(httpContext))
        {
            return new CustomResponse
            {
                IsSuccess = false,
                Message = "You are already signed in",
                HttpStatusCode = HttpStatusCode.BadRequest
            };
        }

        var user = await ValidateAndGetUserByCredentialsAsync(login, cancellationToken);

        if (user is null)
        {
            return new CustomResponse
            {
                IsSuccess = false,
                Message = "Username and/or password not correct",
                HttpStatusCode = HttpStatusCode.BadRequest
            };
        }

        var roles = user.UserRoles!.Select(userRole => userRole.Role).ToList();

        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        claims.AddRange(roles.Select(role => new Claim(role?.Title!, role?.Title!)));

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var principal = new ClaimsPrincipal(identity);

        var properties = new AuthenticationProperties
        {
            IsPersistent = login.RememberMe
        };

        await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);

        return new CustomResponse
        {
            IsSuccess = true,
            Message = "Successfully logged in",
            HttpStatusCode = HttpStatusCode.OK
        };
    }

    public static async Task<CustomResponse> LogoutAsync(HttpContext httpContext)
    {
        if (!IsSignedIn(httpContext))
        {
            return new CustomResponse
            {
                IsSuccess = false,
                Message = "Already logged out",
                HttpStatusCode = HttpStatusCode.BadRequest
            };
        }

        await httpContext.SignOutAsync();

        return new CustomResponse
        {
            IsSuccess = true,
            Message = "Successfully logged out",
            HttpStatusCode = HttpStatusCode.OK
        };
    }

    #endregion
}