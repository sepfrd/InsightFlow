using System.Net;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using InsightFlow.Api.ExceptionHandlers;
using InsightFlow.Api.Transformers;
using InsightFlow.Application.Features.BlogPosts.Commands.CreateBlogPost;
using InsightFlow.Domain.Common;
using InsightFlow.Infrastructure.Common.Configurations;
using InsightFlow.Infrastructure.Common.Constants;
using InsightFlow.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Serilog;

namespace InsightFlow.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        var appOptions = configuration.Get<AppOptions>()!;

        services
            .AddOpenApi(options =>
                options
                    .AddDocumentTransformer<BearerSecuritySchemeTransformer>()
                    .AddDocumentTransformer<DocumentInfoTransformer>())
            .AddHttpContextAccessor()
            .AddSingleton(Options.Create(appOptions))
            .AddInfrastructure(appOptions, environment)
            .AddMediatR(config => config.RegisterServicesFromAssemblyContaining<CreateBlogPostCommandHandler>())
            .AddAuth(appOptions)
            .AddRateLimiters(appOptions)
            .AddCors(appOptions)
            .AddSerilog(configuration)
            .AddExceptionHandler<GlobalExceptionHandler>()
            .AddProblemDetails()
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        services.AddHealthChecks();

        return services;
    }

    public static IServiceCollection AddAuth(
        this IServiceCollection services, AppOptions appOptions) =>
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var rsa = RSA.Create();

                rsa.ImportFromPem(appOptions.JwtOptions!.PublicKey);

                var securityKey = new RsaSecurityKey(rsa);

                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    IssuerSigningKey = securityKey,
                    ValidIssuer = appOptions.ApplicationInformation!.ServerUrl!,
                    ValidAudience = appOptions.ApplicationInformation.ClientUrl!
                };
            })
            .Services
            .AddAuthorization(options =>
            {
                options.AddPolicy(InfrastructureConstants.AdminPolicyName,
                    policy => policy.RequireRole(DomainConstants.AdminRoleTitle));
                options.AddPolicy(InfrastructureConstants.UserPolicyName,
                    policy => policy.RequireRole(DomainConstants.BasicUserRoleTitle));
            });

    public static IServiceCollection AddRateLimiters(this IServiceCollection services, AppOptions appOptions) =>
        services
            .AddRateLimiter(limiterOptions =>
            {
                limiterOptions.AddFixedWindowLimiter(
                    InfrastructureConstants.FixedWindowRateLimiterPolicy,
                    fixedWindowOptions =>
                    {
                        fixedWindowOptions.PermitLimit = appOptions.RateLimitersConfiguration!.FixedWindowRateLimiterOptions!.PermitLimit;
                        fixedWindowOptions.Window = TimeSpan.FromSeconds(appOptions.RateLimitersConfiguration.FixedWindowRateLimiterOptions.WindowSeconds);
                        fixedWindowOptions.QueueLimit = appOptions.RateLimitersConfiguration.FixedWindowRateLimiterOptions.QueueLimit;
                        fixedWindowOptions.QueueProcessingOrder = appOptions.RateLimitersConfiguration.FixedWindowRateLimiterOptions.QueueProcessingOrder;
                        fixedWindowOptions.AutoReplenishment = appOptions.RateLimitersConfiguration.FixedWindowRateLimiterOptions.AutoReplenishment;
                    });

                limiterOptions.AddConcurrencyLimiter(
                    InfrastructureConstants.ConcurrencyRateLimiterPolicy,
                    concurrencyOptions =>
                    {
                        concurrencyOptions.PermitLimit = appOptions.RateLimitersConfiguration!.ConcurrencyLimiterOptions!.PermitLimit;
                        concurrencyOptions.QueueLimit = appOptions.RateLimitersConfiguration.ConcurrencyLimiterOptions.QueueLimit;
                        concurrencyOptions.QueueProcessingOrder = appOptions.RateLimitersConfiguration.ConcurrencyLimiterOptions.QueueProcessingOrder;
                    });

                limiterOptions.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, IPAddress>(context =>
                {
                    var remoteIpAddress = context.Connection.RemoteIpAddress;

                    if (remoteIpAddress is null || IPAddress.IsLoopback(remoteIpAddress))
                    {
                        return RateLimitPartition.GetNoLimiter(IPAddress.Loopback);
                    }

                    return RateLimitPartition.GetTokenBucketLimiter(
                        remoteIpAddress,
                        _ => new TokenBucketRateLimiterOptions
                        {
                            AutoReplenishment = appOptions.RateLimitersConfiguration!.TokenBucketRateLimiterOptions!.AutoReplenishment,
                            QueueLimit = appOptions.RateLimitersConfiguration.TokenBucketRateLimiterOptions.QueueLimit,
                            QueueProcessingOrder = appOptions.RateLimitersConfiguration.TokenBucketRateLimiterOptions.QueueProcessingOrder,
                            ReplenishmentPeriod = TimeSpan.FromSeconds(appOptions.RateLimitersConfiguration.TokenBucketRateLimiterOptions.ReplenishmentPeriodSeconds),
                            TokenLimit = appOptions.RateLimitersConfiguration.TokenBucketRateLimiterOptions.TokenLimit,
                            TokensPerPeriod = appOptions.RateLimitersConfiguration.TokenBucketRateLimiterOptions.TokensPerPeriod
                        });
                });

                limiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            });

    public static IServiceCollection AddSerilog(this IServiceCollection services, IConfiguration configuration) =>
        services.AddSerilog(loggerConfiguration =>
            loggerConfiguration.ReadFrom.Configuration(configuration));

    public static IServiceCollection AddCors(this IServiceCollection services, AppOptions appOptions) =>
        services.AddCors(options =>
        {
            options.AddPolicy(InfrastructureConstants.RestrictedCorsPolicy, builder =>
            {
                builder
                    .WithMethods(HttpMethods.Post, HttpMethods.Get, HttpMethods.Put, HttpMethods.Delete, HttpMethods.Options)
                    .WithHeaders(
                        HeaderNames.Accept,
                        HeaderNames.ContentType,
                        HeaderNames.Authorization)
                    .AllowCredentials()
                    .SetIsOriginAllowed(origin =>
                    {
                        if (string.IsNullOrWhiteSpace(origin))
                        {
                            return false;
                        }

                        if (origin.StartsWith(appOptions.ApplicationInformation!.ServerUrl!, StringComparison.CurrentCultureIgnoreCase) ||
                            origin.StartsWith(appOptions.ApplicationInformation.ClientUrl!, StringComparison.CurrentCultureIgnoreCase))
                        {
                            return true;
                        }

                        return false;
                    });
            });

            options.AddPolicy(InfrastructureConstants.AllowAnyOriginCorsPolicy, builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
}