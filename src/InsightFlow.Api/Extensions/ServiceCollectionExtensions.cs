using System.Net;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using InsightFlow.Api.ExceptionHandlers;
using InsightFlow.Api.Transformers;
using InsightFlow.Application.Features.BlogPosts.Commands.Handlers;
using InsightFlow.Domain.Common;
using InsightFlow.Infrastructure.Common.Constants;
using InsightFlow.Infrastructure.Common.Dtos.Configurations;
using InsightFlow.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Serilog;

namespace InsightFlow.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        services
            .AddOpenApi(options => options.AddDocumentTransformer<BearerSecuritySchemeTransformer>())
            .AddHttpContextAccessor()
            .Configure<JwtOptions>(configuration.GetSection(ApplicationConstants.JwtConfigurationSectionKey))
            .AddInfrastructure(configuration, environment)
            .AddMediatR(config => config.RegisterServicesFromAssemblyContaining<CreateBlogPostCommandHandler>())
            .AddAuth(configuration)
            .AddRateLimiters(configuration)
            .AddCors(configuration)
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

        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration) =>
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var serverUrl = configuration
                    .GetSection(ApplicationConstants.ApplicationUrlsConfigurationSectionKey)
                    .GetValue<string>(ApplicationConstants.ServerUrlConfigurationKey)!;

                var clientUrl = configuration.GetSection(ApplicationConstants.ApplicationUrlsConfigurationSectionKey)
                    .GetValue<string>(ApplicationConstants.ClientUrlConfigurationKey)!;

                var jwtConfiguration = configuration.GetRequiredSection(ApplicationConstants.JwtConfigurationSectionKey).Get<JwtOptions>()!;

                var rsa = RSA.Create();

                rsa.ImportFromPem(jwtConfiguration.PublicKey);

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
                    ValidIssuer = serverUrl,
                    ValidAudience = clientUrl
                };
            })
            .Services
            .AddAuthorization(options =>
            {
                options.AddPolicy(ApplicationConstants.AdminPolicyName,
                    policy => policy.RequireRole(DomainConstants.AdminRoleTitle));
                options.AddPolicy(ApplicationConstants.UserPolicyName,
                    policy => policy.RequireRole(DomainConstants.BasicUserRoleTitle));
            });

    public static IServiceCollection AddRateLimiters(this IServiceCollection services, IConfiguration configuration)
    {
        var customRateLimitOptions = configuration.GetSection(ApplicationConstants.RateLimitersSectionKey).Get<CustomRateLimitOptions>()!;

        services
            .AddRateLimiter(limiterOptions =>
            {
                limiterOptions.AddFixedWindowLimiter(
                    ApplicationConstants.FixedWindowRateLimiterPolicy,
                    fixedWindowOptions =>
                    {
                        fixedWindowOptions.PermitLimit = customRateLimitOptions.FixedWindowRateLimiterOptions.PermitLimit;
                        fixedWindowOptions.Window = TimeSpan.FromSeconds(customRateLimitOptions.FixedWindowRateLimiterOptions.WindowSeconds);
                        fixedWindowOptions.QueueLimit = customRateLimitOptions.FixedWindowRateLimiterOptions.QueueLimit;
                        fixedWindowOptions.QueueProcessingOrder = customRateLimitOptions.FixedWindowRateLimiterOptions.QueueProcessingOrder;
                        fixedWindowOptions.AutoReplenishment = customRateLimitOptions.FixedWindowRateLimiterOptions.AutoReplenishment;
                    });

                limiterOptions.AddConcurrencyLimiter(
                    ApplicationConstants.ConcurrencyRateLimiterPolicy,
                    concurrencyOptions =>
                    {
                        concurrencyOptions.PermitLimit = customRateLimitOptions.ConcurrencyLimiterOptions.PermitLimit;
                        concurrencyOptions.QueueLimit = customRateLimitOptions.ConcurrencyLimiterOptions.QueueLimit;
                        concurrencyOptions.QueueProcessingOrder = customRateLimitOptions.ConcurrencyLimiterOptions.QueueProcessingOrder;
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
                            AutoReplenishment = customRateLimitOptions.TokenBucketRateLimiterOptions.AutoReplenishment,
                            QueueLimit = customRateLimitOptions.TokenBucketRateLimiterOptions.QueueLimit,
                            QueueProcessingOrder = customRateLimitOptions.TokenBucketRateLimiterOptions.QueueProcessingOrder,
                            ReplenishmentPeriod = TimeSpan.FromSeconds(customRateLimitOptions.TokenBucketRateLimiterOptions.ReplenishmentPeriodSeconds),
                            TokenLimit = customRateLimitOptions.TokenBucketRateLimiterOptions.TokenLimit,
                            TokensPerPeriod = customRateLimitOptions.TokenBucketRateLimiterOptions.TokensPerPeriod
                        });
                });

                limiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            });

        return services;
    }

    public static IServiceCollection AddSerilog(this IServiceCollection services, IConfiguration configuration) =>
        services.AddSerilog(loggerConfiguration =>
            loggerConfiguration.ReadFrom.Configuration(configuration));

    public static IServiceCollection AddCors(this IServiceCollection services, IConfiguration configuration) =>
        services.AddCors(options =>
        {
            var serverUrl = configuration
                .GetSection(ApplicationConstants.ApplicationUrlsConfigurationSectionKey)
                .GetValue<string>(ApplicationConstants.ServerUrlConfigurationKey)!;

            var clientUrl = configuration.GetSection(ApplicationConstants.ApplicationUrlsConfigurationSectionKey)
                .GetValue<string>(ApplicationConstants.ClientUrlConfigurationKey)!;

            options.AddPolicy(ApplicationConstants.RestrictedCorsPolicy, builder =>
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

                        if (origin.StartsWith(serverUrl, StringComparison.CurrentCultureIgnoreCase) ||
                            origin.StartsWith(clientUrl, StringComparison.CurrentCultureIgnoreCase))
                        {
                            return true;
                        }

                        return false;
                    });
            });

            options.AddPolicy(ApplicationConstants.AllowAnyOriginCorsPolicy, builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
}