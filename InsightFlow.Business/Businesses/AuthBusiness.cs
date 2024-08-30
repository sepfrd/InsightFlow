using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using InsightFlow.Business.Interfaces;
using InsightFlow.Common.Constants;
using InsightFlow.Common.Dtos;
using InsightFlow.Common.Dtos.CustomResponses;
using InsightFlow.Common.Helpers;
using InsightFlow.DataAccess.Interfaces;
using InsightFlow.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sieve.Models;

namespace InsightFlow.Business.Businesses;

public partial class AuthBusiness : IAuthBusiness
{
    private readonly IBaseRepository<User> _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;

    public AuthBusiness(
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration)
    {
        _userRepository = unitOfWork.UserRepository;
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
    }

    public string GetSignedInUserExternalId() =>
        _httpContextAccessor.HttpContext?.User.FindFirstValue(ApplicationConstants.ExternalIdClaim)!;

    public async Task<CustomResponse<string>> LoginAsync(LoginDto loginDto, CancellationToken cancellationToken = default)
    {
        if (IsSignedIn())
        {
            return CustomResponse<string>.CreateUnsuccessfulResponse(HttpStatusCode.BadRequest, MessageConstants.AlreadySignedInMessage);
        }

        var user = await ValidateAndGetUserByCredentialsAsync(loginDto, cancellationToken);

        if (user is null)
        {
            return CustomResponse<string>.CreateUnsuccessfulResponse(HttpStatusCode.BadRequest, MessageConstants.InvalidCredentialsMessage);
        }

        var jwt = CreateJwt(user)!;

        return CustomResponse<string>.CreateSuccessfulResponse(jwt, MessageConstants.SuccessfulLoginMessage);
    }

    private async Task<User?> ValidateAndGetUserByCredentialsAsync(LoginDto loginDto, CancellationToken cancellationToken = default)
    {
        User? user;

        if (UsernameRegex().IsMatch(loginDto.UsernameOrEmail))
        {
            user = await GetByUsernameAsync(loginDto.UsernameOrEmail, cancellationToken);
        }
        else
        {
            user = await GetByEmailAsync(loginDto.UsernameOrEmail, cancellationToken);
        }

        if (user is null)
        {
            return null;
        }

        var isPasswordValid = PasswordHelper.ValidatePassword(loginDto.Password, user.Password);

        return !isPasswordValid ? null : user;
    }

    private bool IsSignedIn() =>
        _httpContextAccessor.HttpContext!.User.Identity is not null && _httpContextAccessor.HttpContext!.User.Identity.IsAuthenticated;

    private async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        var sieveModel = new SieveModel
        {
            Filters = nameof(User.Username) + "==" + username,
            Page = 1,
            PageSize = 1
        };

        var result = await _userRepository.GetAllAsync(
            sieveModel,
            users => users
                .Include(user => user.UserRoles)
                .ThenInclude(userRole => userRole.Role), cancellationToken);

        return result.Entities?.SingleOrDefault();
    }

    private async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var sieveModel = new SieveModel
        {
            Filters = nameof(User.Email) + "==" + email,
            Page = 1,
            PageSize = 1
        };

        var result = await _userRepository.GetAllAsync(
            sieveModel,
            users => users
                .Include(user => user.UserRoles)
                .ThenInclude(userRole => userRole.Role), cancellationToken);

        return result.Entities?.SingleOrDefault();
    }

    private string? CreateJwt(User user)
    {
        var serverUrl = _configuration
            .GetSection(ApplicationConstants.ApplicationUrlsConfigurationSectionKey)
            .GetValue<string>(ApplicationConstants.ServerUrlConfigurationKey)!;

        var clientUrl = _configuration.GetSection(ApplicationConstants.ApplicationUrlsConfigurationSectionKey)
            .GetValue<string>(ApplicationConstants.ClientUrlConfigurationKey)!;

        var privateKey = _configuration
            .GetSection(ApplicationConstants.JwtConfigurationSectionKey)
            .GetValue<string>(ApplicationConstants.JwtPrivateKeyConfigurationKey);

        var rsa = RSA.Create();

        rsa.ImportFromPem(privateKey);

        var securityKey = new RsaSecurityKey(rsa);

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha512Signature);

        var claims = new ClaimsIdentity(new[]
        {
            new Claim(JwtRegisteredClaimNames.Iss, serverUrl),
            new Claim(JwtRegisteredClaimNames.Aud, clientUrl),
            new Claim(
                JwtRegisteredClaimNames.Iat,
                DateTime.Now.ToUniversalTime().ToString(CultureInfo.InvariantCulture)),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ApplicationConstants.UsernameClaim, user.Username),
            new Claim(ApplicationConstants.ExternalIdClaim, user.Guid.ToString())
        });

        var roles = user.UserRoles.Select(userRole => userRole.Role).ToList();

        foreach (var role in roles)
        {
            var claim = new Claim(ClaimTypes.Role, role?.Name!);

            claims.AddClaim(claim);
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claims,
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = signingCredentials
        };

        var jwtHandler = new JwtSecurityTokenHandler();

        var token = jwtHandler.CreateToken(tokenDescriptor);

        var jwt = jwtHandler.WriteToken(token);

        return jwt;
    }

    [GeneratedRegex(RegexPatternConstants.UsernameRegexPattern)]
    private static partial Regex UsernameRegex();
}