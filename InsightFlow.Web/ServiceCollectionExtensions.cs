using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using FluentValidation;
using FluentValidation.AspNetCore;
using Humanizer;
using InsightFlow.Api.Conventions;
using InsightFlow.Api.Filters;
using InsightFlow.Business.Businesses;
using InsightFlow.Business.Interfaces;
using InsightFlow.Common.Constants;
using InsightFlow.Common.Dtos.Configurations;
using InsightFlow.Common.Profiles;
using InsightFlow.Common.Validators;
using InsightFlow.DataAccess;
using InsightFlow.DataAccess.Interfaces;
using InsightFlow.DataAccess.Sieve;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Serilog;
using Sieve.Services;

namespace InsightFlow.Web;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection InjectApiControllers(this IServiceCollection services) =>
        services
            .AddControllers(options =>
            {
                options.Filters.Add<CustomExceptionFilter>();
                options.Conventions.Add(new ControllerDocumentationConvention());
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            })
            .Services;

    internal static IServiceCollection InjectRateLimiters(this IServiceCollection services, IConfiguration configuration)
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

    internal static IServiceCollection InjectCors(this IServiceCollection services, IConfiguration configuration) =>
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

    internal static IServiceCollection InjectSwagger(this IServiceCollection services, IConfiguration configuration) =>
        services.AddSwaggerGen(options =>
        {
            options.OperationFilter<CamelCaseOperationFilter>();

            var applicationVersion = configuration.GetValue<string>(ApplicationConstants.ApplicationVersionConfigurationKey);

            options.SwaggerDoc(applicationVersion, new OpenApiInfo
            {
                Title = ApplicationConstants.ApplicationName,
                Version = applicationVersion,
                Description = "A Simple Q/A .NET REST Web API Application",

                Contact = new OpenApiContact
                {
                    Name = ApplicationConstants.ApplicationContactName,
                    Url = new Uri(ApplicationConstants.ApplicationContactUrl),
                    Email = ApplicationConstants.ApplicationContactEmail
                }
            });

            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = MessageConstants.SwaggerAuthorizationMessage,
                Name = HeaderNames.Authorization,
                Type = SecuritySchemeType.Http,
                BearerFormat = JwtConstants.TokenType,
                Scheme = JwtBearerDefaults.AuthenticationScheme.ToLowerInvariant()
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    []
                }
            });

            options.CustomOperationIds(apiDescription =>
                apiDescription.ActionDescriptor.RouteValues["action"]?.Kebaberize());
        });

    internal static IServiceCollection InjectUnitOfWork(this IServiceCollection services) =>
        services.AddScoped<IUnitOfWork, UnitOfWork>();

    internal static IServiceCollection InjectDbContext(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        if (environment.IsEnvironment(ApplicationConstants.TestingEnvironmentName))
        {
            return services.AddDbContext<InsightFlowDbContext>(options =>
                options.UseInMemoryDatabase(ApplicationConstants.ApplicationName));
        }

        return services.AddDbContext<InsightFlowDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(ApplicationConstants.SqlServerConfigurationKey));
            options.EnableSensitiveDataLogging();
        });
    }

    internal static IServiceCollection InjectSerilog(this IServiceCollection services, IConfiguration configuration) =>
        services.AddSerilog(loggerConfiguration =>
            loggerConfiguration.ReadFrom.Configuration(configuration));

    internal static IServiceCollection InjectSieve(this IServiceCollection services) =>
        services.AddScoped<ISieveProcessor, CustomSieveProcessor>();

    internal static IServiceCollection InjectAuth(this IServiceCollection services, IConfiguration configuration) =>
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

                var publicKey = configuration.GetSection(ApplicationConstants.JwtConfigurationSectionKey).GetValue<string>(ApplicationConstants.JwtPublicKeyConfigurationKey);

                var rsa = RSA.Create();

                rsa.ImportFromPem(publicKey);

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
                    policy => policy.RequireRole(ApplicationConstants.AdminRoleName));
                options.AddPolicy(ApplicationConstants.UserPolicyName,
                    policy => policy.RequireRole(ApplicationConstants.UserRoleName));
            });

    internal static IServiceCollection InjectBusinesses(this IServiceCollection services) =>
        services
            .AddScoped<IAnswerBusiness, AnswerBusiness>()
            .AddScoped<IAuthBusiness, AuthBusiness>()
            .AddScoped<IQuestionBusiness, QuestionBusiness>()
            .AddScoped<IUserBusiness, UserBusiness>();

    internal static IServiceCollection InjectFluentValidation(this IServiceCollection services) =>
        services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssemblyContaining<LoginDtoValidator>();

    internal static IServiceCollection InjectAutoMapper(this IServiceCollection services) =>
        services.AddAutoMapper(typeof(AnswerProfile).Assembly);
}