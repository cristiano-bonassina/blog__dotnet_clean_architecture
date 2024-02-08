using LogicArt.Common;

namespace blog__dotnet_clean_architecture.Application.Data.Mapping;

public class Mapper : IMapper
{
    private readonly MapsterMapper.IMapper _mapper;

    public Mapper(MapsterMapper.IMapper mapper)
    {
        _mapper = mapper;
    }

    public T Map<T>(object source) => _mapper.Map<T>(source);

    public void Map<TSource, TDestination>(TSource source, TDestination destination) => _mapper.Map(source, destination);
}
