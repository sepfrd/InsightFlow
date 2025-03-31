using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
using InsightFlow.Application.Interfaces;
using InsightFlow.Common.Constants;
using InsightFlow.Domain.Common;
using InsightFlow.Domain.Entities;
using InsightFlow.Infrastructure.Common.Configurations;
using InsightFlow.Infrastructure.Common.Constants;
using InsightFlow.Infrastructure.Common.Dtos;
using InsightFlow.Infrastructure.Common.Helpers;
using InsightFlow.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace InsightFlow.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AuthService> _logger;
    private readonly AppOptions _appOptions;

    public AuthService(
        IHttpContextAccessor httpContextAccessor,
        IUnitOfWork unitOfWork,
        ILogger<AuthService> logger,
        IOptions<AppOptions> appOptions)
    {
        _httpContextAccessor = httpContextAccessor;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _appOptions = appOptions.Value;
    }

    public async Task<DomainResponse<string>> AuthenticateAsync(LoginDto loginDto, CancellationToken cancellationToken = default)
    {
        if (IsSignedIn())
        {
            return DomainResponse<string>.CreateFailure(StringConstants.AlreadySignedIn, StatusCodes.Status400BadRequest);
        }

        var user = await ValidateAndGetUserByCredentialsAsync(loginDto, cancellationToken);

        if (user is null)
        {
            return DomainResponse<string>.CreateFailure(StringConstants.InvalidCredentials, StatusCodes.Status400BadRequest);
        }

        var rolesResponse = await _unitOfWork
            .RoleRepository
            .GetAllAsync(filter: role => user.UserRoles.Select(userRole => userRole.RoleId).Contains(role.Id),
                disableTracking: true, cancellationToken: cancellationToken);

        if (!rolesResponse.IsSuccess ||
            rolesResponse.Data is null ||
            !rolesResponse.Data.Any())
        {
            _logger.LogCritical(StringConstants.InvalidPersistedDataErrorLogTemplate, typeof(Role));

            return DomainResponse<string>.CreateFailure(
                StringConstants.InternalServerError,
                StatusCodes.Status500InternalServerError);
        }

        var jwt = CreateJwt(user, rolesResponse.Data);

        if (jwt is not null)
        {
            return DomainResponse<string>.CreateSuccess(StringConstants.SuccessfulLogin, StatusCodes.Status200OK, jwt);
        }

        _logger.LogCritical(StringConstants.JwtCreationErrorLog);

        return DomainResponse<string>.CreateFailure(
            StringConstants.InternalServerError,
            StatusCodes.Status500InternalServerError);
    }

    public string GetSignedInUserUuid() =>
        _httpContextAccessor
            .HttpContext?
            .User
            .Claims
            .First(claim => claim.Type == InfrastructureConstants.UuidClaim)
            .Value!;

    private async Task<User?> ValidateAndGetUserByCredentialsAsync(LoginDto loginDto, CancellationToken cancellationToken = default)
    {
        Expression<Func<User, bool>>? userPredicate;

        if (RegexValidator.UsernameRegex().IsMatch(loginDto.UsernameOrEmail))
        {
            userPredicate = userEntity => userEntity.Username == loginDto.UsernameOrEmail;
        }
        else
        {
            userPredicate = userEntity => userEntity.Email == loginDto.UsernameOrEmail;
        }

        var user = await _unitOfWork.UserRepository.GetOneAsync(
            userPredicate,
            includes: [user => user.UserRoles],
            disableTracking: true,
            cancellationToken: cancellationToken);

        if (user is null)
        {
            return null;
        }

        var isPasswordValid = PasswordHelper.ValidatePassword(loginDto.Password, user.PasswordHash);

        return !isPasswordValid ? null : user;
    }

    private string? CreateJwt(User user, IEnumerable<Role> roles)
    {
        var rsa = RSA.Create();

        rsa.ImportFromPem(_appOptions.JwtOptions!.PrivateKey);

        var securityKey = new RsaSecurityKey(rsa);

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha512Signature);

        var claims = new ClaimsIdentity(new[]
        {
            new Claim(JwtRegisteredClaimNames.Iss, _appOptions.ApplicationInformation!.ServerUrl!),
            new Claim(JwtRegisteredClaimNames.Aud, _appOptions.ApplicationInformation.ClientUrl!),
            new Claim(
                JwtRegisteredClaimNames.Iat,
                DateTime.Now.ToUniversalTime().ToString(CultureInfo.InvariantCulture)),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(InfrastructureConstants.UuidClaim, user.Uuid.ToString()),
            new Claim(InfrastructureConstants.UsernameClaim, user.Username)
        });

        foreach (var role in roles)
        {
            var claim = new Claim(ClaimTypes.Role, role.Title);

            claims.AddClaim(claim);
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claims,
            Expires = DateTime.Now.AddMinutes(_appOptions.JwtOptions.TokenExpirationDurationMinutes),
            SigningCredentials = signingCredentials
        };

        var jwtHandler = new JwtSecurityTokenHandler();

        var token = jwtHandler.CreateToken(tokenDescriptor);

        var jwt = jwtHandler.WriteToken(token);

        return jwt;
    }

    public bool IsSignedIn() =>
        _httpContextAccessor.HttpContext!.User.Identity is not null && _httpContextAccessor.HttpContext!.User.Identity.IsAuthenticated;
}