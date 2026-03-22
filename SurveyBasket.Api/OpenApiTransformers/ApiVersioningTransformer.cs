using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using Asp.Versioning.ApiExplorer; 


namespace SurveyBasket.Api.OpenApiTransformers;

public class ApiVersioningTransformer(ApiVersionDescription description) : IOpenApiDocumentTransformer
{
    public ApiVersionDescription Description { get; } = description;

    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        document.Info = new OpenApiInfo
        {
            Title = "Survey Basket API",
            Version = Description.ApiVersion.ToString(),
            Description = $"API Description. {(Description.IsDeprecated ? "This API version has been deprecated." : string.Empty)}"
        };
        
        return Task.CompletedTask;
    }
}