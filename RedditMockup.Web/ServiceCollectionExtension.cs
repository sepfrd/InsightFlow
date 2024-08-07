using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using RedditMockup.Api.Filters;
using RedditMockup.Business.Contracts;
using RedditMockup.Business.DomainEntityBusinesses;
using RedditMockup.Business.PublicBusinesses;
using RedditMockup.Common.Constants;
using RedditMockup.Common.Dtos;
using RedditMockup.Common.Profiles;
using RedditMockup.Common.Validations;
using RedditMockup.DataAccess;
using RedditMockup.DataAccess.Context;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.Model.Entities;
using Serilog;
using Sieve.Services;

namespace RedditMockup.Web;

internal static class ServiceCollectionExtension
{
    internal static IServiceCollection InjectApiControllers(this IServiceCollection services) =>
        services
            .AddControllers(options => options.Filters.Add<CustomExceptionFilter>())
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            })
            .Services;

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
            var applicationVersion = configuration.GetValue<string>(ApplicationConstants.ApplicationVersionConfigurationKey);

            options.SwaggerDoc(applicationVersion, new OpenApiInfo
            {
                Title = ApplicationConstants.ApplicationName,
                Version = applicationVersion
            });

            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = MessageConstants.SwaggerAuthorizationMessage,
                Name = HeaderNames.Authorization,
                Type = SecuritySchemeType.Http,
                BearerFormat = JwtConstants.TokenType,
                Scheme = JwtBearerDefaults.AuthenticationScheme
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
            return services.AddDbContext<RedditMockupDbContext>(options =>
                options.UseInMemoryDatabase(ApplicationConstants.ApplicationName));
        }

        return services.AddDbContext<RedditMockupDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString(ApplicationConstants.SqlServerConfigurationKey));
            options.EnableSensitiveDataLogging();
        });
    }

    internal static IServiceCollection InjectSerilog(this IServiceCollection services, IConfiguration configuration) =>
        services.AddSerilog(loggerConfiguration =>
            loggerConfiguration.ReadFrom.Configuration(configuration));

    internal static IServiceCollection InjectSieve(this IServiceCollection services) =>
        services.AddScoped<ISieveProcessor, SieveProcessor>();

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
        services.AddScoped<IBaseBusiness<User, UserDto>, UserBusiness>()
            .AddScoped<IBaseBusiness<Answer, AnswerDto>, AnswerBusiness>()
            .AddScoped<IBaseBusiness<Question, QuestionDto>, QuestionBusiness>()
            .AddScoped<IPublicBaseBusiness<AnswerDto>, PublicAnswerBusiness>()
            .AddScoped<IPublicBaseBusiness<QuestionDto>, PublicQuestionBusiness>()
            .AddScoped<IAuthBusiness, AuthBusiness>();

    internal static IServiceCollection InjectFluentValidation(this IServiceCollection services) =>
        services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssemblyContaining<RoleValidator>();

    internal static IServiceCollection InjectAutoMapper(this IServiceCollection services) =>
        services.AddAutoMapper(typeof(AnswerProfile).Assembly);
}