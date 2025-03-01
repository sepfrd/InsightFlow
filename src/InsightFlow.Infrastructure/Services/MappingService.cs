using InsightFlow.Application.Interfaces;
using Mapster;

namespace InsightFlow.Infrastructure.Services;

public class MappingService : IMappingService
{
    public TDestination Map<TSource, TDestination>(TSource? source) =>
        source.Adapt<TDestination>();

    public TDestination Map<TSource, TDestination>(TSource? source, TDestination destination) =>
        source.Adapt(destination);
}