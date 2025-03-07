using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Common.Resources;
using InsightFlow.Application.Interfaces;
using InsightFlow.Domain.Common;
using InsightFlow.Domain.Entities;
using InsightFlow.Infrastructure.Common.Constants;
using InsightFlow.Infrastructure.Common.Dtos;
using InsightFlow.Infrastructure.Common.Helpers;
using InsightFlow.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace InsightFlow.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(
        IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor,
        IUnitOfWork unitOfWork)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
        _unitOfWork = unitOfWork;
    }

    public async Task<DomainResponse<string>> AuthenticateAsync(LoginDto loginDto, CancellationToken cancellationToken = default)
    {
        if (IsSignedIn())
        {
            return DomainResponse<string>.CreateFailure(DomainErrors.BadRequest, ApplicationMessages.AlreadySignedIn);
        }

        var user = await ValidateAndGetUserByCredentialsAsync(loginDto, cancellationToken);

        if (user is null)
        {
            return DomainResponse<string>.CreateFailure(DomainErrors.BadRequest, ApplicationMessages.InvalidCredentials);
        }

        var roles = await _unitOfWork
            .RoleRepository
            .GetAllAsync(filter: role => user.UserRoles.Select(userRole => userRole.RoleId).Contains(role.Id),
                disableTracking: true, cancellationToken: cancellationToken);

        var jwt = CreateJwt(user, roles);

        if (jwt is null)
        {
            return DomainResponse<string>.CreateFailure(DomainErrors.InternalServerError, ApplicationMessages.InternalServerError);
        }

        return new DomainResponse<string>(jwt);
    }

    public string GetSignedInUserUuid() =>
        _httpContextAccessor
            .HttpContext?
            .User
            .Claims
            .First(claim => claim.Type == ApplicationConstants.UuidClaim)
            .Value!;

    private async Task<User?> ValidateAndGetUserByCredentialsAsync(LoginDto loginDto, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.UserRepository.GetOneAsync(
            user => user.Username == loginDto.Username,
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
            new Claim(ApplicationConstants.UuidClaim, user.Uuid.ToString()),
            new Claim(ApplicationConstants.UsernameClaim, user.Username)
        });

        foreach (var role in roles)
        {
            var claim = new Claim(ClaimTypes.Role, role.Title);

            claims.AddClaim(claim);
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claims,
            Expires = DateTime.Now.AddDays(1d),
            SigningCredentials = signingCredentials
        };

        var jwtHandler = new JwtSecurityTokenHandler();

        var token = jwtHandler.CreateToken(tokenDescriptor);

        var jwt = jwtHandler.WriteToken(token);

        return jwt;
    }

    private bool IsSignedIn() =>
        _httpContextAccessor.HttpContext!.User.Identity is not null && _httpContextAccessor.HttpContext!.User.Identity.IsAuthenticated;
}