using InsightFlow.Infrastructure.Common.Configurations;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;

namespace InsightFlow.Api.Transformers;

public class DocumentInfoTransformer : IOpenApiDocumentTransformer
{
    private readonly AppOptions _appOptions;

    public DocumentInfoTransformer(IOptions<AppOptions> appOptions)
    {
        _appOptions = appOptions.Value;
    }

    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        document.Info.Contact?.Name = _appOptions.ApplicationInformation?.DeveloperName;
        document.Info.Contact?.Url = new Uri(_appOptions.ApplicationInformation?.DeveloperUrl!);
        document.Info.Contact?.Email = _appOptions.ApplicationInformation?.DeveloperEmail;

        document.Info.Version = _appOptions.ApplicationInformation!.Version!;

        document.Info.Title = _appOptions.ApplicationInformation.Name! + " | " + "v" + _appOptions.ApplicationInformation.Version!.Split('.').First();

        return Task.CompletedTask;
    }
}