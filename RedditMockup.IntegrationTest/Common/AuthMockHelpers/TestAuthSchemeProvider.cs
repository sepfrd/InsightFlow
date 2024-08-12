using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace RedditMockup.IntegrationTest.Common.AuthMockHelpers;

public class TestAuthSchemeProvider : AuthenticationSchemeProvider
{

    public TestAuthSchemeProvider(IOptions<AuthenticationOptions> options)
        : base(options)
    {
    }

    protected TestAuthSchemeProvider(
        IOptions<AuthenticationOptions> options,
        IDictionary<string, AuthenticationScheme> schemes
    )
        : base(options, schemes)
    {
    }

    public override Task<AuthenticationScheme?> GetDefaultAuthenticateSchemeAsync()
    {
        var scheme = new AuthenticationScheme(
            Constants.TestAuthSchemeName,
            Constants.TestAuthSchemeName,
            typeof(TestAuthHandler)
        );

        return Task.FromResult(scheme)!;
    }
}