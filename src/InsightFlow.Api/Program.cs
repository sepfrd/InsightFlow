using InsightFlow.Api.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencies();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

app.UseHttpsRedirection();

app.MapGet("/ping", () => { Results.Ok(); })
    .WithName("Ping");

app.MapControllers();

await app.RunAsync();