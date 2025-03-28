using InsightFlow.Infrastructure.Common.Constants;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace InsightFlow.Api.Transformers;

public class DocumentInfoTransformer : IOpenApiDocumentTransformer
{
    private readonly IConfiguration _configuration;

    public DocumentInfoTransformer(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        var applicationVersion = _configuration.GetValue<string>(ApplicationConstants.ApplicationVersionConfigurationKey)!;

        document.Info.Contact = new OpenApiContact
        {
            Name = ApplicationConstants.ApplicationContactName,
            Url = new Uri(ApplicationConstants.ApplicationContactUrl),
            Email = ApplicationConstants.ApplicationContactEmail
        };

        document.Info.Version = applicationVersion;

        document.Info.Title = ApplicationConstants.ApplicationName + " | " + "v" + applicationVersion.Split('.').First();

        return Task.CompletedTask;
    }
}