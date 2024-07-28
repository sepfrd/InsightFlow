using Microsoft.AspNetCore.Mvc.Filters;
using RedditMockup.Common.Constants;
using ILogger = Serilog.ILogger;

namespace RedditMockup.Api.Filters;

public class CustomExceptionFilter : ExceptionFilterAttribute
{
    private readonly ILogger _logger;

    public CustomExceptionFilter(ILogger logger) =>
        _logger = logger;

    public override void OnException(ExceptionContext filterContext)
    {
        if (filterContext.ExceptionHandled)
        {
            return;
        }

        _logger.Error(
            filterContext.Exception,
            MessageConstants.ExceptionMessage,
            filterContext.Exception.GetType());

        filterContext.ExceptionHandled = true;
    }

    public override Task OnExceptionAsync(ExceptionContext filterContext)
    {
        OnException(filterContext);

        return Task.CompletedTask;
    }
}