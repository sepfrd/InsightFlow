using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace RedditMockup.IntegrationTest.Common.AuthMockHelpers;

public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly string _role;

    public TestAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        string role)
        : base(options, logger, encoder)
    {
        _role = role;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        Claim[] claims =
        [
            new Claim(ClaimTypes.NameIdentifier, Constants.TestUsername),
            new Claim(ClaimTypes.Name, Constants.TestUsername),
            new Claim(ClaimTypes.Role, _role) // Use the role provided during setup
        ];

        var identity = new ClaimsIdentity(claims, Constants.TestAuthSchemeName);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Constants.TestAuthSchemeName);

        var result = AuthenticateResult.Success(ticket);

        return Task.FromResult(result);
    }
}