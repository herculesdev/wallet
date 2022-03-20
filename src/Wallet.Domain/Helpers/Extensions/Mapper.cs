using AutoMapper;
using Mapster;
using Wallet.Domain.Entities.Base;

namespace Wallet.Domain.Helpers.Extensions;

public static class Mapper
{
    public static TDestination To<TDestination>(this object? source) where TDestination : class
    {
        if (source == null)
            return null!;
        
        /*var config = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(Utils.GetAssembly());
            cfg.CreateMap(typeof(TDestination), source.GetType());
            
            cfg.CreateMap(typeof(PagedResult<>), typeof(PagedResult<>));
        });*/

        return source.Adapt<TDestination>();
        //return config.CreateMapper().Map<TDestination>(source);
    }
}
