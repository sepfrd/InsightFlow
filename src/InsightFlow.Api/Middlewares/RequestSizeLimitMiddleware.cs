using InsightFlow.Infrastructure.Common.Configurations;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;

namespace InsightFlow.Api.Middlewares;

public class RequestSizeLimitMiddleware : IMiddleware
{
    private readonly AppOptions _appOptions;

    public RequestSizeLimitMiddleware(IOptions<AppOptions> appOptions)
    {
        _appOptions = appOptions.Value;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var feature = context.Features.Get<IHttpMaxRequestBodySizeFeature>();

        if (feature is { IsReadOnly: false })
        {
            feature.MaxRequestBodySize = _appOptions.ProfileImageMaximumAllowedBytes;
        }

        await next(context);
    }
}