using InsightFlow.Application.Interfaces;
using MapsterMapper;

namespace InsightFlow.Infrastructure.Services;

public class MappingService : IMappingService
{
    private readonly IMapper _mapper;

    public MappingService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public TDestination Map<TSource, TDestination>(TSource? source) =>
        _mapper.Map<TSource?, TDestination>(source);

    public TDestination Map<TSource, TDestination>(TSource? source, TDestination destination) =>
        _mapper.Map(source, destination);
}