using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace FashionStore.Application.Common.Mappings;
public static class MapsterConfig
{
    public static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Default.IgnoreNullValues(true);

    }
}
