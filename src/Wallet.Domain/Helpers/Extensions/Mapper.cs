using Mapster;

namespace Wallet.Domain.Helpers.Extensions;

public static class Mapper
{
    public static TDestination? To<TDestination>(this object? source) where TDestination : class
    {
        return source?.Adapt<TDestination>();
    }
}
